using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.Camera
{
    public class Camera : IDisposable
    {
        public RaspistillParam RaspistillParam { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Action<string> Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private StringBuilder _outputString;

        /// <summary>
        /// 
        /// </summary>
        public string OutputString { get { return _outputString.ToString(); } }

        private ProcessStartInfo info;

        private Process process;
        public Camera()
        {
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                throw new NotSupportedException($"不支持此{Environment.OSVersion.Platform}平台");
            }
            info = new ProcessStartInfo("ipconfig") { RedirectStandardOutput = true, UseShellExecute = false };
            RaspistillParam = new RaspistillParam() { Output = "image.jpg" };
            process = new Process();
            _outputString = new StringBuilder();
            process.StartInfo = info;
        }

        public Camera(RaspistillParam _RaspistillParam) : this()
        {
            RaspistillParam = _RaspistillParam;
        }

        public string Capture()
        {
            process.StartInfo.Arguments = RaspistillParam.ToString();
            Logger?.BeginInvoke(process.StartInfo.Arguments, null, null);
            process.Start();
            using (var sr = process.StandardOutput)
            {
                while (!sr.EndOfStream)
                {
                    _outputString.AppendLine(sr.ReadLine());
                }
            }
            return RaspistillParam.Output;
        }

        public void Dispose()
        {
            process.Close();
        }
    }
}
