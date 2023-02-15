using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
    public class Room_Availability
    { 
   
        public int room_id { get; set; }
         
        public string user_id { get; set; }
        public DateTime booking_date { get; set; }
    }
}
