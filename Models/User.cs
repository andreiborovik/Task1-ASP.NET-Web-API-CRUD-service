using System;
namespace Task1.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
