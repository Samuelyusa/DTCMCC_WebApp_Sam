using System.ComponentModel;

namespace DTCMCC_WebApp_Sam.Models
{
    public class Employee
    {
        [DisplayName("EmployeeId")]
        public int EmployeeId { get; set; }

        [DisplayName("FirstName")]
        public string FirstName { get; set; }


    }
}
