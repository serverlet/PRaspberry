using System;
using Raspberry.GPIO;

namespace GpioSwitch
{
    class Program
    {
        static void Main(string[] args)
        {
            using (GpioPin pin = GpioPinManager.Instance.Open(Pin.Pin24))
            {
                //pin.SetDriveMode(GpioPinDriveMode.Output);
                pin.Write(GpioPinValue.High);
                System.Threading.Thread.Sleep(2500);
                pin.Write(GpioPinValue.Low);
            }
        }
    }
}
