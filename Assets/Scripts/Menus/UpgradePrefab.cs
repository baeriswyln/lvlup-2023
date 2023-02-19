using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Menus
{
    public class UpgradePrefab : MonoBehaviour
    {
        public Image img;
        public Image bgRed;
        public Image bgGreen;
        public TextMeshProUGUI description;
        public TextMeshProUGUI value;
        public Button btn;

        public void Show(Upgrade u)
        {
            img.sprite = u.GetImage();
            description.SetText(u.GetName());
            value.SetText(u.BonusAsString());

            if (u.Bonus > 0) bgGreen.gameObject.SetActive(true);
            else bgRed.gameObject.SetActive(true);
        }
    }
}