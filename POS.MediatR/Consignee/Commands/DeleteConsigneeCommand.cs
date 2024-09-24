using POS.Helper;
using MediatR;
using System;

namespace POS.MediatR.CommandAndQuery
{
    public class DeleteConsigneeCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
