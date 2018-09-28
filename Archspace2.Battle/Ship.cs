namespace Archspace2.Battle
{
    // The plan is to redo the battle system so that this is autonomous and tracked on a real per-ship basis.
    public class Ship
    {
        protected int mHP;
        public int HP
        {
            get => mHP;
            set
            {
                if (value < 0)
                {
                    mHP = 0;
                }
                else
                {
                    mHP = value;
                }
            }
        }

        protected int mShieldStrength;
        public int ShieldStrength {
            get => mShieldStrength;
            set
            {
                if (value < 0)
                {
                    mShieldStrength = 0;
                }
                else
                {
                    mShieldStrength = value;
                }
            }
        }

        public Ship(int aHp, int aShieldStrength)
        {
            HP = aHp;
            ShieldStrength = aShieldStrength;
        }
    }
}
