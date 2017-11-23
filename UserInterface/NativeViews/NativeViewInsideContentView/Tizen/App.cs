namespace NativeViewInsideContentView.Tizen
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        static public ElmSharp.Window NativeMainWindow => Xamarin.Forms.Platform.Tizen.Forms.Context.MainWindow;

        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            app.Run(args);
        }
    }

}
