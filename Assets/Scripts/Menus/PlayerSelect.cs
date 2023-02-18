using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.Menus
{
    public class PlayerSelect : MonoBehaviour
    {
        public TextMeshProUGUI TextNumberOfPlayers;
        public GameObject PlayerContainer;
        public GameObject PlayerPrefab;

        public Button ButtonIncreaseNbrPlayers;
        public Button ButtonDecreaseNbrPlayers;
        
        private int _playerCount = Globals.PlayersMin;
        private List<GameObject> _players;

        private void Start()
        {
            _players = new List<GameObject>();

            for (int i = 0; i < Globals.PlayersMax; i++)
            {
                var playerSelector = Instantiate(PlayerPrefab, PlayerContainer.transform);
                _players.Add(Instantiate(PlayerPrefab, PlayerContainer.transform));
                // playerSelector.transform.Get
            }
            
            UpdateNumberOfPlayers();
        }

        private void UpdateNumberOfPlayers()
        {
            TextNumberOfPlayers.text = _playerCount.ToString();
            
            ButtonIncreaseNbrPlayers.enabled = _playerCount < Globals.PlayersMax;
            ButtonDecreaseNbrPlayers.enabled = _playerCount > Globals.PlayersMin;
            
            for (int i = 0; i < Globals.PlayersMax; i++)
            {
                _players[i].active = i < _playerCount;
            }
        }

        public void IncreasePlayerNumber()
        {
            _playerCount++;
            UpdateNumberOfPlayers();
        }

        public void DecreasePlayerNumber()
        {
            _playerCount--;
            UpdateNumberOfPlayers();
        }

        public void StartGame()
        {
            Globals.PlayersToSpawn = new List<PlayerData>();
            
            for(var i = 0; i < _playerCount; i++)
            {
                Globals.PlayersToSpawn.Add(new PlayerData(Globals.KeyMaps[i], Globals.PlayerColors[i]));
            }
            
            SceneManager.LoadScene(Globals.Scenes.Game);
        }
    }
}