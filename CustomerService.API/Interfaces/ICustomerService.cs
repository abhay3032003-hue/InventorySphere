using CustomerService.API.Models;

namespace CustomerService.API.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomers();

    Task<Customer?> GetCustomerById(int id);

    Task<Customer> CreateCustomer(Customer customer);

    Task UpdateCustomer(Customer customer);

    Task DeleteCustomer(int id);
}