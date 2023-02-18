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
        public PlayerConfigurator PlayerPrefab;

        public Button ButtonIncreaseNbrPlayers;
        public Button ButtonDecreaseNbrPlayers;

        private int _playerCount = Globals.PlayersMin;
        private List<PlayerConfigurator> _players;

        private void Start()
        {
            _players = new List<PlayerConfigurator>();

            for (int i = 0; i < Globals.PlayersMax; i++)
            {
                var playerSelector = Instantiate(PlayerPrefab, PlayerContainer.transform);
                _players.Add(playerSelector);
                playerSelector.Color.SetText(Globals.PlayerColors[i].Item2);
                playerSelector.TurnLeft.SetText(Globals.KeyMaps[i][Globals.KeyFunctions.TurnLeft].ToString());
                playerSelector.TurnRight.SetText(Globals.KeyMaps[i][Globals.KeyFunctions.TurnRight].ToString());
                playerSelector.Action1.SetText(Globals.KeyMaps[i][Globals.KeyFunctions.Action1].ToString());
                playerSelector.Action2.SetText(Globals.KeyMaps[i][Globals.KeyFunctions.Action2].ToString());
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
                _players[i].gameObject.SetActive(i < _playerCount);
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

            for (var i = 0; i < _playerCount; i++)
            {
                Globals.PlayersToSpawn.Add(new PlayerData(Globals.KeyMaps[i], Globals.PlayerColors[i].Item1, Globals.PlayerColors[i].Item2));
            }

            SceneManager.LoadScene(Globals.Scenes.Game);
        }
    }
}