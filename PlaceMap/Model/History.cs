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
    class History
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "idCustomer")]
        public String idCustomer { get; set; }

        [JsonProperty(PropertyName = "nameHistory")]
        public String nameHistory { get; set; }
    }
}