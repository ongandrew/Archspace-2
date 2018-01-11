﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
