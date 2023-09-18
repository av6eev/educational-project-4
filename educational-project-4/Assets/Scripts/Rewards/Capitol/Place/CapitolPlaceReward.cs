using System;
using Rewards.Base;
using UnityEngine;
using Utilities;

namespace Rewards.Capitol.Place
{
    [Serializable]
    public class CapitolPlaceReward : BaseReward
    {
        public override void Give(GameManager manager)
        {
            Debug.Log("capitol placed reward");
            PlayerPrefs.SetString($"{nameof(CapitolPlaceReward)}", "true"); 
        }
    }
}