using System;
using System.Text;
using System.Reflection;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace SpeedySolutions.Web.OpenGraph
{
	public class OpenGraph(string title = null, Uri url = null, OgFileWithSize image = null, string type = "website") : IHtmlString
	{
		[OgProperty("title", required: true)]
		public string Title
		{ get; set; }
		= title;

		[OgProperty("type", required: true)]
		public string Type
		{ get; }
		= type;

		[OgProperty("url", required: true)]
		public Uri Url
		{ get; set; }
		= url;

		[OgProperty("image", required: true)]
		public ICollection<OgFileWithSize> Images
		{ get; }
		= image == null ?
			new List<OgFileWithSize>() :
			new List<OgFileWithSize> { image };


		[OgProperty("audio")]
		public OgFile Audio
		{ get; set; }

		[OgProperty("description")]
		public string Description
		{ get; set; }

		[OgProperty("determiner")]
		public Determiner? Determiner
		{ get; set; }

		[OgProperty("locale")]
		public string Locale
		{ get; set; }

		[OgProperty("locale:alternate")]
		public HashSet<string> AlternateLocale
		{ get; }
		= new HashSet<string>();

		[OgProperty("site_name")]
		public string SiteName
		{ get; set; }

		[OgProperty("video")]
		public OgFileWithSize Video
		{ get; set; }

		public override string ToString()
		{
			return OpenGraphSerializer.Serialize(this);
		}

		public string ToHtmlString()
		{
			return ToString();
		}
	}
}
