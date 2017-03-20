using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace PlaceMap
{
    class Comment
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "idCustomer")]
        public String idCustomer { get; set; }

        [JsonProperty(PropertyName = "idPlace")]
        public String idPlace { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public String comment { get; set; }

        [JsonProperty(PropertyName = "dateComment")]
        public String dateComment { get; set; }

        [JsonProperty(PropertyName = "rate")]
        public int rate { get; set; }

        [JsonProperty(PropertyName = "numExactly")]
        public String numExactly { get; set; }
    }
}