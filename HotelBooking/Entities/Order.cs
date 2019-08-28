using System;

namespace HotelBooking.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int NumberOfBeds { get; set; }
        public string Category { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public ApplicationUser User { get; set; }
    }
}