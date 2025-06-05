using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Models
{
    public class Predictions
    {
        [Key]
        public Guid PredictionID { get; set; }
        public Guid StudentID { get; set; }
        public decimal Probability { get; set; }
        public DateTime PredictionDate { get; set; }
        public string Details { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("StudentID")]
        public virtual Users Student { get; set; }
    }
}