using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Raspberry.GPIO
{
    public class GpioPinManager
    {
        private static GpioPinManager instance = new GpioPinManager();

        public static string DevicePath { get; private set; }

        public static GpioPinManager Instance
        {
            get
            {
                if (Environment.OSVersion.Platform != PlatformID.Unix)
                {
                    throw new NotSupportedException("不支持当前操作系统");
                }
                DevicePath = "/sys/class/gpio";
                return instance;
            }
        }

        public IDictionary<string, string> Pins
        {
            get
            {
                DirectoryInfo gpioPinsDic = new DirectoryInfo(DevicePath);

                IDictionary<string, string> keyvaluepairs = new Dictionary<string, string>();

                var pinNames = gpioPinsDic.GetDirectories().Where(m => m.Name.StartsWith("gpio")).Where(m => !m.Name.StartsWith("gpiochip")).Select(m => m.Name).ToArray();

                foreach (var pinName in pinNames)
                {
                    keyvaluepairs.Add(pinName, File.ReadAllText(Path.Combine(DevicePath, pinName, "value")));
                }

                return keyvaluepairs;
            }
        }

        public GpioPin Open(Pin pinNumber)
        {
            // add a file to the export directory with the name <<pin number>>
            // add folder under device path for "gpio<<pinNumber>>"
            var gpioDirectoryPath = Path.Combine(DevicePath, string.Concat("gpio", (int)pinNumber));

            var gpioExportPath = Path.Combine(DevicePath, "export");
Console.WriteLine(gpioExportPath);
            if (!Directory.Exists(gpioDirectoryPath))
            {
                File.WriteAllText(gpioExportPath, ((int)pinNumber).ToString());
                Directory.CreateDirectory(gpioDirectoryPath);
            }
	    

            // instantiate the gpiopin object to return with the pin number.
            return new GpioPin(pinNumber, gpioDirectoryPath);
        }

        public void ClosePin(Pin pinNumber)
        {
            var gpioDirectoryPath = Path.Combine(DevicePath, string.Concat("gpio", ((int)pinNumber).ToString()));

            var gpioExportPath = Path.Combine(DevicePath, "unexport");

            if (Directory.Exists(gpioDirectoryPath))
            {
                File.WriteAllText(gpioExportPath, ((int)pinNumber).ToString());
            }
        }
    }
}
