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
            first.Show(scoreboard[0], 1);
            second.Show(scoreboard[1], 2);

            if (scoreboard.Count > 2) third.Show(scoreboard[1], 3);
            third.gameObject.SetActive(scoreboard.Count > 2);

            // remove all remaining scores
            foreach (Transform child in containerRemainingScores) Destroy(child.gameObject);

            for (var i = 3; i < scoreboard.Count; i++)
            {
                Instantiate(playerScore, containerRemainingScores).Show(scoreboard[i], i + 1);
            }
        }
    }
}