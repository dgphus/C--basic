using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelA.DTO.Request;
using ModelA.DTO.Response;
using Repository.Entity;
using Repository.Repository;
using Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace Service.Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(string Token, LoginResponse loginResponse)> AuthorizeUser(LoginRequest loginRequest)
        {
            var member = _unitOfWork.CustomerRepository
                .Get(filter: a => a.EmailAddress == loginRequest.EmailAddress
                && a.Password == loginRequest.Password && a.CustomerStatus == 1).FirstOrDefault();
            if (member != null)
            {
                string token = GenerateToken(member);
                var adminResponse = _mapper.Map<LoginResponse>(member);
                return (token, adminResponse);
            }

            var adminEmail = _configuration["AdminUser:EmailAddress"];
            var adminPassword = _configuration["AdminUser:Password"];

            if (loginRequest.EmailAddress == adminEmail && loginRequest.Password == adminPassword)
            {
                var defaultAdminResponse = _mapper.Map<LoginResponse>(member);

                string token = GenerateDefaultToken();

                return (token, defaultAdminResponse);
            }

            
            return (null, null);
        }

        private string GenerateDefaultToken()
        {
            var claims = new List<Claim>
    {
        new Claim("EmailAddress", _configuration["AdminUser:EmailAddress"]),
        new Claim("Role", _configuration["AdminUser:Role"]),
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateToken(Customer info)
        {
            List<Claim> claims = new List<Claim>()
        {
                new Claim(ClaimTypes.NameIdentifier, info.CustomerId.ToString()),
            new Claim("EmailAddress", info.EmailAddress),
        };

            var role = _configuration["DefaultUserRole"];
            claims.Add(new Claim("Role", role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public IEnumerable<CustomerResponse> GetAllCustomers()
        {
            var listCustomers = _unitOfWork.CustomerRepository.Get(includeProperties: "BookingReservations").ToList();
            var customerResponses = _mapper.Map<IEnumerable<CustomerResponse>>(listCustomers);
            return customerResponses;
        }

        public async Task<CustomerResponse> GetCustomerById(int id)
        {
            try
            {
                var customer = _unitOfWork.CustomerRepository.Get(
                    filter: a => a.CustomerId == id, includeProperties: "BookingReservations").FirstOrDefault();

                if (customer == null)
                {
                    throw new Exception("customer not found");
                }

                var adminResponse = _mapper.Map<CustomerResponse>(customer);
                return adminResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomerResponse> CreateCustomer(CustomerRequest customerRequest)
        {
            try
            {
                bool emailAddressExists = _unitOfWork.CustomerRepository.Exists(a => a.EmailAddress == customerRequest.EmailAddress);

                if (emailAddressExists)
                {
                    return new CustomerResponse
                    {
                        EmailAddress = "EmailAddress already exists.",
                    };
                }

                var customers = _mapper.Map<Customer>(customerRequest);

                customers.CustomerStatus = 1;

                _unitOfWork.CustomerRepository.Insert(customers);
                _unitOfWork.Save();

                var customerResponse = _mapper.Map<CustomerResponse>(customers);
                return customerResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomerResponse> UpdateCustomer(int id, CustomerRequest customerRequest)
        {
            try
            {
                var existingCustomer = _unitOfWork.CustomerRepository.GetByID(id);

                if (existingCustomer == null || existingCustomer.CustomerStatus == 0)
                {
                    throw new Exception("Customer not found.");
                }

                _mapper.Map(customerRequest, existingCustomer);

                _unitOfWork.CustomerRepository.Update(existingCustomer);
                _unitOfWork.Save();

                var customerResponse = _mapper.Map<CustomerResponse>(existingCustomer);

                return customerResponse;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                var customer = _unitOfWork.CustomerRepository.GetByID(id);
                if (customer == null || customer.CustomerStatus == 0)
                {
                    throw new Exception("customer not found.");
                }

                customer.CustomerStatus = 0;
                _unitOfWork.CustomerRepository.Update(customer);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CustomerResponse> SearchCustomers(string keyword)
        {
            var customers = _unitOfWork.CustomerRepository.Get(filter: c => c.EmailAddress.Contains(keyword));
            var customerResponses = _mapper.Map<IEnumerable<CustomerResponse>>(customers);
            return customerResponses;
        }
    }
}
