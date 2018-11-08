using HovedopgaveSpotify.Interfaces;
using HovedopgaveSpotify.Models;
using HovedopgaveSpotify.Services;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HovedopgaveSpotify.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpotifyAuthViewModel _spotifyAuthViewModel;
        private readonly ISpotifyApi _spotifyApi;
        private static SpotifyWebAPI _spotify;

        public HomeController(SpotifyAuthViewModel spotifyAuthViewModel, ISpotifyApi spotifyApi)
        {            
            _spotifyAuthViewModel = spotifyAuthViewModel;
            _spotifyApi = spotifyApi;
        }

        public ActionResult Index()
        {
            ViewBag.AuthUri = _spotifyAuthViewModel.GetAuthUri();

            return View();
        }

        public ActionResult Spotify(string access_token, string error, string searchString)
        {
            if (error != null || error == "access_denied")
                return View("Error");

            if (string.IsNullOrEmpty(access_token))
                return View();

            try
            {
                _spotifyApi.Token = access_token;
                SpotifyService spotifyService = new SpotifyService(_spotifyApi);
                //Get user_id and user displayName
                SpotifyUser spotifyUser = spotifyService.GetUserProfile();
                ViewBag.UserName = spotifyUser.DisplayName;

                _spotify = new SpotifyWebAPI()
                {
                    //TODO Get token from session
                    AccessToken = access_token,
                    TokenType = "Bearer"
                };
                //TODO SearchQuery i stedet for string "Eminem"
                SearchItem item = _spotify.SearchItems("Eminem", SearchType.Track);
                List<SpotifyAPI.Web.Models.FullTrack> TrackList = item.Tracks.Items.ToList();
                return View(TrackList);
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }
        public ActionResult Play(string access_token, string spotifyUri)
        {
            
            ErrorResponse error = _spotify.ResumePlayback(uris: new List<string> { spotifyUri });
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }
    }
}