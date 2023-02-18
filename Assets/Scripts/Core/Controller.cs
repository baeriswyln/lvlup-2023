using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class Controller : MonoBehaviour
    {
        public Transform spawnArea;
        public Player playerPrefab;

        [Header("UI Elements")] public GameObject pause;
        public GameObject levelUp;
        public GameObject end;
        public GameObject scores;
        public TextMeshProUGUI scorePrefab;

        private List<PlayerData> _deathOrder;


        // Start is called before the first frame update
        private void Start()
        {
            StartRound();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        public void StartRound()
        {
            levelUp.SetActive(false);
            _deathOrder = new List<PlayerData>();

            // remove all trailing players from a previous round
            foreach (var p in GameObject.FindObjectsByType<Player>(FindObjectsSortMode.None))
            {
                Destroy(p.gameObject);
            }


            // exit in case no players must be spawned (allows prototyping scene to work)
            if (Globals.PlayersToSpawn == null) return;

            // spawn players at random positions inside the spawn area
            foreach (var p in Globals.PlayersToSpawn)
            {
                var dx = spawnArea.localScale.x / 2;
                var dy = spawnArea.localScale.y / 2;

                var randomPosition = new Vector3(Random.Range(-dx, dx), Random.Range(-dy, dy), 0);
                var randomRotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
                var newPlayer = Instantiate(playerPrefab, randomPosition, randomRotation);
                newPlayer.sprite.transform.Rotate(-randomRotation.eulerAngles);
                newPlayer.healthBar.transform.Rotate(-randomRotation.eulerAngles);

                newPlayer.Initialize(p);
            }

            Time.timeScale = 1;
        }

        public void TogglePause()
        {
            Time.timeScale = pause.activeInHierarchy ? 1 : 0;
            pause.SetActive(!pause.activeInHierarchy);
        }

        public void Resume()
        {
            Time.timeScale = 1;
            pause.SetActive(false);
        }

        public void EndRound()
        {
            Time.timeScale = 0;
            // levelUp.SetActive(true);

            var txt = Instantiate(scorePrefab, scores.transform);
            txt.SetText("1. " + GetWinner().Name);

            for (var i = _deathOrder.Count - 1; i >= 0; i--)
            {
                txt = Instantiate(scorePrefab, scores.transform);
                txt.SetText(_deathOrder.Count - i + ". " + _deathOrder[i].Name);
            }

            var entrySize = txt.GetComponent<RectTransform>().sizeDelta;
            scores.GetComponent<RectTransform>().sizeDelta = new Vector2(100, entrySize.y * Globals.PlayersToSpawn.Count);

            end.SetActive(true);
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene(Globals.Scenes.Menu);
        }

        public void AnnounceDeath(Player p)
        {
            _deathOrder.Add(p.GetData());
            Destroy(p.gameObject);

            if (_deathOrder.Count + 1 >= Globals.PlayersToSpawn.Count)
            {
                EndRound();
            }
        }

        private PlayerData GetWinner()
        {
            foreach (var p in Globals.PlayersToSpawn)
            {
                if (!_deathOrder.Contains(p))
                {
                    return p;
                }
            }

            return _deathOrder[^1];
        }
    }
}