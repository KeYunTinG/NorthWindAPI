using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customers>> GetAllAsync();
        Task<IEnumerable<Customers>> GetCustomerByIdAsync(int id);
        //Customer總數
        Task<int> GetCount();
        //資料分頁顯示
        Task<IEnumerable<Customers>> GetPageCustomer(int offset, int pageSize);
    }
}
