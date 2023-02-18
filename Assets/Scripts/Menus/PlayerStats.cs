using Core;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Menus
{
    public class PlayerStats : MonoBehaviour
    {
        public TextMeshProUGUI playerName;
        public TextMeshProUGUI maxHealth;
        public TextMeshProUGUI movementSpeed;
        public TextMeshProUGUI rotationSpeed;
        public TextMeshProUGUI damage;
        public TextMeshProUGUI range;
        public TextMeshProUGUI fireInterval;
        public TextMeshProUGUI bulletsPerShot;

        public void SetStats(PlayerData p)
        {
            playerName.SetText(p.Name);
            maxHealth.SetText(p.InitialHealth.ToString());
            movementSpeed.SetText(p.MovementSpeed.ToString());
            rotationSpeed.SetText(p.TurningSpeed.ToString());
            damage.SetText(p.FireDamage.ToString());
            range.SetText(p.FireRange.ToString());
            fireInterval.SetText(p.FireInterval.ToString());
            bulletsPerShot.SetText(p.BulletsPerShot.ToString());
        }
    }
}