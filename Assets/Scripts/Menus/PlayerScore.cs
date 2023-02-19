using Core;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class PlayerScore : MonoBehaviour
    {
        public TextMeshProUGUI playerName;
        public TextMeshProUGUI subtitle;
        public TextMeshProUGUI index;
        public TextMeshProUGUI score;
        public Image portrait;

        public void Show(PlayerData p, int i)
        {
            if (i == 1) subtitle.SetText("The good");
            else if (i == Globals.PlayersToSpawn.Count) subtitle.SetText("The bad");
            else subtitle.SetText("The ugly");
            
            playerName.SetText(p.Name);
            index.SetText(i.ToString());
            score.SetText(p.Points + "/" + Globals.WinningPoints);
            portrait.sprite = p.Image;
        }
    }
}