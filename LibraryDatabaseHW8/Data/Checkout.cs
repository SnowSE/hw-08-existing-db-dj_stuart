using System;
using System.Collections.Generic;

namespace LibraryDatabaseHW8.Data
{
    public partial class Checkout
    {
        public Checkout()
        {
            CheckoutItem = new HashSet<CheckoutItem>();
        }

        public int Id { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int StaffId { get; set; }
        public int StudentId { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<CheckoutItem> CheckoutItem { get; set; }
    }
}
