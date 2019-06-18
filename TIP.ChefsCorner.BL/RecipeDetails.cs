

namespace TIP.ChefsCorner.BL
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class RecipeDetails
    {
       
        [JsonProperty("vegetarian")]
        public bool Vegetarian { get; set; }

        [JsonProperty("vegan")]
        public bool Vegan { get; set; }

        [JsonProperty("glutenFree")]
        public bool GlutenFree { get; set; }

        [JsonProperty("dairyFree")]
        public bool DairyFree { get; set; }

        [JsonProperty("veryHealthy")]
        public bool VeryHealthy { get; set; }

        [JsonProperty("cheap")]
        public bool Cheap { get; set; }

        [JsonProperty("veryPopular")]
        public bool VeryPopular { get; set; }

        [JsonProperty("sustainable")]
        public bool Sustainable { get; set; }

        [JsonProperty("weightWatcherSmartPoints")]
        public int WeightWatcherSmartPoints { get; set; }

        [JsonIgnore]
        public string Gaps { get; set; }

        [JsonIgnore]
        public bool LowFodmap { get; set; }

        [JsonProperty("ketogenic")]
        public bool Ketogenic { get; set; }

        [JsonIgnore]
        public bool Whole30 { get; set; }

        [JsonProperty("preparationMinutes")]
        public long PreparationMinutes { get; set; }

        [JsonProperty("cookingMinutes")]
        public long CookingMinutes { get; set; }

        [JsonIgnore]
        public Uri SourceUrl { get; set; }

        [JsonIgnore]
        public Uri SpoonacularSourceUrl { get; set; }

        [JsonProperty("aggregateLikes")]
        public long AggregateLikes { get; set; }

        [JsonIgnore]
        public long SpoonacularScore { get; set; }

        [JsonIgnore]
        public int HealthScore { get; set; }

        [JsonIgnore]
        public object CreditsText { get; set; }

        [JsonIgnore]
        public object SourceName { get; set; }

        [JsonIgnore]
        public double PricePerServing { get; set; }

        [JsonProperty("extendedIngredients")]
        public ExtendedIngredient[] ExtendedIngredients { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("readyInMinutes")]
        public long ReadyInMinutes { get; set; }

        [JsonProperty("servings")]
        public long Servings { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonIgnore]
        public string ImageType { get; set; }

        [JsonIgnore]
        public object[] Cuisines { get; set; }

        [JsonIgnore]
        public string[] DishTypes { get; set; }

        [JsonIgnore]
        public string[] Diets { get; set; }

        [JsonIgnore]
        public object[] Occasions { get; set; }

        [JsonIgnore]
        public string[] WinePairing { get; set; }

        [JsonProperty("instructions")]
        public object Instructions { get; set; }

        [JsonIgnore]
        public object[] AnalyzedInstructions { get; set; }

    }

    public partial class ExtendedIngredient
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("aisle")]
        public string Aisle { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonIgnore]
        public string Consitency { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public string Original { get; set; }

        [JsonProperty("originalString")]
        public string OriginalString { get; set; }

        [JsonProperty("originalName")]
        public string OriginalName { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonIgnore]
        public string[] Meta { get; set; }

        [JsonIgnore]
        public string[] MetaInformation { get; set; }

        [JsonIgnore]
        public Measures Measures { get; set; }
    }

    public partial class Measures
    {
        [JsonProperty("us")]
        public string Us { get; set; }

        [JsonProperty("metric")]
        public string Metric { get; set; }
    }

    public partial class Metric
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("unitShort")]
        public string UnitShort { get; set; }

        [JsonProperty("unitLong")]
        public string UnitLong { get; set; }
    }

   

    public partial class RecipeDetails
    {
        public static List<RecipeDetails> FromJson(string json) => JsonConvert.DeserializeObject<List<RecipeDetails>>(json, TIP.ChefsCorner.BL.RecipeConverter.Settings);
    }

    public static class RecipeSerialize
    {
        public static string ToJson(this RecipeDetails self) => JsonConvert.SerializeObject(self, TIP.ChefsCorner.BL.RecipeConverter.Settings);
    }

    internal static class RecipeConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
           
        };
    }

   

}