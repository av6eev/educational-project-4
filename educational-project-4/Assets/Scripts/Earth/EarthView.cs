using BuildDialog;
using Descriptions.Base;
using Grid;
using UnityEngine;

namespace Earth
{
    public class EarthView : MonoBehaviour
    {
        public DescriptionsCollectionSo DescriptionsCollection;

        public GridView GridView;
        public BuildDialogView BuildDialogView;
    
        public Camera Camera;
        public GameObject FogParticleSystem;
    }
}