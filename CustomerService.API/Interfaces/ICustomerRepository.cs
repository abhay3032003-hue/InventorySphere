using CustomerService.API.Models;

namespace CustomerService.API.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();

    Task<Customer?> GetByIdAsync(int id);

    Task<Customer> CreateAsync(Customer customer);

    Task UpdateAsync(Customer customer);

    Task DeleteAsync(Customer customer);
}