using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace.Menus
{
    public class PlayerConfigurator : MonoBehaviour
    {
        public RawImage Portrait;
        public TextMeshProUGUI Color;
        public TextMeshProUGUI Name;

        public TextMeshProUGUI TurnRight;
        public TextMeshProUGUI TurnLeft;
        [FormerlySerializedAs("TurnAction1")] public TextMeshProUGUI Action1;
        [FormerlySerializedAs("TurnActtion2")] public TextMeshProUGUI Action2;
    }
}