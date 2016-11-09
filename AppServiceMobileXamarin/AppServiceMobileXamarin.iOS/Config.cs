using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AppServiceMobileXamarin.Interface;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppServiceMobileXamarin.iOS.Config))]

namespace AppServiceMobileXamarin.iOS
{
    public class Config : IConfig
    {
        private string directoryDB;
        private ISQLitePlatform platform;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    var directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directoryDB = Path.Combine(directory, "..", "Library");
                }
                return directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
                }

                return platform;
            }
        }
    }
}