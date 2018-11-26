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
                PlaybackContext context = _spotify.GetPlayingTrack();
                if (context.Item != null)
                    ViewBag.song = context.Item.Artists[0].Name + " - " +context.Item.Name;

                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }
        public ActionResult Play(string access_token, string spotifyUri)
        {
            
            ErrorResponse error = _spotify.ResumePlayback(uris: new List<string> { spotifyUri });
            return Json(spotifyUri, JsonRequestBehavior.AllowGet);
        }
        public void Transfer(string access_token, string deviceID)
        {

            ErrorResponse error = _spotify.TransferPlayback(deviceID, true);

        }//ready to see some masterpiece?yeah
        [HttpGet]
        public ActionResult Search(string access_token, string searchString) {
            //TODO SearchQuery i stedet for string "Eminem"
            SearchItem item = _spotify.SearchItems(searchString, SearchType.Track);
            List<SpotifyAPI.Web.Models.FullTrack> searchResult = new List<SpotifyAPI.Web.Models.FullTrack>();

            if (!string.IsNullOrEmpty(searchString)) {
            searchResult = item.Tracks.Items.ToList();

            }
            return PartialView("searchResult", searchResult);
        }
    }
}