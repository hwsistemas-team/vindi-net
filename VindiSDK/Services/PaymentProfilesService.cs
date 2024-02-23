using System.Collections.Generic;
using System.Threading.Tasks;
using Vindi.SDK.Enttites;
using Vindi.SDK.Http;

namespace Vindi.SDK.Services
{
    public class PaymentProfilesService
    {
        private readonly BaseService<PaymentProfile> _service;

        public PaymentProfilesService(VindiServiceContext context)
        {
            _service = new BaseService<PaymentProfile>(context);
        }

        public async Task<VindiResponseWithData<PaymentProfile>> CreateAsync(PaymentProfile paymentProfile)
        {
            var result = await _service.PostAsync<PaymentProfile, WrapperPaymentProfile>("payment_profiles", paymentProfile);
            return result.MakeNewData(result.Data.PaymentProfile);
        }

        public async Task<VindiResponseWithData<IEnumerable<PaymentProfile>>> FindAsync(VindRequestParams<PaymentProfile> parameters)
        {
            var result = await _service.GetAsync<WrapperPaymentProfiles>("payment_profiles", parameters);
            return result.MakeNewData(result.Data.PaymentProfiles);
        }

        public async Task<VindiResponseWithData<PaymentProfile>> GetAsync(int id)
        {
            var result = await  _service.GetAsync<WrapperPaymentProfile>("payment_profiles/" + id);
            return result.MakeNewData(result.Data.PaymentProfile);
        }

        public async Task<VindiResponseWithData<PaymentProfile>> UpdateAsync(PaymentProfile paymentProfile)
        {
            var result = await _service.PostAsync<PaymentProfile, WrapperPaymentProfile>("payment_profiles/" + paymentProfile.Id, paymentProfile);
            return result.MakeNewData(result.Data.PaymentProfile);
        }

        public async Task<VindiResponse> Verify(int id)
        {
            var result = await _service.PostAsync<object, WrapperCustomer>($"payment_profiles/{id}/verify", null);
            return result.MakeNewData((object)null);
        }
    }
}
