using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using Avalonia.Themes.Fluent;
using FluentAvalonia.Styling;
using Flurl;
using MinecraftLaunch.Modules.Toolkits;
using Natsurainko.FluentCore.Class.Model.Launch;
using Natsurainko.Toolkits.Network;
using Newtonsoft.Json;
using ReactiveUI;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using WCSMCL.Modules.Const;
using WCSMCL.Modules.Controls;
using WCSMCL.Modules.Models;
using WCSMCL.Modules.Toolkits;

namespace WCSMCL
{
    public partial class App : Application
    {
        public App()
        {
            ServicePointManager.DefaultConnectionLimit = 512;
            InitializeModData();
            CheckAsync();
        }

        /// <summary>
        /// 活动核心
        /// </summary>
        public static GameCore CurrentGameCore { get; set; } = new();
        public static DataModels Data { get; set; } = new DataModels();
        /// <summary>
        /// 预启动数据检查
        /// </summary>
        public void CheckAsync()
        {
            ///Modules/Controls/zh-cn.axaml
            BackgroundWorker worker = new();
            worker.DoWork += async (_, _) =>
            {
                JsonToolkit.JsonAllWrite();
                await Task.Delay(900);
                //await WebToolkit.VersionCheckAsync();
                if (!File.Exists(PathConst.DownloaderPath))
                {
                    //resm:WCSMCL.Resources.WCSMCL.Desktop.exe
                    var al = AvaloniaLocator.Current.GetService<IAssetLoader>();
                    var s = al!.Open(new Uri($"resm:WCSMCL.Resources.WCSMCL.Desktop.exe"));

                    if (InfoConst.IsLinux)
                        s = al!.Open(new("resm:WCSMCL.Resources.WCSMCL.Desktop-Linux"));
                    else if(InfoConst.IsMacOS)
                        s = al!.Open(new("resm:WCSMCL.Resources.WCSMCL.Desktop-Mac"));

                    using FileStream fileStream = File.Create(PathConst.DownloaderPath);
                    byte[] bytes = new byte[HttpToolkit.BufferSize];
                    for (int num = await s.ReadAsync(bytes, 0, HttpToolkit.BufferSize); num > 0; num = await s.ReadAsync(bytes, 0, HttpToolkit.BufferSize))
                        await fileStream.WriteAsync(bytes, 0, num);

                    fileStream.Flush();
                    fileStream.Close();

                    if (InfoConst.IsLinux)
                    {
                        var info = new ProcessStartInfo("chmod", $"+x {PathConst.DownloaderPath}")
                        {
                            RedirectStandardOutput = true
                        };
                        using var process = Process.Start(info);
                    }
                }

                if (Data is not null)
                {
                    //Java
                    if(Data.JavaList is not null)
                        Data.JavaList = Data.JavaList.Distinct().ToList();

                    //Game Footer
                    if (Data.GameFooterList is not null)
                        Data.GameFooterList = Data.GameFooterList.Distinct().ToList();

                    //GameCore
                    if (!string.IsNullOrEmpty(Data.FooterPath) && !string.IsNullOrEmpty(Data.SelectedGameCore)) {                    
                        foreach (var i in GameCoreToolkit.GetGameCores(Data.FooterPath)) {
                            if (Data.SelectedGameCore.Equals(i.Id)) {
                                break;
                            }
                        }
                    }
                    else Data.SelectedGameCore = string.Empty;
                }
            };
            worker.RunWorkerAsync();
        }
        public async void InitializeModData()
        {
            //var res = await BitmapToolkit.CropSkinImage("C:\\Users\\w\\Desktop\\总整包\\MC\\mc皮肤资源\\starcloudsea.png");
            //BitmapToolkit.ResizeImage(res, 512, 512).Save("C:\\Users\\w\\Desktop\\starcloudsea.jpg");
            //
            var al = AvaloniaLocator.Current.GetService<IAssetLoader>();
            await Task.Run(() =>
            {
                using var s = al!.Open(new Uri("resm:WCSMCL.Resources.ModData.json"));
                StreamReader stream = new(s);
                var model = JsonConvert.DeserializeObject<List<ModLangDataModel>>(stream.ReadToEnd());
                if (model != null)
                {
                    if(InfoConst.ModLangDatas == null)
                    {
                        InfoConst.ModLangDatas = new();
                    }
                    model.ForEach(x => {
                        if (x.CurseForgeId != null)
                        {
                            InfoConst.ModLangDatas.Add(x.CurseForgeId, x);
                        }
                        });
                    InfoConst.ModLangDatas.Values.ToList().ForEach(x =>
                    {
                        if (x.Chinese.Contains("*"))
                            x.Chinese = x.Chinese.Replace("*",
                                " (" + string.Join(" ", x.CurseForgeId.Split("-").Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1, w.Length - 1))) + ")");
                    });
                }
            }
                );
        }
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        public override void RegisterServices()
        {
            if (InfoConst.IsLinux) {
                AvaloniaLocator.CurrentMutable.Bind<IFontManagerImpl>().ToConstant(new CustomFontManagerImpl());
            }
         
            base.RegisterServices();
        }
    }

    public class CustomFontManagerImpl : IFontManagerImpl
    {
        private readonly Typeface[] _customTypefaces;
        private readonly string _defaultFamilyName;

        //Load font resources in the project, you can load multiple font resources
        private readonly Typeface _defaultTypeface =
            new Typeface("dejavu");

        private readonly string[] _fallbackFontFamilies =
        {
            "Microsoft YaHei", //微软雅黑

            "Noto Sans CJK SC", //NotoSansCJK全家桶
            "Noto Sans CJK TC",
            "Noto Sans CJK HK",
            "Noto Sans CJK JP",
            "Noto Sans CJK KR",

            "WenQuanYi Micro Hei", //文泉驿微米黑

            "Segoe UI",
            "Ubuntu", //Ubuntu自带字体
        };

        private SKTypeface? _fallbackTypeFace;

        public CustomFontManagerImpl()
        {
            _customTypefaces = new[] { _defaultTypeface };
            _defaultFamilyName = _defaultTypeface.FontFamily.FamilyNames.PrimaryFamilyName;
        }

        public string GetDefaultFontFamilyName()
        {
            return _defaultFamilyName;
        }

        public IEnumerable<string> GetInstalledFontFamilyNames(bool checkForUpdates = false)
        {
            return _customTypefaces.Select(x => x.FontFamily.Name);
        }

        private readonly string[] _bcp47 = { CultureInfo.CurrentCulture.ThreeLetterISOLanguageName, CultureInfo.CurrentCulture.TwoLetterISOLanguageName };

        public bool TryMatchCharacter(int codepoint, FontStyle fontStyle, FontWeight fontWeight, FontFamily fontFamily,
            CultureInfo culture, out Typeface typeface)
        {
            foreach (var customTypeface in _customTypefaces)
            {
                if (customTypeface.GlyphTypeface.GetGlyph((uint)codepoint) == 0)
                {
                    continue;
                }

                typeface = new Typeface(customTypeface.FontFamily.Name, fontStyle, fontWeight);

                return true;
            }

            var fallback = SKFontManager.Default.MatchCharacter(fontFamily?.Name, (SKFontStyleWeight)fontWeight,
                SKFontStyleWidth.Normal, (SKFontStyleSlant)fontStyle, _bcp47, codepoint);

            typeface = new Typeface(fallback?.FamilyName ?? _defaultFamilyName, fontStyle, fontWeight);

            return true;
        }

        public IGlyphTypefaceImpl CreateGlyphTypeface(Typeface typeface)
        {
            SKTypeface skTypeface;
            Trace.WriteLine(_defaultTypeface.FontFamily.Name);
            switch (typeface.FontFamily.Name)
            {
                case FontFamily.DefaultFontFamilyName:
                case "dejavu":  //font family name
                    skTypeface = SKTypeface.FromFamilyName(_defaultTypeface.FontFamily.Name); break;
                default:
                    skTypeface = SKTypeface.FromFamilyName(typeface.FontFamily.Name,
                        (SKFontStyleWeight)typeface.Weight, SKFontStyleWidth.Normal, (SKFontStyleSlant)typeface.Style);
                    break;
            }

            if (skTypeface != null)
                return new GlyphTypefaceImpl(skTypeface);

            if (_fallbackTypeFace != null)
                return new GlyphTypefaceImpl(_fallbackTypeFace);

            foreach (var name in _fallbackFontFamilies)
            {
                var fbTypeFace = SKTypeface.FromFamilyName(name);
                if (fbTypeFace == null) continue;

                skTypeface = fbTypeFace;
                _fallbackTypeFace = fbTypeFace;
                break;
            }

            if (skTypeface == null)
                throw new FontNotFoundException($"未能为{typeface.FontFamily.FamilyNames}找到合适的Fallback字体");

            return new GlyphTypefaceImpl(skTypeface);
        }
    }

    public class FontNotFoundException : Exception
    {
        public FontNotFoundException(String msg)
            : base(msg)
        {
        }
    }
}
//Symbolsresm:WCSMCL.Resources.HarmonyOS#HarmonyOS Sans