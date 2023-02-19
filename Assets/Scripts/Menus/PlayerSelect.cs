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
        public SceneLoader sceneLoader;

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
                playerSelector.Name.SetText(Globals.Players[i].Item2);
                playerSelector.Portrait.sprite = Globals.Players[i].Item3;
                playerSelector.TurnLeft.SetText(
                    GetShortKeyName(Globals.KeyMaps[i][Globals.KeyFunctions.TurnLeft].ToString()));
                playerSelector.TurnRight.SetText(
                    GetShortKeyName(Globals.KeyMaps[i][Globals.KeyFunctions.TurnRight].ToString()));
                playerSelector.Shoot.SetText(
                    GetShortKeyName(Globals.KeyMaps[i][Globals.KeyFunctions.Shoot].ToString()));
            }

            UpdateNumberOfPlayers();
        }

        private string GetShortKeyName(string name)
        {
            if (name.IndexOf("Alpha") >= 0)
            {
                return name.Split("Alpha")[1];
            }

            return name switch
            {
                "Z" => "Y",
                "Y" => "Z",
                "Period" => ".",
                "Comma" => ",",
                _ => name
            };
        }

        private void UpdateNumberOfPlayers()
        {
            if (_playerCount < Globals.PlayersMin) _playerCount = Globals.PlayersMin;

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
                Globals.PlayersToSpawn.Add(new PlayerData(Globals.KeyMaps[i], Globals.Players[i].Item1,
                    Globals.Players[i].Item2, Globals.Players[i].Item3, Globals.Players[i].Item4));
            }

            sceneLoader.LoadScene(Globals.Scenes.Game);
        }

        public void Back()
        {
            SceneManager.LoadScene(Globals.Scenes.Title);
        }
    }
}