using System;
using System.Collections.Generic;

namespace LibraryDatabaseHW8.Data
{
    public partial class Student
    {
        public Student()
        {
            Checkout = new HashSet<Checkout>();
            LibraryCard = new HashSet<LibraryCard>();
            RoomReservation = new HashSet<RoomReservation>();
        }

        public int Id { get; set; }
        public string StudentName { get; set; }
        public string StudentPassword { get; set; }

        public virtual ICollection<Checkout> Checkout { get; set; }
        public virtual ICollection<LibraryCard> LibraryCard { get; set; }
        public virtual ICollection<RoomReservation> RoomReservation { get; set; }
    }
}
