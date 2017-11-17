using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;

namespace WorkingWithImages
{
    public class EmbeddedImages : ContentPage
    {
        public EmbeddedImages()
        {
            var embeddedImage = new Image { Aspect = Aspect.AspectFit };
            View resultView;

            // resource identifiers start with assembly-name DOT filename
            // 'try-catch' clause is used to handle unsupported method on Tizen platform.
            try
            {
                embeddedImage.Source = ImageSource.FromResource("WorkingWithImages.beach.jpg");
                resultView = embeddedImage;
            }
            catch
            {
                resultView = new Frame
                {
                    Content = new Label { Text = "ImageSource.FromResource() is currently not supported on Tizen platform." }
                };
            }

            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "ImageSource.FromResource",
                        FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold
                    },
                    resultView,
                    new Label { Text = "WorkingWithImages.beach.jpg embedded resource" }
                },
                Padding = new Thickness(0, 20, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            // NOTE: use for debugging, not in released app code!
            //var assembly = typeof(EmbeddedImages).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //	System.Diagnostics.Debug.WriteLine("found resource: " + res);
        }
    }
}
