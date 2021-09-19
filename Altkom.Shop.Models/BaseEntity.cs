using System;

namespace Altkom.Shop.Models
{
    public abstract class BaseEntity : Base
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public BaseEntity()
        {
            CreatedOn = DateTime.Now;
        }
    }

}
