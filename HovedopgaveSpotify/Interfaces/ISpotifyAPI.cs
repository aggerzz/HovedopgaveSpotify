using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HovedopgaveSpotify.Interfaces
{
    public interface ISpotifyApi
    {
        string Token { get; set; }

        T GetSpotifyType<T>(string url);
    }
}