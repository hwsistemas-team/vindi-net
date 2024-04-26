using System.Threading.Tasks;
using Vindi.SDK.Enttites;
using Vindi.SDK.Http;

namespace Vindi.SDK.Services
{
    public class ProductItemsService
    {
        private readonly BaseService<ProductItem> _service;

        public ProductItemsService(VindiServiceContext context)
        {
            _service = new BaseService<ProductItem>(context);
        }

        public async Task<VindiResponseWithData<ProductItem>> CreateAsync(ProductItem productItem)
        {
            var result = await _service.PostAsync<ProductItem, WrapperProductItem>("product_items", productItem);
            return result.MakeNewData(result.Data.ProductItem);
        }

        public async Task<VindiResponseWithData<ProductItem>> GetAsync(int id)
        {
            var result = await  _service.GetAsync<WrapperProductItem>("product_items/" + id);
            return result.MakeNewData(result.Data.ProductItem);
        }

        public async Task<VindiResponseWithData<ProductItem>> UpdateAsync(ProductItem productItem)
        {
            var result = await _service.PutAsync<ProductItem, WrapperProductItem>("product_items/" + productItem.Id, productItem);
            return result.MakeNewData(result.Data.ProductItem);
        }

        public Task<VindiResponse> DeleteAsync(int id)
        {
            return _service.DeleteAsync("product_items/" + id);
        }
    }
}
