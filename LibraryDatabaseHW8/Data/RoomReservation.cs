using System;
using System.Collections.Generic;

namespace LibraryDatabaseHW8.Data
{
    public partial class RoomReservation
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int RoomId { get; set; }
        public int StudentId { get; set; }

        public virtual Room Room { get; set; }
        public virtual Student Student { get; set; }
    }
}
