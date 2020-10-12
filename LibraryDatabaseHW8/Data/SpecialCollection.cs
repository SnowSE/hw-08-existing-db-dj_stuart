using System;
using System.Collections.Generic;

namespace LibraryDatabaseHW8.Data
{
    public partial class SpecialCollection
    {
        public SpecialCollection()
        {
            Book = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string CollectionName { get; set; }
        public string Restrictions { get; set; }
        public string ContactInstructions { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
