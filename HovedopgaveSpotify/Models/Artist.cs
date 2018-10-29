using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HovedopgaveSpotify.Models
{
    public class Artist
    {
        [JsonProperty("name")]
        public String Name { get; set; }
    }
}