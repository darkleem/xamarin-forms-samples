using Xamarin.Forms.Platform.Tizen;

namespace CustomRenderer.Tizen
{
    public class Program : FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Forms.Context.MainWindow.AvailableRotations = ElmSharp.DisplayRotation.Degree_0;
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            app.Run(args);
        }
    }
}
