using System;
using Xamarin.Forms;

namespace FormsGallery
{
    class ImageCellDemoPage : ContentPage
    {
        public ImageCellDemoPage()
        {
            Label header = new Label
            {
                Text = "ImageCell",
				FontSize = 50,
				FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            ImageCell imageCell = new ImageCell
            {
                Text = "This is an ImageCell",
                Detail = "This is some detail text",
            };

            // Some differences with loading images in initial release.
            if (Device.RuntimePlatform == Device.iOS)
            {
                imageCell.ImageSource = ImageSource.FromUri(new Uri("https://www.xamarin.com/content/images/pages/branding/assets/xamagon.png"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                imageCell.ImageSource = ImageSource.FromFile("ide_xamarin_studio.png");
            }
            else if (Device.RuntimePlatform == Device.WinPhone || Device.RuntimePlatform == Device.WinPhone || Device.RuntimePlatform == Device.UWP)
            {
                imageCell.ImageSource = ImageSource.FromFile("Images/ide-xamarin-studio.png");
            }
            else
            {
                imageCell.ImageSource = ImageSource.FromFile("ide_xamarin_studio.png");
            }

            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        imageCell
                    }
                }
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    tableView
                }
            };
        }
    }
}
