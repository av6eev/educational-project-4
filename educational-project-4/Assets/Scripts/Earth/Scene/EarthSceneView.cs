using UnityEngine;

namespace Earth.Scene
{
    public class EarthSceneView : GameSceneView
    {
        [field: SerializeField] public EarthView EarthView { get; private set; }
    }
}