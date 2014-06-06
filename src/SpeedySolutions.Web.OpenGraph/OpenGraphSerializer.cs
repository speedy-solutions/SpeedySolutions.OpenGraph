using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SpeedySolutions.Web.OpenGraph
{
	internal class OpenGraphSerializer
	{
		private OpenGraphSerializer()
		{ }

		private readonly StringBuilder _sb = new StringBuilder();

		internal static string Serialize(OpenGraph og)
		{
			var serializer = new OpenGraphSerializer();

			serializer.GetTags(og);

			return serializer._sb.ToString();
		}

		private void GetTags(object obj, string prefix = null)
		{
			var properties = obj.GetType().GetRuntimeProperties();

			foreach(var prop in properties)
			{
				foreach(var attr in prop.GetCustomAttributes<OgPropertyAttribute>())
				{
					BuildTagForAttribute(obj, prefix, prop, attr);
				}
			}
		}

		private void BuildTagForAttribute(object obj, string prefix, PropertyInfo prop, OgPropertyAttribute attr)
		{
			var value = prop.GetValue(obj);
			var ogProperty = attr.ToString(prefix);
			if((value == null || value is string && string.IsNullOrWhiteSpace((string)value)))
			{
				if(attr.Required)
					throw new ArgumentNullException("value", string.Format("The property {0} is required", attr.ToString()));

			}
			else if(IsSimpleValue(value))
			{
				AddTag(ogProperty, value);
			}
			else if(value is Enum)
			{
				SerializeEnum((Enum)value, ogProperty);
			}
			else if(value is IEnumerable)
			{
				SerializeCollection(ogProperty, value);
			}
			else
			{
				GetTags(value, ogProperty);
			}
		}

		private void SerializeEnum(Enum value, string ogProperty)
		{
			AddTag(ogProperty, value.ToString().ToLowerInvariant());
		}

		private void SerializeCollection(string ogProperty, object value)
		{
			foreach(var item in (IEnumerable)value)
			{
				if(IsSimpleValue(item))
					AddTag(ogProperty, item);
				else if(item is Enum)
					SerializeEnum((Enum)item, ogProperty);
				GetTags(item, ogProperty);
			}
		}

		private static bool IsSimpleValue(object value)
		{
			return value is string || value is Uri || value is int;
		}

		private void AddTag(string property, object value)
		{
			_sb.AppendLine(string.Format(@"<meta property=""{0}"" content=""{1}"">", property, HttpUtility.HtmlEncode(value.ToString())));
		}
	}
}
