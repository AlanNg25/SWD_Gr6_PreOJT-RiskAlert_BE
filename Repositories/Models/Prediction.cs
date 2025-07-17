using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Models
{
    public class Prediction
    {
        [Key]
        public Guid PredictionID { get; set; }
        public Guid StudentID { get; set; }
        public decimal Probability { get; set; }
        public DateTime PredictionDate { get; set; }//
        public string Details { get; set; }
        public bool IsDeleted { get; set; }

        public virtual User Student { get; set; }
    }
}