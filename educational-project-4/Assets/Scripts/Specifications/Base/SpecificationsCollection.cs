using System;
using System.Collections.Generic;
using Specifications.Builds;
using Specifications.Dialogs.BuildingDialog;
using Specifications.Floors;
using Specifications.Requirements;
using Specifications.Rewards;

namespace Specifications.Base
{
    [Serializable]
    public class SpecificationsCollection
    {
        public BuildsSpecificationSo Builds;
        public List<BuildingDialogSpecificationSo> BuildingDialogs;
        public FloorsSpecificationSo Floors;
        public RewardsDataAsset RewardsData;
        public RequirementsDataAsset RequirementsData;
    }
}