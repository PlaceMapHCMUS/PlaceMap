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
    class Place
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "idCustomer")]
        public String idCustomer { get; set; }

        [JsonProperty(PropertyName = "keyPlace")]
        public String keyPlace { get; set; }

        [JsonProperty(PropertyName = "name")]
        public String name { get; set; }

        [JsonProperty(PropertyName = "addressPlace")]
        public String addressPlace { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public String phone { get; set; }

        [JsonProperty(PropertyName = "website")]
        public String website { get; set; }

        [JsonProperty(PropertyName = "discrip")]
        public String discrip { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public float longitude { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public float latitude { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public String dateCreated { get; set; }

        [JsonProperty(PropertyName = "dateUpdated")]
        public string dateUpdated { get; set; }
        
    }
}