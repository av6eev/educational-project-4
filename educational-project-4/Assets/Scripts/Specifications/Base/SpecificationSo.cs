using UnityEngine;

namespace Specifications.Base
{
    public class SpecificationSo<T> : ScriptableObject where T : ISpecification
    {
        public T Specification;
    }
}