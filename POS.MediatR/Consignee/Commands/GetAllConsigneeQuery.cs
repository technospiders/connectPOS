using POS.Data.Resources;
using POS.Repository;
using MediatR;

namespace POS.MediatR.CommandAndQuery
{
    public class GetAllConsigneeQuery : IRequest<ConsigneeList>
    {
        public ConsigneeResource ConsigneeResource { get; set; }
    }
}
