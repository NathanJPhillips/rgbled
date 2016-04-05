// Copyright © 2015 Daniel Porrey
//
// This file is part of GPIO Fluent API.
// 
// GPIO Fluent API is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// GPIO Fluent API is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Variable RGB LED.  If not, see http://www.gnu.org/licenses/.

namespace Windows.Devices.Gpio.FluentApi
{
	/// <summary>
	/// Concrete internal implementation of IGpioPinConfiguration
	/// used by the fluent API.
	/// </summary>
	internal class GpioPinConfiguration : IGpioPinConfiguration
	{
		/// <summary>
		/// Gets/sets a reference to the GPIO controller
		/// </summary>
		public GpioController Gpio { get; set; }

		/// <summary>
		/// Gets/sets the pin number on the GPIO
		/// controller to be initialized.
		/// </summary>
		public int PinNumber { get; set; }

		/// <summary>
		/// Get/sets the sharing mode to be used on the pin.
		/// </summary>
		public GpioSharingMode SharingMode { get; set; }
	}
}
