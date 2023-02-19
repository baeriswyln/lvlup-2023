using DefaultNamespace.Menus;
using UnityEngine;

namespace Menus
{
    public class UILevelUp : MonoBehaviour
    {
        [Header("Existing containers")]
        public PlayerStats playerStats;
        public Transform upgradeContainer;
        public Transform playerContainer;
        
        [Header("Prefabs")]
        public PlayerScore playerScore;
        public UpgradePrefab upgradePrefab;

    }
}
