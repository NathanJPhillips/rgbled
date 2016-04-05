// Copyright © 2015 Daniel Porrey
//
// This file is part of SoftPwmSharp.
// 
// SoftPwmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SoftPwmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with SoftPwmSharp.  If not, see http://www.gnu.org/licenses/.

using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Windows.Devices.Gpio.SoftPwmSharp
{
    /// <summary>
    /// Provides a software based Pulse Width Modulation capability for any GPIO pin on
    /// the device. PWM is used in a variety of circuits as a way to control analog 
    /// circuits through digital interfaces.
    /// </summary>
    public class SoftPwm : ISoftPwm
    {
		/// <summary>
		/// This event is fired for every pulse (after the low pulse). Monitoring of this event
		/// can impact the performance of this Soft PWM and it's ability to keep accurate timing.
		/// </summary>
		public event EventHandler PwmPulsed;

        private Task _pulserTask;
		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

		/// <summary>
		/// Creates an instance of SoftPwm given an instance
		/// of Windows.Devices.Gpio.GpioPin.
		/// </summary>
		/// <param name="pin">An instance of Windows.Devices.Gpio.GpioPin to create the SoftPwm on.</param>
		public SoftPwm(GpioPin pin)
		{
			if (pin == null)
                throw new ArgumentNullException(nameof(pin));

			// Set up the pin
			this.Pin = pin;
			this.Pin.SetDriveMode(GpioPinDriveMode.Output);
			this.Pin.Write(GpioPinValue.Low);

            PulseFrequency = 100;
		}

		/// <summary>
		/// Gets the underlying Windows.Devices.Gpio.GpioPin instance that this SoftPwm instance
		/// is controlling.
		/// </summary>
		public GpioPin Pin { get; private set; }

        private double _pulseFrequency;
        /// <summary>
        /// Gets/set the frequency of the pulse in Hz.
        /// </summary>
        public double PulseFrequency
        {
            get { return this._pulseFrequency; }
            set
            {
                if (value == this._pulseFrequency)
                    return;
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "PulseFrequence must be positive");
                this._pulseFrequency = value;
                OnPropertyChanged(nameof(this.PulseFrequency));
                OnPropertyChanged(nameof(this.PulseWidth));
                OnPropertyChanged(nameof(this.HighPulseWidth));
                OnPropertyChanged(nameof(this.LowPulseWidth));
            }
        }

        /// <summary>
        /// Gets the total width/length in μs (micro-seconds) of the pulse.
        /// </summary>
        public TimeSpan PulseWidth
		{
			get { return TimeSpan.FromSeconds(1 / this.PulseFrequency); }
		}

        private double _value = 0;
        /// <summary>
        /// Gets/sets the current value.
        /// </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                if (value == this._value)
                    return;
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be between 0 and 1");
                this._value = value;
                OnPropertyChanged(nameof(this.Value));
                OnPropertyChanged(nameof(this.HighPulseWidth));
                OnPropertyChanged(nameof(this.LowPulseWidth));
            }
        }

        /// <summary>
        /// Gets the width/length in μs (micro-seconds) of the high pulse.
        /// </summary>
        public TimeSpan HighPulseWidth
		{
			get { return PulseWidth.MultiplyBy(this.Value); }
		}

		/// <summary>
		/// Gets the width/length in μs (micro-seconds) of the low pulse.
		/// </summary>
		public TimeSpan LowPulseWidth
		{
			get { return PulseWidth.MultiplyBy(1 - this.Value); }
		}

		/// <summary>
		/// Start the SoftPwm in the GPIO pin.
		/// </summary>
		public void Start()
		{
			this.ThrowIfDisposed();

            _pulserTask = Task.Factory.StartNew(async () =>
			{
				while (!_cancellationTokenSource.IsCancellationRequested)
				{
                    this.ThrowIfDisposed();
                    // Pulse High unless the value is 0 in which case the output will stay low.
                    if (this.Value != 0)
						this.Pin.Write(GpioPinValue.High);

					// Delay the for the time specified by HighPulseWidth
					await Task.Delay(this.HighPulseWidth);

                    this.ThrowIfDisposed();
                    // Pulse Low unless the value is 1 in which case the output will stay high.
                    if (this.Value != 1)
						this.Pin.Write(GpioPinValue.Low);

					// Delay the for the time specified by LowPulseWidth
					await Task.Delay(this.LowPulseWidth);

                    // Fire the Pulsed event (monitoring of this event
                    // can impact the performance of the application and 
                    // the ability of this code to keep the timing
                    // correct.
                    this.PwmPulsed?.Invoke(this, new EventArgs());
                }
			});
		}

		/// <summary>
		/// Stop the SoftPwm on the GPIO pin.
		/// </summary>
		/// <returns></returns>
		public async Task StopAsync()
		{
			// Call cancel to stop the loop which will allow it to drop out and stop.
			_cancellationTokenSource.Cancel();
            // Wait for task to complete
            await _pulserTask;
        }

		/// <summary>
		/// Stops the SoftPwm if active and calls Dispose on the GPIO pin.
		/// </summary>
		public void Dispose()
		{
			this.StopAsync().Wait();
            if (Pin != null)
            {
                this.Pin.Dispose();
                this.Pin = null;
            }
		}

		/// <summary>
		/// Checks if this instance has been disposed and 
		/// throws the ObjectDisposedException exception if it is.
		/// </summary>
		private void ThrowIfDisposed()
		{
            if (Pin == null)
                throw new ObjectDisposedException(nameof(SoftPwm));
		}

        #region PropertyChanged

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    internal static class TimeSpanUtils
    {
        public static TimeSpan MultiplyBy(this TimeSpan timeSpan, double multiplier)
        {
            return TimeSpan.FromTicks((long)(timeSpan.Ticks * multiplier));
        }
    }
}
