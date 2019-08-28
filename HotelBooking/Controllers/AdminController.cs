using AutoMapper;
using HotelBooking.Entities;
using HotelBooking.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext context { get; set; } // спробувати написати на ninject
        public AdminController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Admin
        public ActionResult OrderList()
        {
            var config = new MapperConfiguration(x => x.CreateMap<Order, OrderListViewModel>()).CreateMapper();
            var orders = config.Map<IQueryable<Order>, List<OrderListViewModel>>(context.Orders);
            return View(orders);
        }
        public ActionResult RoomList()
        {
            //context.SaveChanges();
            var config = new MapperConfiguration(x => x.CreateMap<Room, RoomListViewModel>()).CreateMapper();
            var conroom = context.Rooms.Include("User").Where(x => x.User == null);
            var rooms = config.Map<IQueryable<Room>, List<RoomListViewModel>>(conroom);
            return View(rooms);
        }

        public ActionResult CreateRoom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRoom(CreateRoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(x => x.CreateMap<CreateRoomViewModel, Room>()).CreateMapper();
                var newRoom = config.Map<CreateRoomViewModel, Room>(room);
                context.Rooms.Add(newRoom);
                context.SaveChanges();
                TempData["color"] = "green";
                TempData["message"] = string.Format(" Кімнату категорії \"{0}\" створено", newRoom.Category);
                return RedirectToAction("RoomList");
            }
            return View();
        }

        public ActionResult DeleteRoom(int id)
        {
            var rom = context.Rooms.Find(id);
            if (rom != null)
            {
                context.Rooms.Remove(rom);
                context.SaveChanges();
                TempData["message"] = string.Format(" Кімнату категорії \"{0}\" видалено", rom.Category);
            }
            return RedirectToAction("RoomList");
        }
        [HttpGet]
        public ActionResult EditOrder(int id)
        {
            var order = context.Orders.Find(id);
            var config = new MapperConfiguration(x => x.CreateMap<Order, EditOrderViewModel>()).CreateMapper();
            var newRoom = config.Map<Order, EditOrderViewModel>(order);
            var config2 = new MapperConfiguration(x => x.CreateMap<Room, RoomListViewModel>()).CreateMapper();
            var rooms = config2.Map<IQueryable<Room>, List<RoomListViewModel>>(context.Rooms.Include("User").Where(x => x.User == null));
            rooms = rooms.Where(x => x.Category == order.Category).Where(x => x.NumberOfBeds == order.NumberOfBeds).ToList();
            newRoom.Costs = new SelectList(rooms, "Id", "Cost");
            return View(newRoom);
        }
        [HttpPost]
        public ActionResult EditOrder(EditOrderViewModel editOrder)
        {
            var order = context.Orders.Include("User").Where(x => x.Id == editOrder.Id).FirstOrDefault(); ;
            if (editOrder.RoomId == null)
            {
                TempData["message"] = string.Format("Немає підходящих кімнат");
                return RedirectToAction("OrderList");
                //треба додати попередження по відсутність кімнати
            }
            else
            {
                var room = context.Rooms.Find(Convert.ToInt32(editOrder.RoomId));
                room.User = order.User;
                room.ArrivalTime = order.ArrivalTime;
                room.DepartureTime = order.DepartureTime;
                context.Orders.Remove(order);
                TempData["color"] = "green";
                TempData["message"] = string.Format("Замовлення \"{0}\" оброблено", order.Id);
                context.SaveChanges();
                return RedirectToAction("OrderList");
            }
            
        }
        public ActionResult ClearRoomStatus()
        {
            var rooms = context.Rooms.Include("User").Where(x => x.User != null);
            foreach (var item in rooms)
            {
                item.User = null;
            }
            context.SaveChanges();
            return RedirectToAction("RoomList");
        }
        public ActionResult ClearStatus(int id)
        {
            var rooms = context.Rooms.Include("User");
            rooms.Where(x => x.Id == id).FirstOrDefault().User = null;
            context.SaveChanges();
            return RedirectToAction("RoomList");
        }
        public ActionResult ResultListAdmin()
        {
            var config = new MapperConfiguration(x => x.CreateMap<Room, RoomListViewModel>().ForMember("Status", y => y.MapFrom(c => c.User.UserName))).CreateMapper();
            var rooms = config.Map<IQueryable<Room>, List<RoomListViewModel>>(context.Rooms.Include("User").Where(x => x.User != null).Where(y => y.Category != "Кімната відсутня"));
            return View(rooms);
        }
    }
}
