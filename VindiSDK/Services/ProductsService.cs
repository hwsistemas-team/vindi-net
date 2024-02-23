using System.Collections.Generic;
using System.Threading.Tasks;
using Vindi.SDK.Enttites;
using Vindi.SDK.Http;

namespace Vindi.SDK.Services
{
    public class ProductsService
    {
        private readonly BaseService<Product> _service;

        public ProductsService(VindiServiceContext context)
        {
            _service = new BaseService<Product>(context);
        }

        public async Task<VindiResponseWithData<Product>> CreateAsync(Product product)
        {
            var result = await _service.PostAsync<Product, WrapperProduct>("products", product);
            return result.MakeNewData(result.Data.Product);
        }

        public async Task<VindiResponseWithData<IEnumerable<Product>>> FindAsync(VindRequestParams<Product> parameters)
        {
            var result = await _service.GetAsync<WrapperProducts>("products", parameters);
            return result.MakeNewData(result.Data.Products);
        }

        public async Task<VindiResponseWithData<Product>> GetAsync(int id)
        {
            var result = await  _service.GetAsync<WrapperProduct>("products/" + id);
            return result.MakeNewData(result.Data.Product);
        }

        public async Task<VindiResponseWithData<Product>> UpdateAsync(Product product)
        {
            var result = await _service.PutAsync<Product, WrapperProduct>("products/" + product.Id, product);
            return result.MakeNewData(result.Data.Product);
        }
    }
}