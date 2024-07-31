using FootScout.WebAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Models.DTOs
{
    public class ClubHistoryCreateDTO
    {
        public string Position { get; set; }
        public string ClubName { get; set; }
        public string League { get; set; }
        public string Region { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Achievements Achievements { get; set; }
        public string UserId { get; set; }

    }
}