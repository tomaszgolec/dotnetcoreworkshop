using Altkom.Shop.Models;
using Bogus; // faker is from here
using System;

namespace Altkom.Shop.Fakers
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker(Faker<Adress> addressFaker)
        {
            StrictMode(true); // bogous domyslnie wrzuci tez wartos dla pol ktore nie majaz zdefiniowanych zasad, ten tryb powoduje ze strict mode podpowie ze czegos zapomnielismy zmapowac
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.CustomerType, f => f.PickRandom<CustomerType>());
            RuleFor(p => p.Gender, f => (Gender) f.Person.Gender);
            RuleFor(p => p.DateOfBirth, f => f.Date.Past(50));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
            RuleFor(p => p.CreatedOn, f => f.Date.Past());
            RuleFor(p => p.ShipAdress, f => addressFaker.Generate());
        }
    }

    public class AddressFaker : Faker<Adress>
    {
        public AddressFaker()
        {
            RuleFor(p => p.City, f => f.Address.City());
            RuleFor(p => p.Street, f => f.Address.StreetName());
            RuleFor(p => p.ZipCode, f => f.Address.ZipCode());
        }
    }
}
