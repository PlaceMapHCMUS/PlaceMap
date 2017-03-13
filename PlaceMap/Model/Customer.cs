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
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PlaceMap
{
    class Customer
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "account")]
        public String account { get; set; }
        [JsonProperty(PropertyName = "pass")]
        public String pass { get; set; }
        [JsonProperty(PropertyName = "email")]
        public String email { get; set; }
        [JsonProperty(PropertyName = "avatar")]
        public String avatar { get; set; }

    }
}