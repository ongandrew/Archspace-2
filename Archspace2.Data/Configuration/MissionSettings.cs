using System.Collections.Generic;

namespace Archspace2
{
    public class MissionSettings
    {
        public int TrainTime { get; set; }
        public int PatrolTime { get; set; }
        public int DispatchToAllyTime { get; set; }
        public int ExpeditionTime { get; set; }
        public int ReturningWithPlanetTime { get; set; }
        public int PrivateerTime { get; set; }

        public int RaidExperience { get; set; }
        public int RaidExperienceMultiplier { get; set; }
        public int PatrolExperience { get; set; }
        public int StationExperience { get; set; }
        public int ExpeditionExperience { get; set; }
        public int BattleExperience { get; set; }
        public int DetectionExperience { get; set; }

        public int MinFleetTrainExperience { get; set; }
        public int MinAdmiralTrainExperience { get; set; }
        public int MinAdmiralPrivateerExperience { get; set; }
        public int MaxAdmiralPrivateerExperience { get; set; }
        public int MaxPrivateerCapacity { get; set; }

        public Dictionary<int, int> ExpeditionChance { get; set; }

        public MissionSettings()
        {
            ExpeditionChance = new Dictionary<int, int>();
        }

        public static MissionSettings CreateDefault()
        {
            return new MissionSettings()
            {
                TrainTime = 21600,
                PatrolTime = 86400,
                DispatchToAllyTime = 259200,
                ExpeditionTime = 21600,
                ReturningWithPlanetTime = 21600,
                PrivateerTime = 21600,

                RaidExperience = 0,
                RaidExperienceMultiplier = 20,
                PatrolExperience = 10,
                StationExperience = 5,
                ExpeditionExperience = 1000,
                BattleExperience = 0,
                DetectionExperience = 0,
                MinFleetTrainExperience = 10,
                MinAdmiralTrainExperience = 1000,
                MinAdmiralPrivateerExperience = 0,
                MaxAdmiralPrivateerExperience = 1000,
                MaxPrivateerCapacity = 70,

                ExpeditionChance = new Dictionary<int, int>()
                {
                    [0] = 100,
                    [1] = 100,
                    [2] = 75,
                    [3] = 50,
                    [4] = 50,
                    [5] = 29,
                    [6] = 23,
                    [7] = 20,
                    [8] = 17,
                    [9] = 14,
                    [10] = 12,
                    [11] = 10,
                    [12] = 9,
                    [13] = 8,
                    [14] = 7,
                    [15] = 5,
                    [16] = 5,
                    [17] = 4,
                    [18] = 3,
                    [19] = 2,
                    [20] = 1
                }
            };
        }
    }
}
