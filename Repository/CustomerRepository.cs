using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace WebApplication1.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string? _connectString;
        public CustomerRepository(IConfiguration configuration)
        {
            _connectString = configuration.GetConnectionString("DBConnection");
        }
        //取得全部資料
        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_connectString))
            {
                return await conn.QueryAsync<Customers>("SELECT * FROM Customers");
            }
        }
        //取得資料筆數
        public async Task<int> GetCount()
        {
            using (var conn = new SqlConnection(_connectString))
            {
                return await conn.QuerySingleAsync<int>("SELECT COUNT(*) FROM Customers");
            }
        }
        //處理分頁資料
        public async Task<IEnumerable<Customers>> GetPageCustomer(int offset,int pageSize)
        {
            using (var conn = new SqlConnection(_connectString))
            {
                string sql = @"SELECT * 
                               FROM Customers
                               ORDER BY CustomerID
                               OFFSET @Offset ROWS
                               FETCH NEXT @PageSize ROWS ONLY";
                var parameters = new { offset, pageSize };

                return await conn.QueryAsync<Customers>(sql, parameters);
            }
        }
        //public async Task<IEnumerable<Customers>> SearchCustomer(int offset, int pageSize,string keyword)
        //{
        //    using (var conn = new SqlConnection(_connectString))
        //    {
        //        string sql = @"SELECT * 
        //                       FROM Customers
        //                       ORDER BY CustomerID
        //                       OFFSET @Offset ROWS
        //                       FETCH NEXT @PageSize ROWS ONLY";
        //        var parameters = new { offset, pageSize };

        //        return await conn.QueryAsync<Customers>(sql, parameters);
        //    }
        //}
        public async Task<IEnumerable<Customers>> GetCustomerByIdAsync(int id)
        {
            using (var conn = new SqlConnection(_connectString))
            {
                return await conn.QueryAsync<Customers>("SELECT * FROM Members WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
