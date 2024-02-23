using System.Threading.Tasks;
using Vindi.SDK.Enttites;
using Vindi.SDK.Http;

namespace Vindi.SDK.Services
{
    public class UsagesService
    {
        private readonly BaseService<Usage> _service;

        public UsagesService(VindiServiceContext context)
        {
            _service = new BaseService<Usage>(context);
        }

        public async Task<VindiResponseWithData<Usage>> CreateAsync(Usage usage)
        {
            var result = await _service.PostAsync<Usage, WrapperUsage>("usages", usage);
            return result.MakeNewData(result.Data.Usage);
        }

        public Task<VindiResponse> DeleteAsync(int id)
        {
            return _service.DeleteAsync("usages/" + id);
        }
    }
}
