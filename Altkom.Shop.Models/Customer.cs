using System;

namespace Altkom.Shop.Models
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public CustomerType CustomerType { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsRemoved { get; set; }
    }
}
