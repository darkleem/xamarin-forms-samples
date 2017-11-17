using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WorkingWithImages
{
	// You exclude the 'Extension' suffix when using in Xaml markup
	[Preserve(AllMembers = true)]
	[ContentProperty ("Source")]
	public class ImageResourceExtension : IMarkupExtension
	{
		public string Source { get; set; }

		public object ProvideValue (IServiceProvider serviceProvider)
		{
			if (Source == null)
				return null;

            ImageSource imageSource = null;
            // Do your translation lookup here, using whatever method you require
            // 'try-catch' clause is used to handle unsupported method on Tizen platform.
            try
            {
                imageSource = ImageSource.FromResource(Source);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("ImageSource.FromResource() is currently not supported on Tizen platform.");
            }

			return imageSource;
		}
	}
}

