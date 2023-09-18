using System;
using Rewards.Base;
using UnityEngine;
using Utilities;

namespace Rewards.Capitol.FifthLevel
{
    [Serializable]
    public class CapitolFifthLevelReward : BaseReward
    {
        public override void Give(GameManager manager)
        {
            Debug.Log("fifth level award");
            PlayerPrefs.SetString($"{nameof(CapitolFifthLevelReward)}", "true");            
        }
    }
}