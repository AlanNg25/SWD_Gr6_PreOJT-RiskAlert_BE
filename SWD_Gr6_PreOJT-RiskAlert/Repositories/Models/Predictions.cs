using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Predictions
    {
        [Key]
        public Guid PredictionID { get; set; }
        public Guid StudentID { get; set; }
        public Guid SubjectID { get; set; }
        public string RiskLevel { get; set; }
        public decimal Probability { get; set; }
        public DateTime PredictionDate { get; set; }
        public string Details { get; set; }
        public bool IsDeleted { get; set; }

        public Users Student { get; set; }
        public Subjects Subject { get; set; }
    }
}