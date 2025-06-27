using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Create
{
    public class GradeDetailCreateDto
    {
        public Guid GradeID { get; set; }
        public string GradeType { get; set; }
        public decimal Score { get; set; }
        public decimal ScoreWeight { get; set; }
        public decimal MinScr { get; set; }
    }
}
