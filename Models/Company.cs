using System.Collections.Generic;
namespace Task1.Models
{
    public class Company : Entity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; } = new();
    }
}
