using System;
using TMPro;
using UnityEngine;

namespace Core
{
    public class Countdown : MonoBehaviour
    {
        public TextMeshProUGUI text;

        private const int MaxTime = 3;
        private int lastValShown;
        private float startTime;

        private void OnEnable()
        {
            text.SetText(MaxTime + "");
            startTime = 0;
            lastValShown = MaxTime;
        }

        private void Update()
        {
            var timeDiff = MaxTime - (int)(Time.unscaledTime - startTime);

            if (startTime == 0) startTime = Time.unscaledTime;
            if (timeDiff == lastValShown) return;

            switch (timeDiff)
            {
                case 0:
                    text.SetText("GO!");
                    break;
                case -1:
                    Time.timeScale = 1;
                    gameObject.SetActive(false);
                    break;
                default:
                    lastValShown = timeDiff;
                    text.SetText(lastValShown + "");
                    break;
            }
        }
    }
}