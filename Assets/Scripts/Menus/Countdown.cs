using System;
using TMPro;
using UnityEngine;

namespace Core
{
    public class Countdown : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public AudioSource bip;
        public Controller controller;
        
        private const int MaxTime = 3;
        private int lastValShown;
        private float startTime;

        private bool running = false;

        private void OnEnable()
        {
            text.SetText(MaxTime + "");
        }

        public void StartCounting()
        {
            startTime = 0;
            lastValShown = MaxTime;
            running = true;
            bip.Play(0);
        }

        private void Update()
        {
            if (running)
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
                        controller.OnCountdownDone();
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
}