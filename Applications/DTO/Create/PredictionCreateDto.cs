using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Create
{
    public class PredictionCreateDto
    {
        public Guid StudentID { get; set; }
        public decimal Probability { get; set; }
        public DateTime PredictionDate { get; set; }
        public string Details { get; set; }
    }
}
