using AutoMapper;
using HotelBooking.Entities;
using HotelBooking.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HotelBooking.Controllers
{
    [Authorize] 
    [RoutePrefix("api/rooms")]
    public class RoomApiController : ApiController
    {
        private ApplicationDbContext context { get; set; } // спробувати написати на ninject
        public RoomApiController()
        {
            context = new ApplicationDbContext();

        }
        //запит на отримання моїх кімнат 
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetRooms()
        {
            //return Ok(context.Rooms.Select(x => x));
            var r = RequestContext.Principal.Identity.Name;
            var user = RequestContext.Principal.Identity.GetUserId();
            var rooms = context.Rooms.Where(x => x.User.Id == user);
            var config = new MapperConfiguration(x => x.CreateMap<Room, RoomListViewModel>()).CreateMapper();
            var result = config.Map<IQueryable<Room>, List<RoomListViewModel>>(rooms);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        //запит на відхилення кімнати get
        [Route("cancel/{id:int}")]
        [HttpGet]
        public IHttpActionResult CancelRoom(int id)
        {
            var result = context.Rooms.Include("User").Where(x => x.Id == id).FirstOrDefault();
            if (result!=null)
            {
                result.User = null;
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
