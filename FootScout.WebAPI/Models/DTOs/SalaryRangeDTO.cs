using System.ComponentModel.DataAnnotations;

namespace FootScout.WebAPI.Entities
{
    public class SalaryRangeDTO
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }
}