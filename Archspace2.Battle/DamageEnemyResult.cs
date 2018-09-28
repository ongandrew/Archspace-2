namespace Archspace2.Battle
{
    public class DamageEnemyResult
    {
        public int TotalDamage { get; set; }
        public int HitCount { get; set; }
        public int MissCount { get; set; }
        public int SunkenCount { get; set; }

        public DamageEnemyResult(int aTotalDamage, int aHitCount, int aMissCount, int aSunkenCount)
        {
            TotalDamage = aTotalDamage;
            HitCount = aHitCount;
            MissCount = aMissCount;
            SunkenCount = aSunkenCount;
        }
    }
}
