using Altkom.Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Shop.IServices
{
    public interface ICustomerClient
    {
        Task YouHaveGotNewCustomer(Customer customer);
        Task Pong(string message);
    }
}
