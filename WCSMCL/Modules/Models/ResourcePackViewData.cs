using Avalonia.Media.Imaging;
using MinecraftLaunch.Modules.Models.Download;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCSMCL.Modules.Toolkits;
using WCSMCL.ViewModels;

namespace WCSMCL.Modules.Models
{
    public class ResourcePackViewData<T> : ViewDataBase<T>
    {
        public ResourcePackViewData(T data) : base(data)
        {    }       
       

        public Bitmap PackLogo
        {
            get => _;
            set => RaiseAndSetIfChanged(ref _, value);
        }

        public Bitmap _ = (BitmapToolkit.GetAssetsImage("resm:WCSMCL.Resources.normal.png")! as Bitmap)!;
    }
}
