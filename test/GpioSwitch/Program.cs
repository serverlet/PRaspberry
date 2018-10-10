using Raspberry.GPIO;
using System;

namespace GpioSwitch
{
    class Program
    {
        static void Main(string[] args)
        {
            using (GpioPin pin = GpioPinManager.Instance.Open(Pin.Pin24))
            {
                pin.DriveMode = GpioPinDriveMode.Out;
                //for (int i = 0; i < 10; i++)
                //{
                pin.Write(GpioPinValue.High);
                System.Threading.Thread.Sleep(5500);
                pin.Write(GpioPinValue.Low);
                //}
            }
            Console.WriteLine("OK");
        }
    }
}
