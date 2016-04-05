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
	/// Fluent API Extension for SoftPwm.
	/// </summary>
	public static class SoftPwmExtensions
	{
		/// <summary>
		/// Creates an instance of a SoftPwm object from the given 
		/// Windows.Devices.Gpio.GpioPin instance.
		/// </summary>
		/// <param name="pin">An instance of Windows.Devices.Gpio.GpioPin to 
		/// create the SoftPwm on.</param>
		/// <returns>Returns a new SOftPwm instance.</returns>
		public static ISoftPwm AssignSoftPwm(this GpioPin pin)
		{
			return new SoftPwm(pin);
		}

		/// <summary>
		/// Sets the value of a SoftPwm instance with the given value.
		/// </summary>
		/// <param name="pwm">The instance of SoftPwm to start.</param>
		/// <param name="value">The value to set the SoftPwm instance to.</param>
		/// <returns></returns>
		public static ISoftPwm SetValue(this ISoftPwm pwm, double value)
		{
			pwm.Value = value;
			return pwm;
		}

		/// <summary>
		/// Sets the pulse frequency (in Hz) of the SoftPwm instance.
		/// </summary>
		/// <param name="pwm">The instance of SoftPwm to start.</param>
		/// <param name="pulseFrequency">The pulse frequency to use given in Hz.</param>
		/// <returns></returns>
		public static ISoftPwm SetPulseFrequency(this ISoftPwm pwm, double pulseFrequency)
		{
			pwm.PulseFrequency = pulseFrequency;
			return pwm;
		}
	}
}
