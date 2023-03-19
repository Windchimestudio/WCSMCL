using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCSMCL.Modules.Toolkits;

namespace WCSMCL.Views.Converters
{
    /// <summary>
    /// 图像转换器
    /// </summary>
    internal class BitmapConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
                return BitmapToolkit.GetAssetsImage("resm:WCSMCL.Resources.normal.png");

            try
            {
                if (((bool)value) is true)
                    return BitmapToolkit.GetAssetsImage("resm:WCSMCL.Resources.forge.png");
                else
                    return BitmapToolkit.GetAssetsImage("resm:WCSMCL.Resources.normal.png");
            }
            catch { }

            return BitmapToolkit.GetAssetsImage("resm:WCSMCL.Resources.normal.png");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}