using Newtonsoft.Json;
using System;

namespace HovedopgaveSpotify.Models
{
    public class PublicProfile
    {
        [JsonProperty("id")]
        public String Id { get; set; }
    }
}