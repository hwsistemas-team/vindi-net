using System.Threading.Tasks;
using Vindi.SDK.Enttites;

namespace Vindi.SDK.Services
{
    public class DiscountService
    {
        private readonly BaseService<Discount> _service;

        public DiscountService(VindiServiceContext context)
        {
            _service = new BaseService<Discount>(context);
        }

        public async Task<VindiResponseWithData<Discount>> CreateAsync(Discount bill)
        {
            var result = await _service.PostAsync<Discount, WrapperDiscount>("discounts", bill);
            return result.MakeNewData(result.Data.Discount);
        }

        public async Task<VindiResponseWithData<Discount>> GetAsync(int id)
        {
            var result = await _service.GetAsync<WrapperDiscount>("discounts/" + id);
            return result.MakeNewData(result.Data.Discount);
        }

        public async Task<VindiResponseWithData<Discount>> CancelAsync(int id)
        {
            var result = await  _service.DeleteAsync<WrapperDiscount>("discounts/" + id);
            return result.MakeNewData(result.Data.Discount);
        }
    }
}
