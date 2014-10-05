using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DiplomaSeminar.Core.Helpers;
using DiplomaSeminar.Core.Interfaces;
using DiplomaSeminar.Core.Services;
using DiplomaSeminar.Core.ViewModels;
using SQLite.Net;
using SQLite.Net.Interop;
#if __IOS__
using SQLite.Net.Platform.XamarinIOS;
using Microsoft.WindowsAzure.MobileServices;
#elif __ANDROID__
using SQLite.Net.Platform.XamarinAndroid;
#elif WINDOWS_PHONE
using Windows.Storage;
using SQLite.Net.Platform.WindowsPhone8;
#endif

namespace DiplomaSeminar.Droid.Helpers
{
    public static class ServiceRegistrar
    {
        public static void Startup()
        {
            SQLiteConnection connection = null;
            string dbLocation = "presentationsDB.db3";

#if __ANDROID__
            var library = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dbLocation = Path.Combine(library, dbLocation);
            var platform = new SQLitePlatformAndroid();
            connection = new SQLiteConnection(platform, dbLocation);

#elif __IOS__
      CurrentPlatform.Init();
      var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      var libraryPath = Path.Combine(docsPath, "../Library/");
      dbLocation = Path.Combine(libraryPath, dbLocation);
      var platform = new SQLitePlatformIOS();
      connection = new SQLiteConnection(platform, dbLocation);
      
#elif WINDOWS_PHONE
      var platform = new SQLitePlatformWP8();
      dbLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbLocation);
      connection = new SQLiteConnection(platform, dbLocation);
#endif
            ServiceContainer.Register<IPresentationService>(() => new PresentationService(connection));
            ServiceContainer.Register<PresentationsViewModel>();
            ServiceContainer.Register<AddViewModel>();
        }
    }
}