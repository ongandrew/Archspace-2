using System;
using System.Collections.Generic;
using System.Linq;

namespace Archspace2.Extensions
{
    public static class ArchspaceExtensions
    {
        public static bool Evaluate(this List<PlayerPrerequisite> tList, Player aPlayer)
        {
            if (tList.Count == 0)
            {
                return true;
            }
            else
            {
                return tList.TrueForAll(x => x.Evaluate(aPlayer));
            }
        }

        public static ControlModel CalculateControlModelModifier(this IEnumerable<IControlModelModifier> tEnumerable)
        {
            ControlModel result = tEnumerable.Select(x => x.ControlModelModifier).Aggregate(new ControlModel(), (a, b) => a + b);

            return result;
        }

        public static ControlModel GetControlModelModifier(this ConcentrationMode tConcentrationMode)
        {
            switch (tConcentrationMode)
            {
                case ConcentrationMode.Industry:
                    return new ControlModel()
                    {
                        Production = 3,
                        Military = -2,
                        Research = -1
                    };
                case ConcentrationMode.Military:
                    return new ControlModel()
                    {
                        Production = -2,
                        Military = 5,
                        Research = -2
                    };
                case ConcentrationMode.Research:
                    return new ControlModel()
                    {
                        Production = -2,
                        Military = -2,
                        Research = 3
                    };
                case ConcentrationMode.Balanced:
                default:
                    return new ControlModel();
            }
        }

        public static long GetTotalFactoryCount(this IEnumerable<Planet> tPlanets)
        {
            return tPlanets == null || !tPlanets.Any() ? 0 : tPlanets.Sum(x => x.Infrastructure.Factory);
        }

        public static long GetTotalResearchLabCount(this IEnumerable<Planet> tPlanets)
        {
            return tPlanets == null || !tPlanets.Any() ? 0 : tPlanets.Sum(x => x.Infrastructure.ResearchLab);
        }

        public static long GetTotalMilitaryBaseCount(this IEnumerable<Planet> tPlanets)
        {
            return tPlanets == null || !tPlanets.Any() ? 0 : tPlanets.Sum(x => x.Infrastructure.MilitaryBase);
        }

        public static bool Contains(this IEnumerable<PlanetAttribute> tEnumerable, PlanetAttributeType aPlanetAttributeType)
        {
            return tEnumerable.Any(x => x.Type == aPlanetAttributeType);
        }

        public static PlanetAttribute ToPlanetAttribute(this PlanetAttributeType tPlanetAttributeType)
        {
            return Game.Configuration.PlanetAttributes.Single(x => x.Type == tPlanetAttributeType);
        }

        public static bool Evaluate(this PlayerPrerequisite tPlayerPrerequisite, Player aPlayer)
        {
            switch (tPlayerPrerequisite.Type)
            {
                case PrerequisiteType.Race:
                    return tPlayerPrerequisite.EvaluateRacePrerequisite(aPlayer);
                case PrerequisiteType.RacialTrait:
                    return tPlayerPrerequisite.EvaluateRacialTraitPrerequisite(aPlayer);
                case PrerequisiteType.Society:
                    return tPlayerPrerequisite.EvaluateSocietyPrerequisite(aPlayer);
                case PrerequisiteType.Planet:
                    throw new NotImplementedException();
                case PrerequisiteType.Tech:
                    return tPlayerPrerequisite.EvaluateTechPrerequisite(aPlayer);
                default:
                    return false;
            }
        }

        private static bool EvaluateRacePrerequisite(this PlayerPrerequisite tPlayerPrerequisite, Player aPlayer)
        {
            return aPlayer.Race.Id == (int)tPlayerPrerequisite.Value;
        }

        private static bool EvaluateRacialTraitPrerequisite(this PlayerPrerequisite tPlayerPrerequisite, Player aPlayer)
        {
            return aPlayer.Race.BaseTraits.Contains((RacialTrait) tPlayerPrerequisite.Value);
        }

        private static bool EvaluateSocietyPrerequisite(this PlayerPrerequisite tPlayerPrerequisite, Player aPlayer)
        {
            return aPlayer.Race.SocietyType == (SocietyType)tPlayerPrerequisite.Value;
        }

        private static bool EvaluateTechPrerequisite(this PlayerPrerequisite tPlayerPrerequisite, Player aPlayer)
        {
            return aPlayer.Techs.Any(x => x.Id == (int)tPlayerPrerequisite.Value);
        }
    }
}
