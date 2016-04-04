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

namespace Windows.Devices.Gpio.SoftPwmSharp
{
	/// <summary>
	/// Provides data for the PulseWidthChangedEvent.
	/// </summary>
	public class PulseWidthChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Creates an instance of PulseWidthChangedEventArgs with the provide
		/// values for HighPulseWidth and LowPulseWidth.
		/// </summary>
		/// <param name="highPulseWidth"></param>
		/// <param name="lowPulseWidth"></param>
		public PulseWidthChangedEventArgs(TimeSpan highPulseWidth, TimeSpan lowPulseWidth)
		{
			this.HighPulseWidth = highPulseWidth;
			this.LowPulseWidth = lowPulseWidth;
		}

		/// <summary>
		/// Gets the length in μs (micro-seconds) of the high pulse.
		/// </summary>
		public TimeSpan HighPulseWidth { get; private set; }

		/// <summary>
		/// Gets the length in μs (micro-seconds) of the low pulse.
		/// </summary>
		public TimeSpan LowPulseWidth { get; private set; }
	}

    /// <summary>
    /// Delegate for the PulseWidthChangedEvent.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    public delegate void PulseWidthChangedEventHandler(object sender, PulseWidthChangedEventArgs e);
}
