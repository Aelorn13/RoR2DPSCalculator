using System;
using System.Collections.Generic;
using System.Text;

namespace RoR2DPSCalculator
{
    class RoRCharacter
    {
        public int Health, HealthPerL, Armor, Level=0;
        public double HealthRegen, HealthRegenPerL, Damage, DamagePerL, Speed, Proc_coef,Attack_speed;
        public string Name;
        public RoRCharacter() :this("",0,0,0,0,0,0,0,0,0,0,0)
        {
        }

        public RoRCharacter(string name, int health, int healthPerL, double healthRegen, double healthRegenPerL, double damage, double damagePerL, double speed, int armor,double proc_coef,double attack_speed,int level)
        {
            Name = name;
            Health = health;
            HealthPerL = healthPerL;
            HealthRegen = healthRegen;
            HealthRegenPerL = healthRegenPerL;
            Damage = damage;
            DamagePerL = damagePerL;
            Speed = speed;
            Armor = armor;
            Proc_coef = proc_coef;
            Attack_speed = attack_speed;
            Level = level;
        }
        public void LevelUp( int HowManyLevels)
        {
               Level += HowManyLevels;
               Health += (HealthPerL * HowManyLevels);
               HealthRegen += (HealthRegenPerL * HowManyLevels);
               Damage += (DamagePerL * HowManyLevels);
        }
        public double Attack (int range)
        {
            double attack_dmg=0;
            if (Name == " Commando")
            {
                    attack_dmg = Damage ;            
            }
            else if (Name== " Huntress")
            {
                attack_dmg = Damage * 1.5;
            }
            else if (Name== " REX")
            {
                attack_dmg = (Damage * 0.8)* 3;
            }
            return attack_dmg;
        }
    }
}
