using System.Collections.Generic;
using Core;
using Objects;
using UnityEngine;

namespace DefaultNamespace
{
    public static class Globals
    {
        public const float SpeedAdjFactor = 5;

        public const int PlayersMin = 2;
        public const int PlayersMax = 6;

        public const int WinningPoints = 3;
        public const int ShootingAngleDelta = 10;

        public const string PlayerHeadSprite = "Head";
        public const string Controller = "Controller";

        public static List<PlayerData> PlayersToSpawn;

        public static class KeyFunctions
        {
            public const string TurnLeft = "tl";
            public const string TurnRight = "tr";
            public const string Shoot = "a1";
            public const string Ability = "a2";
        }

        public static readonly List<(Color, string, Sprite, Weapon.WeaponType)> Players = new()
        {
            (new Color(236f/255, 226f/255, 40f/255,1), "Jesse Lasso", Resources.Load<Sprite>("icons/yellow_portrait"), Weapon.WeaponType.Pistol),
            (new Color(224f/255, 121f/255, 27f/255,1), "Wyatt Granger", Resources.Load<Sprite>("icons/orange_portrait"), Weapon.WeaponType.LongRange),
            (new Color(226f/255, 39f/255, 28f/255,1), "Silas McCoy", Resources.Load<Sprite>("icons/red_portrait"), Weapon.WeaponType.Shotgun),
            (new Color(58f/255, 156f/255, 218f/255,1), "Cheyenne Parker", Resources.Load<Sprite>("icons/blue_portrait"), Weapon.WeaponType.LongRange),
            (new Color(155f/255, 38f/255, 227f/255,1), "Savannah West", Resources.Load<Sprite>("icons/purple_portrait"), Weapon.WeaponType.Pistol),
            (new Color(38f/255, 198f/255, 17f/255,1), "Abigail Yates", Resources.Load<Sprite>("icons/green_portrait"), Weapon.WeaponType.Shotgun),
        };

        public static readonly List<Dictionary<string, KeyCode>> KeyMaps = new()
        {
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.Q,
                [KeyFunctions.TurnRight] = KeyCode.E,
                [KeyFunctions.Shoot] = KeyCode.Alpha2,
                [KeyFunctions.Ability] = KeyCode.W
            },
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.R,
                [KeyFunctions.TurnRight] = KeyCode.Y,
                [KeyFunctions.Shoot] = KeyCode.Alpha5,
                [KeyFunctions.Ability] = KeyCode.T
            },
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.U,
                [KeyFunctions.TurnRight] = KeyCode.O,
                [KeyFunctions.Shoot] = KeyCode.Alpha8,
                [KeyFunctions.Ability] = KeyCode.I
            },
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.Z,
                [KeyFunctions.TurnRight] = KeyCode.C,
                [KeyFunctions.Shoot] = KeyCode.S,
                [KeyFunctions.Ability] = KeyCode.X
            },
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.V,
                [KeyFunctions.TurnRight] = KeyCode.N,
                [KeyFunctions.Shoot] = KeyCode.G,
                [KeyFunctions.Ability] = KeyCode.B
            },
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.M,
                [KeyFunctions.TurnRight] = KeyCode.Period,
                [KeyFunctions.Shoot] = KeyCode.K,
                [KeyFunctions.Ability] = KeyCode.Comma
            }
        };
        
        
        public static readonly Dictionary<Weapon.WeaponType, Weapon.InitValues> InitWeaponValues = new()
        {
            [Weapon.WeaponType.Pistol] = new Weapon.InitValues(10, 1, 1, 1),
            [Weapon.WeaponType.LongRange] = new Weapon.InitValues(20, 3, 1, 4),
            [Weapon.WeaponType.Shotgun] = new Weapon.InitValues(5, 0.2f, 5, 2)
        };

        public static class InitPlayerVals
        {
            public const float MovementSpeed = 1;
            public const float TurningSpeed = 1;
            public const float Health = 2;
        }

        public static class Scenes
        {
            public const string Game = "Game";
            public const string Menu = "MainMenu";
            public const string Title = "Title";
            public const string About = "About";
        }
    }
}