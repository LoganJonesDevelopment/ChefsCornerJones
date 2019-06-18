using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TIP.ChefsCorner.BL
{

    public partial class IngredientSearch
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        //    [JsonProperty("type")]
        //    public string Type { get; set; }

        //    [JsonProperty("products")]
        //    public Product[] Products { get; set; }

        //    [JsonProperty("offset")]
        //    public long Offset { get; set; }

        //    [JsonProperty("number")]
        //    public long Number { get; set; }

        //    [JsonProperty("totalProducts")]
        //    public long TotalProducts { get; set; }

        //    [JsonProperty("processingTimeMs")]
        //    public long ProcessingTimeMs { get; set; }

        //    [JsonProperty("expires")]
        //    public long Expires { get; set; }
    }

    //public partial class Product
    //{
    //    [JsonProperty("id")]
    //    public long Id { get; set; }

    //    [JsonProperty("title")]
    //    public string Title { get; set; }

    //    [JsonProperty("image")]
    //    public Uri Image { get; set; }

    //    [JsonProperty("imageType")]
    //    public string ImageType { get; set; }
    //}

    public partial class IngredientSearch
    {
      //  public static IngredientSearch FromJson(string json) => JsonConvert.DeserializeObject<IngredientSearch>(json, TIP.ChefsCorner.BL.IngredientConverter.Settings);
        public static IngredientSearch[] FromJson(string json) => JsonConvert.DeserializeObject<IngredientSearch[]>(json, TIP.ChefsCorner.BL.IngredientConverter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this IngredientSearch[] self) => JsonConvert.SerializeObject(self, TIP.ChefsCorner.BL.IngredientConverter.Settings);
    }

    internal static class IngredientConverter
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
