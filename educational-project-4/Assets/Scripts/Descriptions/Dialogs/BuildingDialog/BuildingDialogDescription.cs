using System;
using Descriptions.Base;
using UnityEngine;

namespace Descriptions.Dialogs.BuildingDialog
{
    [Serializable]
    public class BuildingDialogDescription : IDescription
    {
        public string Id;
        public string Description;
        public Sprite PreviewImage;
    }
}