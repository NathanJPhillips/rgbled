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
using System.Threading.Tasks;

namespace Windows.Devices.Gpio.SoftPwmSharp
{
	public interface ISoftPwm : INotifyPropertyChanged, IDisposable
    {
		GpioPin Pin { get; }
		double PulseFrequency { get; set; }
        TimeSpan PulseWidth { get; }
        TimeSpan HighPulseWidth { get; }
        TimeSpan LowPulseWidth { get; }
		double Value { get; set; }
		void StartAsync();
		Task StopAsync();
    }
}