using HotelBooking.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class CreateOrderViewModel
    {
        [Range(1, 50, ErrorMessage = "Кількість місць повина бути в проміжку від 1 до 50")]
        [Required(ErrorMessage = "Вкажіть кількість місць")]
        [Display(Name = "Кількість місць")]
        public int NumberOfBeds { get; set; }
        [Required(ErrorMessage ="Вкажіть категорію")]
        [Display(Name = "Категорія")]
        public string Category { get; set; }
        [Required(ErrorMessage ="Вкажіть час прибуття")]
        [Display(Name = "Час прибуття")]
        public DateTime ArrivalTime { get; set; }
        [Required(ErrorMessage ="Вкажіть час відбуття")]
        [Display(Name = "Час відбуття")]
        public DateTime DepartureTime { get; set; }

    }
}