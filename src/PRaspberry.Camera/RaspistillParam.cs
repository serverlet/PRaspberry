using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.Camera
{

    /// <summary>
    /// http://shumeipai.nxez.com/2014/09/21/raspicam-documentation.html
    /// https://www.raspberrypi.org/documentation/raspbian/applications/camera.md
    /// </summary>
    public class RaspistillParam
    {
        /// <summary>
        /// 设置图像宽度 <尺寸>
        /// </summary>
        private int? _width;

        /// <summary>
        /// 设置图像高度 <尺寸>
        /// </summary>

        private int? _height;

        /// <summary>
        /// 设置jpeg品质 0 -100
        /// </summary>
        private int? _quality;

        /// <summary>
        /// 增加raw原始拜尔数据到JPEG元数据
        /// </summary>
        private bool _raw;

        /// <summary>
        /// 输出文件名 <文件名>，如果要写到stdout，使用`-o -`，如果不特别指定，图像文件不会被保存
        /// </summary>
        private string _output;

        /// <summary>
        /// 在运行摄像头时输出详细信息
        /// </summary>
        private bool _verbose;

        /// <summary>
        /// 单位毫秒，拍照和关闭时的延时指定，未指定时默认是5s
        /// </summary>
        private int _timeout = 1;

        /// <summary>
        /// 
        /// </summary>
        private ImageEncoding _encoding;

        /// <summary>
        /// 设置水平翻转
        /// </summary>
        private bool _hflip;

        /// <summary>
        /// 设置垂直翻转
        /// </summary>
        private bool _vflip;

        public string Output
        {
            get
            {
                return _output;
            }

            set
            {
                _output = value;
            }
        }

        public int? Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }

        public int? Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }

        public ImageEncoding Encoding
        {
            get
            {
                return _encoding;
            }

            set
            {
                _encoding = value;
            }
        }

        public bool Verbose
        {
            get
            {
                return _verbose;
            }

            set
            {
                _verbose = value;
            }
        }

        public int Timeout
        {
            get
            {
                return _timeout;
            }

            set
            {
                _timeout = value;
            }
        }

        public bool Hflip
        {
            get
            {
                return _hflip;
            }

            set
            {
                _hflip = value;
            }
        }

        public bool Vflip
        {
            get
            {
                return _vflip;
            }

            set
            {
                _vflip = value;
            }
        }

        public bool Raw
        {
            get
            {
                return _raw;
            }

            set
            {
                _raw = value;
            }
        }

        public int? Quality
        {
            get
            {
                return _quality;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new OverflowException("");
                }
                _quality = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            Type p = this.GetType();

            PropertyInfo[] ps = p.GetProperties();

            foreach (PropertyInfo item in ps)
            {
                object value = item.GetValue(this);
                if (value != null)
                {
                    sb.Append($" --{item.Name.ToLower()}");
                    if (item.PropertyType.IsValueType && value is bool)
                    {
                        continue;
                    }

                    if (item.PropertyType.IsEnum)
                    {
                        value = value.ToString().ToLower();
                    }

                    sb.Append($" {value}");
                }

            }
            return sb.ToString();
        }
    }
}
