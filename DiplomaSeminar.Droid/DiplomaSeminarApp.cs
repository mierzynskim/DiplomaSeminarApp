using System;
using Android.App;
using DiplomaSeminar.Droid.Helpers;

namespace DiplomaSeminar.Droid
{
    [Application(Theme = "@android:style/Theme.Holo.Light")]
    public class DiplomaSeminarApp : Application
    {
        public static Activity CurrentActivity { get; set; }
        public DiplomaSeminarApp(IntPtr handle, global::Android.Runtime.JniHandleOwnership transer)
            : base(handle, transer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();
            ServiceRegistrar.Startup();
        }
    }
}