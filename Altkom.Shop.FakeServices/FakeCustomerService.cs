using Altkom.Shop.IServices;
using Altkom.Shop.Models;
using Altkom.Shop.Models.SearchCriteria;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.Shop.FakeServices
{
    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers; //przypisanie tylko w konstruktorze jest mozliwe - tak przypominam bo juz zapomniales ziomek 

        public FakeCustomerService(Faker<Customer> faker)
        {
            this.customers = faker.Generate(100);
        }

        public void Add(Customer customer)
        {
            int id = customers.Max(c => c.Id);
            customer.Id = ++id;
            customers.Add(customer);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public Customer Get(int id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }


        //todo !!! tak sie robi zapytania rozsadnie, zrob fakera do adresow i stestuj
        public IEnumerable<Customer> Get(CustomerSearchCriteria criteria)
        {
            var query = customers.AsQueryable();
            if (!string.IsNullOrEmpty(criteria.City))
            {
                query = query.Where(c => c.ShipAdress.City == criteria.City);

            }

            if (!string.IsNullOrEmpty(criteria.Street))
            {
                query = query.Where(c => c.ShipAdress.Street == criteria.Street);

            }

            if (!string.IsNullOrEmpty(criteria.Zipcode))
            {
                query = query.Where(c => c.ShipAdress.ZipCode == criteria.Zipcode);

            }
            return query.ToList();
        }

        public void Remove(int id)
        {
            customers.Remove(Get(id));
        }

        public void Update(Customer customer)
        {
            var existingCustomer = Get(customer.Id);
            existingCustomer.IsRemoved = true;
        }
    }
}
