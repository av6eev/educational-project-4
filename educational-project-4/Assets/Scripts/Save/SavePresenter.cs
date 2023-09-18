using System;
using System.Linq;
using Newtonsoft.Json;
using Requirements.Base;
using Requirements.Capitol.FifthLevel;
using UnityEngine;
using Utilities;

namespace Save
{
    public class SavePresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly SaveModel _model;

        public SavePresenter(GameManager manager, SaveModel model)
        {
            _manager = manager;
            _model = model;
        }
        
        public void Deactivate()
        {
            _model.OnSave -= Save;
        }

        public void Activate()
        {
            _model.OnSave += Save;
        }

        private void Save()
        {
            var stringToJson = "{\"requirements\": [";
            var requirements = _manager.Specifications.Requirements.ToList();

            for (var i = 0; i < requirements.Count; i++)
            {
                var requirement = requirements[i];
                
                stringToJson += $"\"{requirement.Key}\": " + "{";

                switch (requirement.Value.Type)
                {
                    case RequirementType.Single:
                        stringToJson += "\"type\": \"single\",";
                        break;
                    case RequirementType.Complex:
                        var complexRequirement = (ComplexRequirement)requirement.Value;
                        stringToJson += "\"sub_type\": \"complex\",";

                        switch (complexRequirement.SubType) 
                        {
                            case SubRequirementType.Place:
                                break;
                            case SubRequirementType.Counter:
                                break;
                            case SubRequirementType.CapitolLevel:
                                var capitolFifthLevel = (CapitolFifthLevelRequirement)complexRequirement;
                                stringToJson += $"\"current_level\": {capitolFifthLevel.CurrentLevel},";
                                stringToJson += $"\"require_level\": {capitolFifthLevel.RequireLevel},";
                                break;
                            default:
                                throw new ArgumentOutOfRangeException($"No sub type for {requirement.Key}");
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"No type for {requirement.Key}");
                }
                
                stringToJson += "\"completed\": ";
                stringToJson += requirement.Value.Completed ? "true" : "false";
                stringToJson += i == requirements.Count - 1 ? "}" : "},";
            }
            
            stringToJson += "]}";

            var serializeObject = JsonConvert.SerializeObject(stringToJson, Formatting.Indented);
            var finalString = serializeObject.Replace('\\', ' ');
            
            PlayerPrefs.SetString("requirements", finalString);
        }
    }
}