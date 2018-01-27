using System;
using System.Collections.Generic;
using System.Text;
using Universal.Common.Extensions;

namespace Archspace2.Battle
{
    public class Turret : Weapon
    {
        public Fleet Fleet { get; set; }
        public int Number { get; set; }
        public int RemainingCooldown { get; set; }

        public Turret(Weapon aWeapon, int aNumber)
        {
            this.Bind(aWeapon);
            Number = aNumber;
            RemainingCooldown = 0;
        }

        public bool IsReady()
        {
            return RemainingCooldown <= 0;
        }

        public void Fire()
        {
            int cooldown = CoolingTime;
            cooldown = (int)(cooldown * (100 + Fleet.Morale / -5 + 10 + Fleet.Experience / -5 + 10) / 100);

            cooldown = Fleet.Effects.OfType(FleetEffectType.CoolingTime).CalculateTotalEffect(cooldown, x => x.Amount.Value);

            switch (Type)
            {
                case WeaponType.Beam:
                    cooldown = Fleet.Effects.OfType(FleetEffectType.BeamCoolingTime).CalculateTotalEffect(cooldown, x => x.Amount.Value);
                    break;
                case WeaponType.Missile:
                    cooldown = Fleet.Effects.OfType(FleetEffectType.MissileCoolingTime).CalculateTotalEffect(cooldown, x => x.Amount.Value);
                    break;
                case WeaponType.Projectile:
                    cooldown = Fleet.Effects.OfType(FleetEffectType.ProjectileCoolingTime).CalculateTotalEffect(cooldown, x => x.Amount.Value);
                    break;
                default:
                    break;
            }

            RemainingCooldown = cooldown;
        }

        public void Cool()
        {
            if (RemainingCooldown > 0)
            {
                RemainingCooldown--;
            }
        }

        public int CalculateAttackRating()
        {
            int computerAttackRating = Fleet.Computer.AttackRating;
            int turretAttackRating = AttackRating;

            computerAttackRating = Fleet.Effects.OfType(FleetEffectType.Computer).CalculateTotalEffect(computerAttackRating, x => x.Amount.Value);
            turretAttackRating = Fleet.Effects.OfType(FleetEffectType.WeaponAttackRating).CalculateTotalEffect(turretAttackRating, x => x.Amount.Value);

            switch (Type)
            {
                case WeaponType.Beam:
                    turretAttackRating = Fleet.Effects.OfType(FleetEffectType.BeamAttackRating).CalculateTotalEffect(turretAttackRating, x => x.Amount.Value);
                    break;
                case WeaponType.Missile:
                    turretAttackRating = Fleet.Effects.OfType(FleetEffectType.MissileAttackRating).CalculateTotalEffect(turretAttackRating, x => x.Amount.Value);
                    break;
                case WeaponType.Projectile:
                    turretAttackRating = Fleet.Effects.OfType(FleetEffectType.ProjectileAttackRating).CalculateTotalEffect(turretAttackRating, x => x.Amount.Value);
                    break;
            }

            int result = turretAttackRating * (100 + computerAttackRating + Fleet.Experience) / 100;

            result = Fleet.Effects.OfType(FleetEffectType.AttackRating).CalculateTotalEffect(result, x => x.Amount.Value);

            return result;
        }
    }
}
