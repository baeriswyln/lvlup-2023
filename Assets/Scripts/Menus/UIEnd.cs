using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Menus
{
    public class UIEnd : MonoBehaviour
    {
        public PlayerScore first;
        public PlayerScore second;
        public PlayerScore third;
        public Transform containerRemainingScores;

        [Header("Prefabs")] public PlayerScore playerScore;

        public void Show(List<PlayerData> scoreboard)
        {
            var l = scoreboard.Count - 1;
            
            first.Show(scoreboard[l], 1);
            second.Show(scoreboard[l-1], 2);

            if (scoreboard.Count > 2) third.Show(scoreboard[l-3], 3);
            third.gameObject.SetActive(scoreboard.Count > 2);

            // remove all remaining scores
            foreach (Transform child in containerRemainingScores) Destroy(child.gameObject);

            for (var i = 3; i < scoreboard.Count; i++)
            {
                Instantiate(playerScore, containerRemainingScores).Show(scoreboard[l-i], i + 1);
            }
        }
    }
}