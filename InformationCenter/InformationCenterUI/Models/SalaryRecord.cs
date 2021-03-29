using System;

namespace InformationCenterUI.Models
{
    public class SalaryRecord
    {
        public int Id { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public string Company { get; set; }

        public string Position { get; set; }

        public int Salary { get; set; }

        // Years of experience.
        public double Experience { get; set; }
    }
}
