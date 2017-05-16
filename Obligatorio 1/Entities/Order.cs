﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public int Status { get; set; }
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public DateTime PayedOn { get; set; }

        public Order()
        {
            this.Id = Guid.NewGuid();
            this.PayedOn = DateTime.Now;
        }

    }
}