using System.Collections.Generic;
using UnityEngine;

namespace Specifications.Rewards
{
    [CreateAssetMenu(menuName = "Create Specifications Collection/New RewardsData Specification", fileName = "RewardsDataSpecification", order = 51)]
    public class RewardsDataAsset : ScriptableObject
    {
        public List<RewardAssetSo> Assets;
    }
}