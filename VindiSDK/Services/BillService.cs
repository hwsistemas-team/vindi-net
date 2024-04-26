using System.Collections.Generic;
using System.Threading.Tasks;
using Vindi.SDK.Enttites;
using Vindi.SDK.Http;

namespace Vindi.SDK.Services
{
    public class BillService
    {
        private readonly BaseService<Bill> _service;

        public BillService(VindiServiceContext context)
        {
            _service = new BaseService<Bill>(context);
        }

        public async Task<VindiResponseWithData<Bill>> CreateAsync(Bill bill)
        {
            var result = await _service.PostAsync<Bill, WrapperBill>("bills", bill);
            return result.MakeNewData(result.Data.Bill);
        }

        public async Task<VindiResponseWithData<IEnumerable<Bill>>> FindAsync(VindRequestParams<Bill> parameters)
        {
            var result = await _service.GetAsync<WrapperBills>("bills", parameters);
            return result.MakeNewData(result.Data.Bills);
        }

        public async Task<VindiResponseWithData<Bill>> GetAsync(int id)
        {
            var result = await _service.GetAsync<WrapperBill>("bills/" + id);
            return result.MakeNewData(result.Data.Bill);
        }

        public async Task<VindiResponseWithData<Bill>> UpdateAsync(Bill bill)
        {
            var result = await _service.PostAsync<Bill, WrapperBill>("bills/" + bill.Id, bill);
            return result.MakeNewData(result.Data.Bill);
        }

        public async Task<VindiResponseWithData<Bill>> CancelAsync(int id, BillCancelParams parameters)
        {
            var urlParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("id", id.ToString()),
                new KeyValuePair<string, string>("comments", parameters.Comments)
            };

            var result = await  _service.DeleteAsync<WrapperBill>(UrlFormatter.Format("bills/{id}", urlParams));
            return result.MakeNewData(result.Data.Bill);
        }

        public async Task<VindiResponseWithData<Bill>> ApproveAsync(int id)
        {
            var result = await _service.PutAsync<Bill, WrapperBill>("bills/" + id, null);
            return result.MakeNewData(result.Data.Bill);
        }
    }

    public class BillCancelParams
    {
        public string Comments { get; set; }
    }
}
