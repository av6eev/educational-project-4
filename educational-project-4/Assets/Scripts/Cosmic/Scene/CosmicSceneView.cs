using Cosmic.Ship;
using UnityEngine;

namespace Cosmic.Scene
{
    public class CosmicSceneView : GameSceneView
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public CosmicView CosmicView { get; private set; }
        [field: SerializeField] public CosmicShipView CosmicShipView { get; private set; }
    }
}