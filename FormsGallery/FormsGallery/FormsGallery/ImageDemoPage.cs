using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace FormsGallery
{
    class ImageDemoPage : ContentPage
    {
        public ImageDemoPage()
        {
            Label header = new Label
            {
                Text = "Image",
				FontSize = 50,
				FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            Image image = new Image
            {
                //Source = new UriImageSource
                //{
                //    Uri = new Uri("http://xamarin.com/images/index/ide-xamarin-studio.png")
                //},
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Debug.WriteLine($"@@@@@@@ tewqtewqtewqtewq : {Device.RuntimePlatform}");

            // Some differences with loading images in initial release.
            if (Device.RuntimePlatform == Device.iOS)
            {
                image.Source = ImageSource.FromUri(new Uri("https://www.xamarin.com/content/images/pages/branding/assets/xamagon.png"));
            }
            else if(Device.RuntimePlatform == Device.Android)
            {
                image.Source = ImageSource.FromFile("ide_xamarin_studio.png");
            }
            else if(Device.RuntimePlatform == Device.WinPhone || Device.RuntimePlatform == Device.WinPhone || Device.RuntimePlatform == Device.UWP)
            {
                image.Source = ImageSource.FromUri(new Uri("https://www.xamarin.com/content/images/pages/branding/assets/xamagon.png"));
            }
            else if(Device.RuntimePlatform == "tizen")
            {
                image.Source = ImageSource.FromFile("ide_xamarin_studio.png");
            }

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    image
                }
            };
        }
    }
}
