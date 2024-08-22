using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Service
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IEnumerable<Customers>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Customers>> GetCustomerByIdAsync(int id)
        { 
            return await _customerRepository.GetCustomerByIdAsync(id);
        }
        public async Task<IEnumerable<Customers>> GetPageCustomer(int pageNumber,int pageSize)
        {
            // 取得總數
            int count = await _customerRepository.GetCount();
            // 取得總數
            // 取得總頁數
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);
            // 確認頁碼有效
            if (pageNumber < 1 || pageNumber > totalPages)
            {
                throw new ArgumentException("Invalid page number.");
            }
            // 計算偏移量
            int offset = (pageNumber - 1) * pageSize;
            // 執行查詢並傳回結果列表
            return  await _customerRepository.GetPageCustomer(offset, pageSize);
        }
        public async Task<int> GetCount()
        {
            return await _customerRepository.GetCount();
        }
    }
}
