using POS.Data.Dto;
using MediatR;
using System.Collections.Generic;
using System;

namespace POS.MediatR.CommandAndQuery
{
    public class GetAllPackingTypesQuery : IRequest<List<PackingTypeDto>>
    {
        public Guid? Id { get; set; }
        public bool IsDropDown { get; set; } = true;
    }
}
