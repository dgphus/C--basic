using ModelA.DTO.Request;
using ModelA.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {
        Task<(string Token, LoginResponse loginResponse)> AuthorizeUser(LoginRequest loginRequest);

        IEnumerable<CustomerResponse> GetAllCustomers();

        Task<CustomerResponse> CreateCustomer(CustomerRequest customerRequest);
        Task<CustomerResponse> UpdateCustomer(int id, CustomerRequest customerRequest);
        Task<bool> DeleteCustomer(int id);
        Task<CustomerResponse> GetCustomerById(int id);

        IEnumerable<CustomerResponse> SearchCustomers(string keyword);
    }
}
