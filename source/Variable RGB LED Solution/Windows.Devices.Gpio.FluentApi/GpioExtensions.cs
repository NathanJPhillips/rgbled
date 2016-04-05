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
	/// Provides a set of extension methods to create a fluent API for
	/// the GPIO pin.
	/// </summary>
	public static class GpioFluentApiExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="gpio">The GPIO Controller.</param>
		/// <param name="pinNumber">The pin number on the controller to assign.</param>
		/// <returns>Returns an instance of Windows.Devices.Gpio.IGpioPinConfiguration
		/// used to open the pin on the controller.</returns>
		public static IGpioPinConfiguration OnPin(this GpioController gpio, int pinNumber)
		{
			return
                new GpioPinConfiguration
			    {
				    Gpio = gpio,
				    PinNumber = pinNumber,
				    SharingMode = GpioSharingMode.SharedReadOnly
			    };
        }

		/// <summary>
		/// Specifies that the pin on the controller should be opened in exclusive mode.
		/// </summary>
		/// <param name="configuration">An instance of Windows.Devices.Gpio.IGpioPinConfiguration
		/// containing the current configuration used top open the pin on the controller.</param>
		/// <returns>Returns an instance of Windows.Devices.Gpio.IGpioPinConfiguration
		/// used to open the pin on the controller.</returns>
		public static IGpioPinConfiguration AsExclusive(this IGpioPinConfiguration configuration)
		{
            return
                new GpioPinConfiguration
                {
                    Gpio = configuration.Gpio,
                    PinNumber = configuration.PinNumber,
                    SharingMode = GpioSharingMode.Exclusive
                };
        }

		/// <summary>
		/// Specifies that the pin on the controller should be opened in shared read-only mode.
		/// </summary>
		/// <param name="configuration">An instance of Windows.Devices.Gpio.IGpioPinConfiguration
		/// containing the current configuration used top open the pin on the controller.</param>
		/// <returns>Returns an instance of Windows.Devices.Gpio.IGpioPinConfiguration
		/// used to open the pin on the controller.</returns>
		public static IGpioPinConfiguration AsSharedReadOnly(this IGpioPinConfiguration configuration)
		{
            return
                new GpioPinConfiguration
                {
                    Gpio = configuration.Gpio,
                    PinNumber = configuration.PinNumber,
                    SharingMode = GpioSharingMode.SharedReadOnly
                };
        }

        /// <summary>
        /// Opens the pin on the controller using the configuration provided.
        /// </summary>
        /// <param name="configuration">An instance of Windows.Devices.Gpio.IGpioPinConfiguration
        /// containing the current configuration used top open the pin on the controller.</param>
        /// <returns>Returns an instance of Windows.Devices.Gpio.GpioPin if it is successfully
        /// opened.</returns>
        public static GpioPin Open(this IGpioPinConfiguration configuration)
		{
			return configuration.Gpio.OpenPin(configuration.PinNumber, configuration.SharingMode);
		}
	}
}
