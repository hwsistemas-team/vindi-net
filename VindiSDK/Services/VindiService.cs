using System;

namespace Vindi.SDK.Services
{
    public class VindiService
    {
        private readonly VindiServiceContext _context;

        private CustomerService _customers;
        private ProductsService _products;
        private BillService _bills;
        private PaymentProfilesService _paymentProfiles;
        private SubscriptionService _subscriptions;
        private PeriodsService _periods;
        private UsagesService _usages;
        private ProductItemsService _productItems;
        private DiscountService _discounts;

        public VindiService(string baseUrl, string apiKey)
        {
            if (String.IsNullOrEmpty(apiKey))
                throw new ArgumentException($"Parameter {nameof(apiKey)} is not valid");

            if (String.IsNullOrEmpty(baseUrl))
                throw new ArgumentException($"Parameter {nameof(baseUrl)} is not valid");

            _context = new VindiServiceContext(baseUrl, apiKey);
        }

        public CustomerService Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new CustomerService(_context);

                return _customers;
            }
        }

        public ProductsService Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductsService(_context);

                return _products;
            }
        }

        public BillService Bills
        {
            get
            {
                if (_bills == null)
                    _bills = new BillService(_context);

                return _bills;
            }
        }

        public ProductItemsService ProductItems
        {
            get
            {
                if (_productItems == null)
                    _productItems = new ProductItemsService(_context);

                return _productItems;
            }
        }

        public PaymentProfilesService PaymentProfiles
        {
            get
            {
                if (_paymentProfiles == null)
                    _paymentProfiles = new PaymentProfilesService(_context);

                return _paymentProfiles;
            }
        }

        public SubscriptionService Subscriptions
        {
            get
            {
                if (_subscriptions == null)
                    _subscriptions = new SubscriptionService(_context);

                return _subscriptions;
            }
        }

        public PeriodsService Periods
        {
            get
            {
                if (_periods == null)
                    _periods = new PeriodsService(_context);

                return _periods;
            }
        }

        public UsagesService Usages
        {
            get
            {
                if (_usages == null)
                    _usages = new UsagesService(_context);

                return _usages;
            }
        }

        public DiscountService Discounts
        {
            get
            {
                if (_discounts == null)
                    _discounts = new DiscountService(_context);

                return _discounts;
            }
        }
    }
}
