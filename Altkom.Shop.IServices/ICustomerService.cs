using Altkom.Shop.Models;
using Altkom.Shop.Models.SearchCriteria;
using System;
using System.Collections.Generic;

namespace Altkom.Shop.IServices
{
    public interface ICustomerService
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        IEnumerable<Customer> Get(CustomerSearchCriteria criteria);
        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(int id);

    }
}
