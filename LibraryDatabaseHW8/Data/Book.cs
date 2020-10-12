using System;
using System.Collections.Generic;

namespace LibraryDatabaseHW8.Data
{
    public partial class Book
    {
        public Book()
        {
            CheckoutItem = new HashSet<CheckoutItem>();
        }

        public int Id { get; set; }
        public string BookTitle { get; set; }
        public int? PublishedYear { get; set; }
        public string ShelfPosition { get; set; }
        public int? SpecialCollectionId { get; set; }
        public int? AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual SpecialCollection SpecialCollection { get; set; }
        public virtual ICollection<CheckoutItem> CheckoutItem { get; set; }
    }
}
