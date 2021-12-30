using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIMaxiTransfersTest.Models
{
    public class Beneficiary
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public int EmployeeNumber { get; set; }
        public string Curp { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public int ParticipationPercentage { get; set; }
    }
}
