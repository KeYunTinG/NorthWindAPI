using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customers>> GetAllCustomersAsync();
        Task<IEnumerable<Customers>> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customers>> GetPageCustomer(int pageNumber,int pageSize);
        Task<int> GetCount();
    }
}
