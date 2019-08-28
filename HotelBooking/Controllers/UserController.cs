using HotelBooking.Entities;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using HotelBooking.Models;
using AutoMapper;

namespace HotelBooking.Controllers
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private ApplicationDbContext context { get; set; } // спробувати написати на ninject
        private ApplicationUserManager UserManager { get; set; }
        public UserController()
        {
            context = new ApplicationDbContext();
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
        }

        public ActionResult OrderList()
        {
            ////var user= HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId();
            var user = UserManager.FindById(User.Identity.GetUserId());
            var config = new MapperConfiguration(x => x.CreateMap<Order, OrderListViewModel>()).CreateMapper();
            var orders = config.Map<IQueryable<Order>, List<OrderListViewModel>>(context.Orders.Where(x=>x.User.Id==user.Id));

            return View(orders);
        }
        [HttpGet]
        public ActionResult CreateOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateOrder(CreateOrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                var user= UserManager.FindById(User.Identity.GetUserId()); 
                var config = new MapperConfiguration(x => x.CreateMap<CreateOrderViewModel, Order>()).CreateMapper();
                var newOrder = config.Map<CreateOrderViewModel, Order>(order);
                newOrder.User = user;
                context.Orders.Add(newOrder);
                context.Users.Find(user.Id).Orders.Add(newOrder);
                context.SaveChanges();
                TempData["color"] = "green";
                TempData["message"] = string.Format(" Зомовлення для кімнати створено");
                return RedirectToAction("OrderList");
            }
            return View();
        }
        public ActionResult DeleteOrder(int id)
        {
            var ord = context.Orders.Find(id);
            if (ord != null)
            {
                context.Orders.Remove(ord);
                context.SaveChanges();
                TempData["message"] = string.Format(" Зомовлення  \"{0}\" видалено", ord.Id);

            }
            return RedirectToAction("OrderList");
        }
        public ActionResult ResultList()
        {
            var userId = User.Identity.GetUserId();
            var rooms = context.Rooms.Include("User").Where(x=>x.User.Id==userId);
            var config = new MapperConfiguration(x => x.CreateMap<Room, RoomListViewModel>()).CreateMapper();
            var resultRooms = config.Map<IQueryable<Room>, List<RoomListViewModel>>(rooms);
            return View(resultRooms);
        }
        public ActionResult CancelRoom(int id)
        {
            var result = context.Rooms.Include("User");
            result.Where(x => x.Id == id).FirstOrDefault().User=null;
            context.SaveChanges();
            TempData["message"] = string.Format(" Кімнату \"{0}\" очищено", id);
            return RedirectToAction("OrderList");
        }
    }
}