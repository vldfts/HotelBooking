using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class RoomListViewModel
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
        [Display(Name = "Ціна")]
        public decimal Cost { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public string Status { get; set; }
    }
}