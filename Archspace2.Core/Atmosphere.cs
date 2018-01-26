using Newtonsoft.Json;
using System.Collections.Generic;
using Universal.Common.Extensions;

namespace Archspace2
{
    public enum GasType
    {
        H2,
        Cl2,
        CO2,
        O2,
        N2,
        CH4,
        H2O
    };

    public class Atmosphere
    {
        [JsonProperty("H2")]
        public int H2 { get; set; }
        [JsonProperty("Cl2")]
        public int Cl2 { get; set; }
        [JsonProperty("CO2")]
        public int CO2 { get; set; }
        [JsonProperty("O2")]
        public int O2 { get; set; }
        [JsonProperty("N2")]
        public int N2 { get; set; }
        [JsonProperty("CH4")]
        public int CH4 { get; set; }
        [JsonProperty("H2O")]
        public int H2O { get; set; }

        public Atmosphere()
        {
        }
        public Atmosphere(Atmosphere aOther)
        {
            this.Bind(aOther);
        }

        public static bool operator ==(Atmosphere lhs, Atmosphere rhs)
        {
            return lhs.H2 == rhs.H2 && lhs.Cl2 == rhs.Cl2 && lhs.CO2 == rhs.CO2 &&
                lhs.O2 == rhs.O2 && lhs.N2 == rhs.N2 && lhs.CH4 == rhs.CH4 && lhs.H2O == rhs.H2O;
        }

        public static bool operator !=(Atmosphere lhs, Atmosphere rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            return this.Equals((Atmosphere)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return H2.GetHashCode() * Cl2.GetHashCode() * CO2.GetHashCode() * O2.GetHashCode() + N2 + CH4 + H2O;
            }
        }

        public void Change(GasType aGasToDecrease, GasType aGasToIncrease)
        {
            this[aGasToDecrease]--;
            this[aGasToIncrease]++;
        }

        public int this[GasType index]
        {
            get
            {
                switch (index)
                {
                    case GasType.H2:
                        return H2;
                    case GasType.Cl2:
                        return Cl2;
                    case GasType.CO2:
                        return CO2;
                    case GasType.O2:
                        return O2;
                    case GasType.N2:
                        return N2;
                    case GasType.CH4:
                        return CH4;
                    case GasType.H2O:
                        return H2O;
                    default:
                        return 0;
                }
            }
            set
            {
                switch (index)
                {
                    case GasType.H2:
                        H2 = value;
                        break;
                    case GasType.Cl2:
                        Cl2 = value;
                        break;
                    case GasType.CO2:
                        CO2 = value;
                        break;
                    case GasType.O2:
                        O2 = value;
                        break;
                    case GasType.N2:
                        N2 = value;
                        break;
                    case GasType.CH4:
                        CH4 = value;
                        break;
                    case GasType.H2O:
                        H2O =value;
                        break;
                    default:
                        return;
                }
            }
        }

        public Dictionary<GasType, int> AsDictionary()
        {
            Dictionary<GasType, int> result = new Dictionary<GasType, int>()
            {
                [GasType.H2] = H2,
                [GasType.Cl2] = Cl2,
                [GasType.CO2] = CO2,
                [GasType.O2] = O2,
                [GasType.N2] = N2,
                [GasType.CH4] = CH4,
                [GasType.H2O] = H2O
            };

            return result;
        }
    }
}
