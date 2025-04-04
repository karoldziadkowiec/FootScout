﻿using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class OfferStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string StatusName { get; set; }
    }
}