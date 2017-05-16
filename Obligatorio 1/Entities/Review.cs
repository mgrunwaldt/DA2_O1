using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProductId { get; set;}
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
