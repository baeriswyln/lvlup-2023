using System.Collections.Generic;
using Core;
using UnityEngine;

namespace DefaultNamespace
{
    public static class Globals
    {
        public const float SpeedAdjFactor = 5;

        public const int PlayersMin = 2;
        public const int PlayersMax = 6;
        
        public const string PlayerHeadSprite = "Head";
        public const string Controller = "Controller";

        public static List<PlayerData> PlayersToSpawn;

        public static class KeyFunctions
        {
            public const string TurnLeft = "tl";
            public const string TurnRight = "tr";
            public const string Action1 = "a1";
            public const string Action2 = "a2";
        }

        public static List<Color> PlayerColors = new()
        {
            Color.black,
            Color.red,
            Color.green,
            Color.blue,
            Color.magenta,
            Color.yellow
        };

        public static List<Dictionary<string, KeyCode>> KeyMaps = new()
        {
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.Q,
                [KeyFunctions.TurnRight] = KeyCode.E,
                [KeyFunctions.Action1] = KeyCode.Alpha2,
                [KeyFunctions.Action2] = KeyCode.W
            },
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.R,
                [KeyFunctions.TurnRight] = KeyCode.Y,
                [KeyFunctions.Action1] = KeyCode.Alpha5,
                [KeyFunctions.Action2] = KeyCode.T
            },
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.U,
                [KeyFunctions.TurnRight] = KeyCode.O,
                [KeyFunctions.Action1] = KeyCode.Alpha8,
                [KeyFunctions.Action2] = KeyCode.I
            },
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.Z,
                [KeyFunctions.TurnRight] = KeyCode.C,
                [KeyFunctions.Action1] = KeyCode.S,
                [KeyFunctions.Action2] = KeyCode.X
            },
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.V,
                [KeyFunctions.TurnRight] = KeyCode.N,
                [KeyFunctions.Action1] = KeyCode.G,
                [KeyFunctions.Action2] = KeyCode.B
            },
            new Dictionary<string, KeyCode>()
            {
                [KeyFunctions.TurnLeft] = KeyCode.M,
                [KeyFunctions.TurnRight] = KeyCode.Period,
                [KeyFunctions.Action1] = KeyCode.K,
                [KeyFunctions.Action2] = KeyCode.Comma
            }
        };

        public static class Scenes
        {
            public const string Game = "Game";
            public const string Menu = "MainMenu";   
        }
    }
}