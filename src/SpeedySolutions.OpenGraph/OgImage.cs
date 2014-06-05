using System;

namespace SpeedySolutions.OpenGraph
{
	public class OgImage
	{
		[OgProperty("", required: true)]
		public Uri Url { get; set; }

		[OgProperty("secure_url")]
		public Uri SecureUrl { get; set; }

		[OgProperty("type")]
		public string Type { get; set; }

		[OgProperty("width")]
		public int? Width { get; set; }

		[OgProperty("height")]
		public int? Height { get; set; }

		public static implicit operator OgImage(Uri url)
		{
			var image = new OgImage
			{
				Url = url
			};

			if (url.Scheme == "https")
				image.SecureUrl = url;

			return image;
		}
	}
}
