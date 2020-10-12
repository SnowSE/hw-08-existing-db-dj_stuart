using System;
using System.Collections.Generic;

namespace LibraryDatabaseHW8.Data
{
    public partial class LibraryCard
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
