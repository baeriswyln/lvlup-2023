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
                case < 10:
                    upgrades.Add(new PlayerSpeedUpgrade());
                    break;
                case < 20:
                    upgrades.Add(new RotationSpeedUpgrade());
                    break;
                case < 30:
                    upgrades.Add(new RangeUpgrade());
                    break;
                case < 50:
                    upgrades.Add(new FireRateUpgrade());
                    break;
                case < 80:
                    upgrades.Add(new DamageUpgrade());
                    break;
                case < 95:
                    upgrades.Add(new HealthUpgrade());
                    break;
                default:
                    upgrades.Add(new BulletsUpgrade());
                    break;
            }
        }

        return upgrades;
    }
}

public abstract class Upgrade
{
    protected readonly float Bonus;

    public Upgrade()
    {
        do
        {
            Bonus = (float)Math.Round(Random.Range(GetMin(), GetMax()), 2);
            Debug.Log(GetMin());
            Debug.Log(GetMax());
        } while (Bonus == 0);
    }

    public string BonusAsString()
    {
        return (Bonus > 0 ? "+" : "") + Bonus;
    }

    public abstract void ApplyToPlayer(PlayerData p);
    public abstract string GetMessage();
    public abstract Image GetImage();
    public abstract float GetMin();
    public abstract float GetMax();
}

public class PlayerSpeedUpgrade : Upgrade
{
    private const float MinVal = 0.3f;

    public override void ApplyToPlayer(PlayerData p)
    {
        p.MovementSpeed += Bonus;

        if (p.MovementSpeed < MinVal)
        {
            p.MovementSpeed = MinVal;
        }
    }

    public override string GetMessage()
    {
        return "Player speed " + BonusAsString();
    }

    public override Image GetImage()
    {
        throw new NotImplementedException();
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

public class RotationSpeedUpgrade : Upgrade
{
    private const float MinVal = 0.2f;

    public override void ApplyToPlayer(PlayerData p)
    {
        p.TurningSpeed += Bonus;

        if (p.TurningSpeed < MinVal)
        {
            p.TurningSpeed = MinVal;
        }
    }

    public override string GetMessage()
    {
        return "Rotation speed " + BonusAsString();
    }

    public override Image GetImage()
    {
        throw new NotImplementedException();
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

    public override string GetMessage()
    {
        return "Fire interval " + BonusAsString() + "s";
    }

    public override Image GetImage()
    {
        throw new NotImplementedException();
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

    public override string GetMessage()
    {
        return "Range " + BonusAsString();
    }

    public override Image GetImage()
    {
        throw new NotImplementedException();
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

    public override string GetMessage()
    {
        return "Damage " + BonusAsString();
    }

    public override Image GetImage()
    {
        throw new NotImplementedException();
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

    public override string GetMessage()
    {
        return "Max health " + BonusAsString();
    }

    public override Image GetImage()
    {
        throw new NotImplementedException();
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

public class BulletsUpgrade : Upgrade
{
    private const int MinVal = 1;

    public override void ApplyToPlayer(PlayerData p)
    {
        p.BulletsPerShot += (Bonus > 0 ? 1 : -1);

        if (p.BulletsPerShot < MinVal)
        {
            p.BulletsPerShot = MinVal;
        }
    }

    public override string GetMessage()
    {
        return "Bullets per shot " + (Bonus > 0 ? "+" : "-") + "1";
    }

    public override Image GetImage()
    {
        throw new NotImplementedException();
    }

    public override float GetMin()
    {
        return -1f;
    }

    public override float GetMax()
    {
        return 1f;
    }
}