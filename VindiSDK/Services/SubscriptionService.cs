using System.Collections.Generic;
using System.Threading.Tasks;
using Vindi.SDK.Enttites;
using Vindi.SDK.Http;

namespace Vindi.SDK.Services
{
    public class SubscriptionService
    {
        private readonly BaseService<Subscription> _service;

        public SubscriptionService(VindiServiceContext context)
        {
            _service = new BaseService<Subscription>(context);
        }

        public async Task<VindiResponseWithData<Subscription>> CreateAsync(Subscription subscription)
        {
            var result = await _service.PostAsync<Subscription, WrapperSubscription>("subscriptions", subscription);
            return result.MakeNewData(result.Data.Subscription);
        }

        public async Task<VindiResponseWithData<IEnumerable<Subscription>>> FindAsync(VindRequestParams<Subscription> parameters)
        {
            var result = await _service.GetAsync<WrapperSubscriptions>("subscriptions", parameters);
            return result.MakeNewData(result.Data.Subscriptions);
        }

        public async Task<VindiResponseWithData<Subscription>> GetAsync(int id)
        {
            var result = await  _service.GetAsync<WrapperSubscription>("subscriptions/" + id);
            return result.MakeNewData(result.Data.Subscription);
        }

        public async Task<VindiResponseWithData<Subscription>> UpdateAsync(Subscription subscription)
        {
            var result = await _service.PostAsync<Subscription, WrapperSubscription>("subscriptions/" + subscription.Id, subscription);
            return result.MakeNewData(result.Data.Subscription);
        }

        public Task<VindiResponse> CancelAsync(int id, SubscriptionCancelParams parameters)
        {
            var urlParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("id", id.ToString()),
                new KeyValuePair<string, string>("cancel_bills", parameters.CancelBills?.ToString()),
                new KeyValuePair<string, string>("comments", parameters.Comments)
            };

            return _service.DeleteAsync(UrlFormatter.Format("subscriptions/{id}", urlParams));
        }

        public async Task<VindiResponseWithData<Subscription>> ReactivateAsync(int id)
        {
            var result = await _service.PostAsync<object, WrapperSubscription>($"subscriptions/{id}/reactivate", null);
            return result.MakeNewData(result.Data.Subscription);
        }

        public async Task<VindiResponseWithData<Subscription>> RenewAsync(int id)
        {
            var result = await _service.PostAsync<object, WrapperSubscription>($"subscriptions/{id}/renew", null);
            return result.MakeNewData(result.Data.Subscription);
        }
    }

    public class SubscriptionCancelParams
    {
        public bool? CancelBills { get; set; }
        public string Comments { get; set; }
    }
}
