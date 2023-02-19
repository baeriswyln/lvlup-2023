using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerUpgrades
{
    public static List<Upgrade> GenerateUpgrades(int n)
    {
        var upgrades = new List<Upgrade>();

        for (var i = 0; i < n; i++)
        {
            switch (Random.Range(0, 100))
            {
                case < 20:
                    upgrades.Add(new PlayerSpeedUpgrade());
                    break;
                case < 40:
                    upgrades.Add(new RangeUpgrade());
                    break;
                case < 60:
                    upgrades.Add(new FireRateUpgrade());
                    break;
                case < 80:
                    upgrades.Add(new DamageUpgrade());
                    break;
                default:
                    upgrades.Add(new HealthUpgrade());
                    break;
            }
        }

        return upgrades;
    }
}

public abstract class Upgrade
{
    public Sprite icon;
    public readonly float Bonus;

    public Upgrade()
    {
        do
        {
            Bonus = (float)Math.Round(Random.Range(GetMin(), GetMax()), 2);
        } while (Bonus == 0);
    }

    public string BonusAsString()
    {
        return (Bonus > 0 ? "+" : "") + Bonus;
    }

    public abstract void ApplyToPlayer(PlayerData p);
    public abstract string GetName();
    public abstract Sprite GetImage();
    public abstract float GetMin();
    public abstract float GetMax();
}

public class PlayerSpeedUpgrade : Upgrade
{
    private const float MinVal = 0.3f;

    public override void ApplyToPlayer(PlayerData p)
    {
        p.MovementSpeed += Bonus;
        p.TurningSpeed += Bonus;

        if (p.MovementSpeed < MinVal)
        {
            p.MovementSpeed = MinVal;
        }
        
        if (p.TurningSpeed < MinVal)
        {
            p.TurningSpeed = MinVal;
        }
    }

    public override string GetName()
    {
        return "Player speed";
    }

    public override Sprite GetImage()
    {
        return Resources.Load<Sprite>("icons/bonus_coffee");
    }

    public override float GetMin()
    {
        return -0.2f;
    }

    public override float GetMax()
    {
        return 0.2f;
    }
}

public class FireRateUpgrade : Upgrade
{
    private const float MinVal = 0.1f;
    private const float MaxVal = 5f;

    public override void ApplyToPlayer(PlayerData p)
    {
        p.FireInterval += Bonus;

        if (p.FireInterval < MinVal)
        {
            p.FireInterval = MinVal;
        }
        else if (p.FireInterval > MaxVal)
        {
            p.FireInterval = MaxVal;
        }
    }

    public override string GetName()
    {
        return "Fire interval";
    }

    public override Sprite GetImage()
    {
        return Resources.Load<Sprite>("icons/bonus_whiskey");
    }

    public override float GetMin()
    {
        return -0.2f;
    }

    public override float GetMax()
    {
        return 0.2f;
    }
}

public class RangeUpgrade : Upgrade
{
    private const float MinVal = 3;

    public override void ApplyToPlayer(PlayerData p)
    {
        p.FireRange += Bonus;

        if (p.FireRange < MinVal)
        {
            p.FireRange = MinVal;
        }
    }

    public override string GetName()
    {
        return "Range";
    }

    public override Sprite GetImage()
    {
        return Resources.Load<Sprite>("icons/bonus_gunpowder");
    }

    public override float GetMin()
    {
        return -2f;
    }

    public override float GetMax()
    {
        return 3f;
    }
}

public class DamageUpgrade : Upgrade
{
    private const float MinVal = 0.3f;

    public override void ApplyToPlayer(PlayerData p)
    {
        p.FireDamage += Bonus;

        if (p.FireDamage < MinVal)
        {
            p.FireDamage = MinVal;
        }
    }

    public override string GetName()
    {
        return "Damage";
    }

    public override Sprite GetImage()
    {
        return Resources.Load<Sprite>("icons/bonus_golden_bullet");
    }

    public override float GetMin()
    {
        return -1f;
    }

    public override float GetMax()
    {
        return 2f;
    }
}

public class HealthUpgrade : Upgrade
{
    private const float MinVal = 3;

    public override void ApplyToPlayer(PlayerData p)
    {
        p.InitialHealth += Bonus;

        if (p.InitialHealth < MinVal)
        {
            p.InitialHealth = MinVal;
        }
    }

    public override string GetName()
    {
        return "Max health";
    }

    public override Sprite GetImage()
    {
        return Resources.Load<Sprite>("icons/bonus_burger");
    }

    public override float GetMin()
    {
        return -2f;
    }

    public override float GetMax()
    {
        return 5f;
    }
}