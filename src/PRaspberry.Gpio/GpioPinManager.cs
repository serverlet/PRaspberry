using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Raspberry.GPIO
{
    public class GpioPinManager
    {
        private static GpioPinManager instance = new GpioPinManager();

        const string DevicePath = "\\sys\\class\\gpio";

        public static GpioPinManager Instance
        {
            get
            {
                /*if (Environment.OSVersion.Platform != PlatformID.Unix)
                {
                    throw new NotSupportedException("只支持Linux系统");
                }*/
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
            var gpioDirectoryPath = Path.Combine(DevicePath, string.Concat("gpio", pinNumber.ToString("d")));
            var gpioExportPath = Path.Combine(DevicePath, "export");
            if (!Directory.Exists(gpioDirectoryPath))
            {
                File.WriteAllText(gpioExportPath, pinNumber.ToString("d"));
                //Directory.CreateDirectory(gpioDirectoryPath);
            }
            return new GpioPin(pinNumber, gpioDirectoryPath);
        }

        public void ClosePin(Pin pinNumber)
        {
            var gpioDirectoryPath = Path.Combine(DevicePath, string.Concat("gpio", pinNumber.ToString("d")));

            var gpioExportPath = Path.Combine(DevicePath, "unexport");

            if (Directory.Exists(gpioDirectoryPath))
            {
                File.WriteAllText(gpioExportPath, pinNumber.ToString("d"));
            }
        }
    }
}
