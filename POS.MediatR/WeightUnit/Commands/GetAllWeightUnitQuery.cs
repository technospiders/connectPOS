using POS.Data.Dto;
using MediatR;
using System.Collections.Generic;
using System;

namespace POS.MediatR.CommandAndQuery
{
    public class GetAllWeightUnitQuery : IRequest<List<WeightUnitDto>>
    {
        public Guid? Id { get; set; }
        public bool IsDropDown { get; set; } = true;
    }
}
