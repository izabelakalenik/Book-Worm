using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BookWorm.Models;

namespace BookWorm.Models
{
    public enum Payment
    {
        blik, PayU, transfer
    }

    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public Payment Payment { get; set; }
        [NotMapped]
        // public int Loyalty_points { get; set; }
        public List<(Article, int)> OrderedItems { get; set; }

    }
}