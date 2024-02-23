using System.Collections.Generic;
using System.Threading.Tasks;
using Vindi.SDK.Enttites;
using Vindi.SDK.Http;

namespace Vindi.SDK.Services
{
    public class PeriodsService
    {
        private readonly BaseService<Period> _service;

        public PeriodsService(VindiServiceContext context)
        {
            _service = new BaseService<Period>(context);
        }

        public async Task<VindiResponseWithData<IEnumerable<Period>>> FindAsync(VindRequestParams<Period> parameters)
        {
            var result = await _service.GetAsync<WrapperPeriods>("periods", parameters);
            return result.MakeNewData(result.Data.Periods);
        }

        public async Task<VindiResponseWithData<Period>> GetAsync(int id)
        {
            var result = await  _service.GetAsync<WrapperPeriod>("periods/" + id);
            return result.MakeNewData(result.Data.Period);
        }

        public async Task<VindiResponseWithData<Period>> UpdateAsync(Period period)
        {
            var result = await _service.PostAsync<Period, WrapperPeriod>("periods/" + period.Id, period);
            return result.MakeNewData(result.Data.Period);
        }

        public async Task<VindiResponseWithData<Bill>> IssueBillAsync(int id)
        {
            var result = await _service.PostAsync<object, WrapperBill>($"periods/{id}/bill", null);
            return result.MakeNewData(result.Data.Bill);
        }

        public async Task<VindiResponseWithData<IEnumerable<Usage>>> UsagesAsync(int id)
        {
            var result = await  _service.GetAsync<WrapperUsages>($"periods/{id}/usages");
            return result.MakeNewData(result.Data.Usages);
        }
    }
}
