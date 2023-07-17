using BuildDialog;
using Descriptions.Base;
using Earth.Grid;
using UnityEngine;
using UnityEngine.Serialization;

namespace Earth
{
    public class EarthView : MonoBehaviour
    {
        public DescriptionsCollectionSo DescriptionsCollection;

        [FormerlySerializedAs("GridView")] public EarthGridView EarthGridView;
        public BuildDialogView BuildDialogView;
    
        public Camera Camera;
        public GameObject FogParticleSystem;
    }
}