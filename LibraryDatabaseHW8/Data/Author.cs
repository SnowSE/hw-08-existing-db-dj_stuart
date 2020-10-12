using System;
using System.Collections.Generic;

namespace LibraryDatabaseHW8.Data
{
    public partial class Author
    {
        public Author()
        {
            Book = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string AuthorName { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
