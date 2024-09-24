using POS.Common.GenericRepository;
using POS.Data;
using POS.Data.Resources;
using System.Threading.Tasks;

namespace POS.Repository
{
    public interface IConsigneeRepository : IGenericRepository<Consignee>
    {
        Task<ConsigneeList> GetConsignees(ConsigneeResource ConsigneeResource);
        //Task<ConsigneePaymentList> GetConsigneesPayment(ConsigneeResource ConsigneeResource);
    }
}
