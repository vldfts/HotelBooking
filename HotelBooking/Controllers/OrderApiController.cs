using AutoMapper;
using HotelBooking.Entities;
using HotelBooking.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HotelBooking.Controllers
{
    [Authorize]
    [RoutePrefix("api/orders")]
    public class OrderApiController : ApiController
    {
        private ApplicationUserManager UserManager { get; set; }
        private ApplicationDbContext context { get; set; } // спробувати написати на ninject
        public OrderApiController()
        {
            context = new ApplicationDbContext();
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

        }

        //запит на отримання списку моїх ордерів 
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetOrders()
        {
            var r = RequestContext.Principal.Identity.Name;
            //var orders = context.Orders;
            var user = RequestContext.Principal.Identity.GetUserId();
            var config = new MapperConfiguration(x => x.CreateMap<Order, OrderListViewModel>()).CreateMapper();
            var orders = config.Map<IQueryable<Order>, List<OrderListViewModel>>(context.Orders.Where(x => x.User.Id == user));
            if (orders != null)
            {
                return Ok(orders);
            }
            return BadRequest();
        }

        //запит на створення ордеру 
        [Route("create")]
        [HttpPost]
        public IHttpActionResult CreateOrder([FromBody]CreateOrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(RequestContext.Principal.Identity.GetUserId());
                var config = new MapperConfiguration(x => x.CreateMap<CreateOrderViewModel, Order>()).CreateMapper();
                var newOrder = config.Map<CreateOrderViewModel, Order>(order);
                newOrder.User = user;
                context.Orders.Add(newOrder);
                context.Users.Find(user.Id).Orders.Add(newOrder);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //запит на видалення ордеру 
        [Route("delete/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {
            var ord = context.Orders.Find(id);
            if (ord != null)
            {
                context.Orders.Remove(ord);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
