﻿using System.Collections.Generic;
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

        public Task<VindiResponseWithData<IEnumerable<Bill>>> FindAsync(VindRequestParams<Bill> parameters)
        {
            return _service.GetAsync<IEnumerable<Bill>>("bills", parameters);
        }

        public Task<VindiResponseWithData<Bill>> GetAsync(int id)
        {
            return _service.GetAsync<Bill>("bills/" + id);
        }

        public Task<VindiResponseWithData<Bill>> UpdateAsync(Bill bill)
        {
            return _service.PostAsync<Bill, Bill>("bills/" + bill.Id, bill);
        }

        public Task<VindiResponse> CancelAsync(int id)
        {
            return _service.DeleteAsync("bills/" + id);
        }

        public Task<VindiResponseWithData<Bill>> ApproveAsync(int id)
        {
            return _service.PutAsync<Bill, Bill>("bills/" + id, null);
        }
    }
}
