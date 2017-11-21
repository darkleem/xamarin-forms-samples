using Xamarin.Forms.Platform.Tizen;

namespace NativeSwitch.Tizen
{
    class Check : ElmSharp.Check
    {
        public Check() : base(Forms.Context.MainWindow)
        {
        }
    }
}
