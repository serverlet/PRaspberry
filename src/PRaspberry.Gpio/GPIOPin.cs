using System;
using System.IO;

namespace Raspberry.GPIO
{
    public class GpioPin : IDisposable
    {
        public Pin PinNumber { get; private set; }

        public string GpioPath { get; private set; }

        public GpioPin(Pin pinNumber, string gpioPath)
        {
            this.PinNumber = pinNumber;
            this.GpioPath = gpioPath;
        }

        public void Dispose()
        {
            var controller = GpioPinManager.Instance;
            controller.ClosePin(PinNumber);
            //Dispose();
        }
        public GpioPinDriveMode DriveMode
        {
            get
            {
                string retu = File.ReadAllText(Path.Combine(this.GpioPath, "direction"));
                return (GpioPinDriveMode)Enum.Parse(typeof(GpioPinDriveMode), retu, true);
            }
            set
            {
                if (value == GpioPinDriveMode.Out)
                {
                    File.WriteAllText(Path.Combine(this.GpioPath, "direction"), "out");
                    Directory.SetLastWriteTime(Path.Combine(this.GpioPath), DateTime.UtcNow);
                }
                else
                {
                    File.WriteAllText(Path.Combine(this.GpioPath, "direction"), "in");
                    Directory.SetLastWriteTime(Path.Combine(this.GpioPath), DateTime.UtcNow);
                }
            }
        }

        public void Write(GpioPinValue pinValue)
        {
            File.WriteAllText(Path.Combine(this.GpioPath, "value"), ((int)pinValue).ToString());
            Directory.SetLastWriteTime(Path.Combine(this.GpioPath), DateTime.UtcNow);
        }

        public GpioPinValue Read()
        {
            if (File.Exists(Path.Combine(this.GpioPath, "value")))
            {
                var pinValue = File.ReadAllText(Path.Combine(this.GpioPath, "value"));

                return (GpioPinValue)Enum.Parse(typeof(GpioPinValue), pinValue);
            }
            else
            {
                return GpioPinValue.Low;
            }
        }
    }
}
