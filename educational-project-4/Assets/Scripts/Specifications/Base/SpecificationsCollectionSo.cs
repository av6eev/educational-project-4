using UnityEngine;

namespace Specifications.Base
{
    [CreateAssetMenu(menuName = "Create Specifications Collection/New Specification Collection", fileName = "NewCollection", order = 51)]
    public class SpecificationsCollectionSo : ScriptableObject
    {
        public SpecificationsCollection Collection;
    }
}