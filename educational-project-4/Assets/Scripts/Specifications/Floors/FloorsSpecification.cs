using System;
using System.Collections.Generic;
using Specifications.Base;

namespace Specifications.Floors
{
    [Serializable]
    public class FloorsSpecification : ISpecification
    {
        public List<FloorSpecificationSo> Floors;
    }
}