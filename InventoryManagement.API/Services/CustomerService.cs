using InventoryManagement.API.Interfaces;
using InventoryManagement.API.Models;

namespace InventoryManagement.API.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Customer> CreateCustomer(Customer customer)
    {
        return await _repository.CreateAsync(customer);
    }

    public async Task UpdateCustomer(Customer customer)
    {
        await _repository.UpdateAsync(customer);
    }

    public async Task DeleteCustomer(int id)
    {
        var customer = await _repository.GetByIdAsync(id);

        if (customer != null)
        {
            await _repository.DeleteAsync(customer);
        }
    }
}