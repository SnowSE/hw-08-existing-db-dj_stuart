using System;
using System.Collections.Generic;

namespace LibraryDatabaseHW8.Data
{
    public partial class Room
    {
        public Room()
        {
            RoomReservation = new HashSet<RoomReservation>();
        }

        public int Id { get; set; }
        public string RoomName { get; set; }
        public int? FloorNumber { get; set; }
        public int? Capacity { get; set; }

        public virtual ICollection<RoomReservation> RoomReservation { get; set; }
    }
}
