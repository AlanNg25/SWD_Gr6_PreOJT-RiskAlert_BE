using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Response
{
    public class GradeDetailDto
    {
        public Guid GradeDetailID { get; set; }
        public Guid GradeID { get; set; }
        public string GradeType { get; set; }
        public decimal Score { get; set; }
        public decimal ScoreWeight { get; set; }
        public decimal MinScr { get; set; }
        public bool IsDeleted { get; set; }
    }
}
