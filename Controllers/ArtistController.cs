using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace MusicApi.Controllers {

    
    public class ArtistController : Controller {

        private List<Artist> allArtists;

        public ArtistController() {
            allArtists = JsonToFile<Artist>.ReadJson();
        }

        //This method is shown to the user navigating to the default API domain name
        //It just display some basic information on how this API functions
        [Route("")]
        [HttpGet]
        public string Index() {
            //String describing the API functionality
            string instructions = "Welcome to the Music API~~\n========================\n";
            instructions += "    Use the route /artists/ to get artist info.\n";
            instructions += "    Params:\n";
            instructions += "       *Name=?\n";
            instructions += "       *RealName=?\n";
            instructions += "       *Hometown=?\n";
            instructions += "       *GroupId=?\n\n";
            instructions += "    Use the route /groups/ to get group info.\n";
            instructions += "    Params:\n";
            instructions += "       *Name=?\n";
            instructions += "       *GroupId=?\n";
            instructions += "       *ListArtists=?(true/false)\n";
            return instructions;
        }

        [Route("artists")]
        [HttpGet]
        public JsonResult Artists() {
            return Json(allArtists);
        }

        [Route("artists/name/{name}")]
        [HttpGet]
        public JsonResult ArtistByName(string name) {
            var foundArtists = allArtists.Where(artist => artist.ArtistName.Contains(name)).ToList();
            return Json(foundArtists);
        }

        [Route("artists/realname/{realname}")]
        [HttpGet]
        public JsonResult ArtistByRealName(string realname) {
            var foundArtists = allArtists.Where(artist => artist.RealName.Contains(realname)).ToList();
            return Json(foundArtists);
        }
        
        [Route("artists/hometown/{town}")]
        [HttpGet]
        public JsonResult ArtistByHometown(string town){
            var foundArtists = allArtists.Where(artist => artist.Hometown == town).ToList();
            return Json(foundArtists);
        }

        [Route("artists/groupid/{id}")]
        [HttpGet]
        public JsonResult ArtistByGroupId(int id) {
            var foundArtists = allArtists.Where(artist => artist.GroupId == id).ToList();
            return Json(foundArtists);
        }

    }
}