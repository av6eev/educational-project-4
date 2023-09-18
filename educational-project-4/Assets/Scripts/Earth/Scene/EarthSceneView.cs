using UnityEngine;
using Utilities;

namespace Earth.Scene
{
    public class EarthSceneView : GameSceneView
    {
        [field: SerializeField] public EarthView EarthView { get; private set; }
    }
}