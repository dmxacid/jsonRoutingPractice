using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace MusicApi.Controllers {
    public class GroupController : Controller {
        List<Group> allGroups {get; set;}
        List<Artist> allArtists {get; set;}

        public GroupController() {
            allGroups = JsonToFile<Group>.ReadJson();
            allArtists = JsonToFile<Artist>.ReadJson();
        }

        [Route("groups")]
        [HttpGet]
        public JsonResult Groups() {
            return Json(allGroups);
        }

        [Route("groups/name/{name}")]
        [HttpGet]
        public JsonResult GroupByName(string name, bool listArtists) {
            var foundGroups = allGroups.Where(group => group.GroupName.Contains(name)).ToList();
            if(listArtists == true) {
                foundGroups = foundGroups.GroupJoin(allArtists,
                        group => group.Id,
                        artist => artist.GroupId,
                        (group, artists) => {group.Members = artists.ToList(); return group;}).ToList();
            }
            return Json(foundGroups);
        }

        [Route("groups/id/{id}")]
        [HttpGet]
        public JsonResult GroupById(int id, bool listArtists) {
            var foundGroups = allGroups.Where(group => group.Id == id).ToList();
            if(listArtists == true) {
                foundGroups = foundGroups.GroupJoin(allArtists,
                        group => group.Id,
                        artist => artist.GroupId,
                        (group, artists) => {group.Members = artists.ToList(); return group;}).ToList();
            }
            return Json(foundGroups);
        }
    }
}