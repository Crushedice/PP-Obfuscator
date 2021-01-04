using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using R = Newtonsoft.Json.Required;
using N = Newtonsoft.Json.NullValueHandling;

namespace KeikoObfuscator
{
	

	public partial class Opts
	{
		[J("SkipPublicMethods", NullValueHandling = N.Ignore)] public bool SkipPublicMethods { get; set; }
		[J("SkipPrivateMethods", NullValueHandling = N.Ignore)] public bool SkipPrivateMethods { get; set; }
		[J("SkipPublicFields", NullValueHandling = N.Ignore)] public bool SkipPublicFields { get; set; }
	}

	public partial class Opts
	{
		public static Opts FromJson(string json) => JsonConvert.DeserializeObject<Opts>(json, KeikoObfuscator.Converter.Settings);
	}

	public static class Serialize
	{
		public static string ToJson(this Opts self) => JsonConvert.SerializeObject(self, KeikoObfuscator.Converter.Settings);
	}

	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}
}
