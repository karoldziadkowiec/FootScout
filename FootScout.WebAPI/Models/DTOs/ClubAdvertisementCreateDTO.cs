﻿using FootScout.WebAPI.Entities;

namespace FootScout.WebAPI.Models.DTOs
{
    public class ClubAdvertisementCreateDTO
    {
        public int PlayerPositionId { get; set; }
        public string ClubName { get; set; }
        public string League { get; set; }
        public string Region { get; set; }
        public SalaryRangeDTO SalaryRangeDTO { get; set; }
        public string ClubMemberId { get; set; }
    }
}