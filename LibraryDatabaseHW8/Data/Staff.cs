using System;
using System.Collections.Generic;

namespace LibraryDatabaseHW8.Data
{
    public partial class Staff
    {
        public Staff()
        {
            Checkout = new HashSet<Checkout>();
        }

        public int Id { get; set; }
        public string StaffName { get; set; }
        public string StaffPassword { get; set; }

        public virtual ICollection<Checkout> Checkout { get; set; }
    }
}
