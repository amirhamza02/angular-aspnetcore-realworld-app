﻿using Flone.Data.Models.Constant;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flone.Data.Models.Listing
{
    public class Listing :BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid BrandId { get; set; }
        public virtual Brand Brands { get; set; }
        public Guid MarketId { get; set; }
        public virtual Markets Markets { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsFreeShiping { get; set; } = false;
        public WeightUnits WeightUnit { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        

    }
}
