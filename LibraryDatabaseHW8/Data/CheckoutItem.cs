using System;
using System.Collections.Generic;

namespace LibraryDatabaseHW8.Data
{
    public partial class CheckoutItem
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? CheckoutId { get; set; }
        public DateTime? Returned { get; set; }

        public virtual Book Book { get; set; }
        public virtual Checkout Checkout { get; set; }
    }
}
