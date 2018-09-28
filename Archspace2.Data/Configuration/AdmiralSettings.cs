using System.Collections.Generic;

namespace Archspace2
{
    public class AdmiralSettings
    {
        public int MaxLevel { get; set; }

        public Dictionary<int, AdmiralLevelSetting> LevelSettings { get; set; }
        public Dictionary<StartingCircumstance, int> StartingCircumstanceWeights { get; set; }

        public AdmiralSettings()
        {
            LevelSettings = new Dictionary<int, AdmiralLevelSetting>();
            StartingCircumstanceWeights = new Dictionary<StartingCircumstance, int>();
        }

        public static AdmiralSettings CreateDefault()
        {
            return new AdmiralSettings()
            {
                MaxLevel = 20,
                LevelSettings = new Dictionary<int, AdmiralLevelSetting>()
                {
                    [0] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 0,
                        AdditionalFleetCommanding = 0
                    },
                    [1] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 1000,
                        AdditionalFleetCommanding = 0
                    },
                    [2] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 3000,
                        AdditionalFleetCommanding = 3
                    },
                    [3] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 6000,
                        AdditionalFleetCommanding = 3
                    },
                    [4] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 10000,
                        AdditionalFleetCommanding = 3
                    },
                    [5] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 15000,
                        AdditionalFleetCommanding = 2
                    },
                    [6] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 21000,
                        AdditionalFleetCommanding = 2
                    },
                    [7] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 28000,
                        AdditionalFleetCommanding = 2
                    },
                    [8] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 36000,
                        AdditionalFleetCommanding = 2
                    },
                    [9] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 45000,
                        AdditionalFleetCommanding = 2
                    },
                    [10] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 55000,
                        AdditionalFleetCommanding = 1
                    },
                    [11] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 66000,
                        AdditionalFleetCommanding = 1
                    },
                    [12] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 78000,
                        AdditionalFleetCommanding = 1
                    },
                    [13] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 91000,
                        AdditionalFleetCommanding = 1
                    },
                    [14] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 105000,
                        AdditionalFleetCommanding = 1
                    },
                    [15] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 120000,
                        AdditionalFleetCommanding = 1
                    },
                    [16] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 136000,
                        AdditionalFleetCommanding = 1
                    },
                    [17] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 153000,
                        AdditionalFleetCommanding = 1
                    },
                    [18] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 171000,
                        AdditionalFleetCommanding = 1
                    },
                    [19] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 190000,
                        AdditionalFleetCommanding = 1
                    },
                    [20] = new AdmiralLevelSetting()
                    {
                        RequiredExperience = 210000,
                        AdditionalFleetCommanding = 0
                    }
                },
                StartingCircumstanceWeights = new Dictionary<StartingCircumstance, int>()
                {
                    [StartingCircumstance.Supercommander] = 0,
                    [StartingCircumstance.Excellent] = 10,
                    [StartingCircumstance.VeryGood] = 20,
                    [StartingCircumstance.Good] = 30,
                    [StartingCircumstance.Average] = 40,
                    [StartingCircumstance.Poor] = 50,
                    [StartingCircumstance.Bad] = 60,
                    [StartingCircumstance.VeryBad] = 70,
                    [StartingCircumstance.CannonFodder] = 80
                }
            };
        }
    }
}
