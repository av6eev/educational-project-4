using System;
using System.Collections.Generic;
using Descriptions.Builds;
using Descriptions.Dialogs.BuildingDialog;

namespace Descriptions.Base
{
    [Serializable]
    public class DescriptionsCollection
    {
        public BuildsDescriptionSo Builds;
        public List<BuildingDialogDescriptionSo> BuildingDialogs;
    }
}