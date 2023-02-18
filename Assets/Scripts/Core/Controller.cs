using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Menus;
using Objects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core
{
    public class Controller : MonoBehaviour
    {
        public Player playerPrefab;

        [Header("UI Elements")] public GameObject pause;
        public GameObject levelUp;
        public GameObject end;
        public GameObject scores;
        public TextMeshProUGUI scorePrefab;
        public TextMeshProUGUI playerToUpgrade;
        public PlayerStats playerStats;
        public UpgradePrefab upgradePrefab;
        public Transform upgradeContainer;

        public List<Transform> spawnPoints;
        
        private List<PlayerData> _deathOrder;
        private int _nextPlayerToSelectUpgrade;


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
            
            // remove all trailing bullets from a previous round
            foreach (var p in GameObject.FindObjectsByType<Bullet>(FindObjectsSortMode.None))
            {
                Destroy(p.gameObject);
            }


            // exit in case no players must be spawned (allows prototyping scene to work)
            if (Globals.PlayersToSpawn == null) return;

            // spawn players at random positions inside the spawn area
            List<int> usedIdx = new List<int>();
            foreach (var p in Globals.PlayersToSpawn)
            {
                int idx;
                do {
                    idx = Random.Range(0, spawnPoints.Count);
                } while (usedIdx.Contains(idx));
                
                var randomPosition = spawnPoints[idx].position;
                var randomRotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
                var newPlayer = Instantiate(playerPrefab, randomPosition, randomRotation);
                newPlayer.sprite.transform.Rotate(-randomRotation.eulerAngles);
                newPlayer.bars.transform.Rotate(-randomRotation.eulerAngles);
                
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

            _deathOrder[^1].Points++;
            if (_deathOrder[^1].Points >= Globals.WinningPoints)
            {
                ShowFinalScore();
                return;
            }

            ShowUpgradeScreen();
        }

        private void ShowUpgradeScreen()
        {
            levelUp.SetActive(true);

            var upgrades = PlayerUpgrades.GenerateUpgrades(Globals.PlayersToSpawn.Count);
            playerStats.SetStats(_deathOrder[0]);
            _nextPlayerToSelectUpgrade = 0;

            foreach (var upgrade in upgrades)
            {
                var upgradeObj = Instantiate(upgradePrefab, upgradeContainer);
                upgradeObj.description.SetText(upgrade.GetMessage());
                upgradeObj.img.sprite = upgrade.GetImage();
                upgradeObj.btn.onClick.AddListener(() => UpgradeSelected(upgrade, upgradeObj));
            }
        }

        private void UpgradeSelected(Upgrade upgrade, UpgradePrefab upgradeGameObj)
        {
            upgrade.ApplyToPlayer(_deathOrder[_nextPlayerToSelectUpgrade]);
            Destroy(upgradeGameObj.gameObject);

            _nextPlayerToSelectUpgrade++;

            if (_nextPlayerToSelectUpgrade >= Globals.PlayersToSpawn.Count)
            {
                Globals.PlayersToSpawn = _deathOrder;
                StartRound();
                return;
            }

            playerStats.SetStats(_deathOrder[_nextPlayerToSelectUpgrade]);
        }

        public void ShowFinalScore()
        {
            TextMeshProUGUI txt = null;

            var scoreboard = _deathOrder.OrderBy(p => p.Points).ToList();

            for (var i = scoreboard.Count - 1; i >= 0; i--)
            {
                txt = Instantiate(scorePrefab, scores.transform);
                txt.SetText(scoreboard[i].Name + " - " + scoreboard[i].Points + " wins");
            }

            var entrySize = txt.GetComponent<RectTransform>().sizeDelta;
            scores.GetComponent<RectTransform>().sizeDelta =
                new Vector2(100, entrySize.y * Globals.PlayersToSpawn.Count);

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
                if (_deathOrder.Count < Globals.PlayersToSpawn.Count)
                {
                    _deathOrder.Add(GetWinner());
                }

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