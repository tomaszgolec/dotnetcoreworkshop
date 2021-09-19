namespace Altkom.Shop.Models
{
    public abstract class Item : BaseEntity
    {
        public string Name { get; set; }
        public string UnitPrice { get; set; }
    }
}
