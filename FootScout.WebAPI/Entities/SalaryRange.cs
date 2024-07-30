﻿using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class SalaryRange
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Min { get; set; }
        [Required]
        public int Max { get; set; }
    }
}