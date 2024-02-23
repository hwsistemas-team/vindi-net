using System.Collections.Generic;
using System.Threading.Tasks;
using Vindi.SDK.Enttites;
using Vindi.SDK.Http;

namespace Vindi.SDK.Services
{
    public class CustomerService
    {
        private readonly BaseService<Customer> _service;

        public CustomerService(VindiServiceContext context)
        {
            _service = new BaseService<Customer>(context);
        }

        public async Task<VindiResponseWithData<Customer>> CreateAsync(Customer customer)
        {
            var result = await _service.PostAsync<Customer, WrapperCustomer>("customers", customer);
            return result.MakeNewData(result.Data.Customer);
        }

        public async Task<VindiResponseWithData<IEnumerable<Customer>>> FindAsync(VindRequestParams<Customer> parameters)
        {
            var result = await _service.GetAsync<WrapperCustomers>("customers", parameters);
            return result.MakeNewData(result.Data.Customers);
        }

        public async Task<VindiResponseWithData<Customer>> GetAsync(int id)
        {
            var result = await  _service.GetAsync<WrapperCustomer>("customers/" + id);
            return result.MakeNewData(result.Data.Customer);
        }

        public async Task<VindiResponseWithData<Customer>> UpdateAsync(Customer customer)
        {
            var result = await _service.PostAsync<Customer, WrapperCustomer>("customers/" + customer.Id, customer);
            return result.MakeNewData(result.Data.Customer);
        }

        public Task<VindiResponse> ArchiveAsync(int id)
        {
            return _service.DeleteAsync("customers/" + id);
        }

        public async Task<VindiResponse> UnarchiveAsync(int id)
        {
            var result = await _service.PostAsync<object, WrapperCustomer>($"customers/{id}/unarchive", null);
            return result.MakeNewData((object)null);
        }
    }
}