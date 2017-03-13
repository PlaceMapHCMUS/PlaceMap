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
    class ImagePlace
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "idPlace")]
        public String idPlace { get; set; }

        [JsonProperty(PropertyName = "imagePlace")]
        public String imagePlace { get; set; }
    }
}