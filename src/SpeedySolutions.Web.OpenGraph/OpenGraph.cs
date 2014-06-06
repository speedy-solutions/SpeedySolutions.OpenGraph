using System;
using System.Text;
using System.Reflection;
using System.Web;
using System.Linq;

namespace SpeedySolutions.Web.OpenGraph
{
	public class OpenGraph(string title = null, Uri url = null, OgImage image = null, string type = "website") : IHtmlString
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
		public OgImage Image
		{ get; set; }
		= image;

		public override string ToString()
		{
			var sb = new StringBuilder();

			GetTags(this, sb);

			return sb.ToString();
		}

		private static void GetTags(object obj, StringBuilder sb, string prefix = null)
		{
			var properties = obj.GetType().GetRuntimeProperties();

			foreach(var prop in properties)
			{
				foreach(var attr in prop.GetCustomAttributes<OgPropertyAttribute>())
				{
					BuildTagForAttribute(obj, sb, prefix, prop, attr);
				}
			}
		}

		private static void BuildTagForAttribute(object obj, StringBuilder sb, string prefix, PropertyInfo prop, OgPropertyAttribute attr)
		{
			var value = prop.GetValue(obj);
			var ogProperty = attr.ToString(prefix);
			var isString = value is string;
			if(attr.Required && (value == null || isString && string.IsNullOrWhiteSpace((string)value)))
			{
				throw new ArgumentNullException("value", string.Format("The property {0} is required", attr.ToString()));
			}
			else if(isString || value is Uri || value is int)
			{
				sb.AppendLine(string.Format(@"<meta property=""{0}"" content=""{1}"">", ogProperty, value));
			}
			else
			{
				GetTags(value, sb, ogProperty);
			}
		}

		public string ToHtmlString()
		{
			return ToString();
		}
	}
}
