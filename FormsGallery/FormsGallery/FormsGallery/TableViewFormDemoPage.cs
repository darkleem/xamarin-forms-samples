using System;
using Xamarin.Forms;

namespace FormsGallery
{
    class TableViewFormDemoPage : ContentPage
    {
        public TableViewFormDemoPage()
        {
            Label header = new Label
            {
                Text = "TableView for a form",
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            ImageCell imageCell = new ImageCell
            {
                Text = "Image Cell",
                Detail = "With Detail Text",
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
                Root = new TableRoot("TableView Title")
                {
                    new TableSection("Table Section")
                    {
                        new TextCell
                        {
                            Text = "Text Cell",
                            Detail = "With Detail Text",
                        },
                        imageCell,
                        new SwitchCell
                        {
                            Text = "Switch Cell"
                        },
                        new EntryCell
                        {
                            Label = "Entry Cell",
                            Placeholder = "Type text here"
                        },
                        new ViewCell
                        {
                            View = new Label
                            {
                                Text = "A View Cell can be anything you want!"
                            }
                        }
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
