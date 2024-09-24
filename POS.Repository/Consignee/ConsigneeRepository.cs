using AutoMapper;
using POS.Common.GenericRepository;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Data.Dto;
using POS.Data.Resources;
using POS.Domain;
using POS.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POS.Repository
{
    public class ConsigneeRepository : GenericRepository<Consignee, POSDbContext>, IConsigneeRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        public ConsigneeRepository(
            IUnitOfWork<POSDbContext> uow,
            IPropertyMappingService propertyMappingService,
             IMapper mapper,
             IPurchaseOrderRepository purchaseOrderRepository)
            : base(uow)
        {
            _mapper = mapper;
            _propertyMappingService = propertyMappingService;
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public async Task<ConsigneeList> GetConsignees(ConsigneeResource consigneeResource)
        {
            var collectionBeforePaging =
                AllIncluding(c => c.ConsigneeAddress, c => c.Customer).ApplySort(consigneeResource.OrderBy,
                _propertyMappingService.GetPropertyMapping<ConsigneeDto, Consignee>());


            // Filter by CustomerId if provided
            if (consigneeResource.CustomerId != Guid.Empty)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.CustomerId == consigneeResource.CustomerId);
            }


            if (consigneeResource.Id != null)
            {
                // trim & ignore casing
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Id == consigneeResource.Id);
            }

            if (!string.IsNullOrEmpty(consigneeResource.ConsigneeName))
            {
                // trim & ignore casing
                var genreForWhereClause = consigneeResource.ConsigneeName
                    .Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.ConsigneeName, $"{encodingName}%"));
            }

            if (!string.IsNullOrEmpty(consigneeResource.MobileNo))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = consigneeResource.MobileNo
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => (a.MobileNo != null && EF.Functions.Like(a.MobileNo, $"%{searchQueryForWhereClause}%")) ||
                    (a.PhoneNo != null && EF.Functions.Like(a.PhoneNo, $"%{searchQueryForWhereClause}%")));
            }
            if (!string.IsNullOrEmpty(consigneeResource.Email))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = consigneeResource.Email
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Email, $"{searchQueryForWhereClause}%"));
            }
            if (!string.IsNullOrEmpty(consigneeResource.Website))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = consigneeResource.Website
                    .Trim().ToLowerInvariant();

                var name = Uri.UnescapeDataString(searchQueryForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Website, $"%{encodingName}%"));
            }
            if (!string.IsNullOrEmpty(consigneeResource.SearchQuery))
            {
                var searchQueryForWhereClause = consigneeResource.SearchQuery
              .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a =>
                    EF.Functions.Like(a.ConsigneeName, $"%{searchQueryForWhereClause}%")
                    || EF.Functions.Like(a.MobileNo, $"{searchQueryForWhereClause}%")
                    || (a.PhoneNo != null && EF.Functions.Like(a.PhoneNo, $"{searchQueryForWhereClause}%"))
                    || EF.Functions.Like(a.PhoneNo, $"{searchQueryForWhereClause}%"
                    )
                    );
            }

            if (!string.IsNullOrWhiteSpace(consigneeResource.Country))
            {
                collectionBeforePaging = collectionBeforePaging
                  .Where(a => a.ConsigneeAddress.CountryName == consigneeResource.Country);
            }

            var ConsigneeList = new ConsigneeList(_mapper);
            return await ConsigneeList.Create(collectionBeforePaging, consigneeResource.Skip, consigneeResource.PageSize);
        }

        //public async Task<ConsigneePaymentList> GetConsigneesPayment(ConsigneeResource supplierResource)
        //{
        //    var collectionBeforePaging =
        //        _purchaseOrderRepository
        //        .AllIncluding(c => c.Consignee)
        //        .ApplySort(supplierResource.OrderBy,
        //        _propertyMappingService.GetPropertyMapping<PurchaseOrderDto, PurchaseOrder>());

        //    if (!string.IsNullOrEmpty(supplierResource.ConsigneeName))
        //    {
        //        // trim & ignore casing
        //        var genreForWhereClause = supplierResource.ConsigneeName
        //            .Trim().ToLowerInvariant();
        //        var name = Uri.UnescapeDataString(genreForWhereClause);
        //        var encodingName = WebUtility.UrlDecode(name);
        //        var ecapestring = Regex.Unescape(encodingName);
        //        encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
        //        collectionBeforePaging = collectionBeforePaging
        //            .Where(a => EF.Functions.Like(a.Consignee.ConsigneeName, $"{encodingName}%"));
        //    }

        //    var groupedCollection = collectionBeforePaging.GroupBy(c => c.ConsigneeId);

        //    var supplierPayments = new ConsigneePaymentList();
        //    return await supplierPayments.Create(groupedCollection, supplierResource.Skip, supplierResource.PageSize);
        //}
    
    }
}
