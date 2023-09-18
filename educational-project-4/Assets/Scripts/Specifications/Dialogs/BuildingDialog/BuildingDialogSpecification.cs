using System;
using Specifications.Base;
using UnityEngine;

namespace Specifications.Dialogs.BuildingDialog
{
    [Serializable]
    public class BuildingDialogSpecification : ISpecification
    {
        public string Id;
        public string Specification;
        public Sprite PreviewImage;
    }
}