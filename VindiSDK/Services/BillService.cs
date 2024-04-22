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

        public Task<VindiResponseWithData<Bill>> CreateAsync(Bill bill)
        {
            return _service.PostAsync<Bill, Bill>("bills", bill);
        }

        public async Task<VindiResponseWithData<IEnumerable<Bill>>> FindAsync(VindRequestParams<Bill> parameters)
        {
            var result = await _service.GetAsync<WrapperBills>("bills", parameters);
            return result.MakeNewData(result.Data.Bills);
        }

        public Task<VindiResponseWithData<Bill>> GetAsync(int id)
        {
            return _service.GetAsync<Bill>("bills/" + id);
        }

        public Task<VindiResponseWithData<Bill>> UpdateAsync(Bill bill)
        {
            return _service.PostAsync<Bill, Bill>("bills/" + bill.Id, bill);
        }

        public Task<VindiResponse> CancelAsync(int id, BillCancelParams parameters)
        {
            var urlParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("id", id.ToString()),
                new KeyValuePair<string, string>("comments", parameters.Comments)
            };

            return _service.DeleteAsync(UrlFormatter.Format("bills/{id}", urlParams));
        }

        public Task<VindiResponseWithData<Bill>> ApproveAsync(int id)
        {
            return _service.PutAsync<Bill, Bill>("bills/" + id, null);
        }
    }

    public class BillCancelParams
    {
        public string Comments { get; set; }
    }
}
