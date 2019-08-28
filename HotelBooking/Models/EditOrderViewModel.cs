using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Models
{
    public class EditOrderViewModel
    {
        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Кількість місць")]
        public int NumberOfBeds { get; set; }
        [Required]
        [Display(Name = "Категорія")]
        public string Category { get; set; }
        [Required]
        [Display(Name = "Час прибуття")]
        public DateTime ArrivalTime { get; set; }
        [Required]
        [Display(Name = "Час відбуття")]
        public DateTime DepartureTime { get; set; }
        [Required]
        [Display(Name = "ID кімнати")]
        public string RoomId { get; set; }
        public SelectList Costs { get; set; }
        //public List<SelectListItem> Costs { get; set; }
    }
}