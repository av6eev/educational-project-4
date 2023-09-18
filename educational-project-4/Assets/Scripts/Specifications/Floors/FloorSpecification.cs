using System;
using Specifications.Base;

namespace Specifications.Floors
{
    [Serializable]
    public class FloorSpecification : ISpecification
    {
        public int Index;
        public int AccessLevel;
    }
}