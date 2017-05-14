using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ProductFeature
    {
        public Guid FeatureId { get; set; }
        public Guid ProductId { get; set; }
        public string Value { get; set; }

        [Key]
        public Guid Id { get; set; }

        public ProductFeature()
        {
            this.Id = Guid.NewGuid();
        }

    }
}