using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Menus
{
    public class PlayerIcon : MonoBehaviour
    {
        
        public TextMeshProUGUI IconName;
        public Image Portrait;
        
        public string AvatarName;
        public Sprite AvatarIcon;
        private void Start()
        {
            IconName.text = AvatarName;
            Portrait.sprite = AvatarIcon;
        }

        public void OnChoosed()
        {
            Portrait.tintColor = Color.black; // TODO
        }

    }
}