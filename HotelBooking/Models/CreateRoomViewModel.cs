using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class CreateRoomViewModel
    {
        [Range(1,50,ErrorMessage = "Кількість місць повина бути в проміжку від 1 до 50")]
        [Required(ErrorMessage ="Вкажіть кількість місць")]
        [Display(Name = "Кількість місць")]
        public int NumberOfBeds { get; set; }
        [Required(ErrorMessage ="Вкажіть категорію")]
        [Display(Name = "Категорія")]
        public string Category { get; set; }
        [Range(1,200000,ErrorMessage = "Ціна повинна бути в проміжку від 1 до 200000")]
        [Required(ErrorMessage ="Вкажіть ціну")]
        [Display(Name = "Ціна")]
        public decimal Cost { get; set; }
    }
}