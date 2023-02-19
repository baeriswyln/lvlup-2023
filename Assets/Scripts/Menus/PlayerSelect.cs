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
        public Button ButtonStart;
        public Button ButtonYellow;
        public Button ButtonOrange;
        public Button ButtonRed;
        public Button ButtonBlue;
        public Button ButtonPurple;
        public Button ButtonGreen;

        private int _playerCount = Globals.PlayersMin;
        private List<PlayerConfigurator> _players;
        public List<GameObject> _playersRows;

        private void Start()
        {
            _players = new List<PlayerConfigurator>();

            for (int i = 0; i < Globals.Players.Count; i++)
            {
                
            }

            // TODO : 
            for (int i = 0; i < Globals.PlayersMax; i++)
            {
                var playerSelector = Instantiate(PlayerPrefab, PlayerContainer.transform);
                _players.Add(playerSelector);
                playerSelector.Portrait.sprite = Globals.Players[i].Item3;
                playerSelector.Color.SetText(Globals.Players[i].Item2);
                playerSelector.TurnLeft.SetText(Globals.KeyMaps[i][Globals.KeyFunctions.TurnLeft].ToString());
                playerSelector.TurnRight.SetText(Globals.KeyMaps[i][Globals.KeyFunctions.TurnRight].ToString());
                playerSelector.Action1.SetText(Globals.KeyMaps[i][Globals.KeyFunctions.Action1].ToString());
                playerSelector.Action2.SetText(Globals.KeyMaps[i][Globals.KeyFunctions.Action2].ToString());
            }

            UpdateNumberOfPlayers();
        }

        private void UpdateNumberOfPlayers()
        {
            if (_playerCount < Globals.PlayersMin) _playerCount = Globals.PlayersMin;
            
            TextNumberOfPlayers.text = _playerCount.ToString();

            ButtonIncreaseNbrPlayers.enabled = _playerCount < Globals.PlayersMax;
            ButtonDecreaseNbrPlayers.enabled = _playerCount > Globals.PlayersMin;
            
            for (int i = 0; i < _playersRows.Count; i++)
            {
                _playersRows[i].SetActive(i-1 <= _playerCount);
            }

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
                Globals.PlayersToSpawn.Add(new PlayerData(Globals.KeyMaps[i], Globals.Players[i].Item1, Globals.Players[i].Item2, Globals.Players[i].Item4));
            }

            SceneManager.LoadScene(Globals.Scenes.Game);
        }
    }
}