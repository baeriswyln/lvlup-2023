using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Menus;
using Menus;
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

        [Header("UI Screens")] public GameObject pause;
        public UILevelUp levelUp;
        public GameObject end;
        public GameObject scores;
        public GameObject countdown;
        
        [Header("UI Elements")]
        public TextMeshProUGUI scorePrefab;
        public TextMeshProUGUI playerToUpgrade;

        public List<Transform> spawnPoints;
        
        private List<PlayerData> _deathOrder;
        private int _nextPlayerToSelectUpgrade;


        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(StartRound());
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        public IEnumerator StartRound()
        {
            yield return new WaitForSeconds(1.4f);
            Time.timeScale = 0;
            levelUp.gameObject.SetActive(false);
            _deathOrder = new List<PlayerData>();

            // remove all trailing players, bullets and particle effects from a previous round
            DestroyAll<Player>();
            DestroyAll<Bullet>();
            DestroyAll<ParticleSystem>();
            
            // remove particles
            // remove all trailing bullets from a previous round
            foreach (var p in GameObject.FindObjectsByType<ParticleSystem>(FindObjectsSortMode.None))
            {
                Destroy(p.gameObject);
            }


            // exit in case no players must be spawned (allows prototyping scene to work)
            if (Globals.PlayersToSpawn == null) yield return new WaitForEndOfFrame();

            // spawn players at random positions inside the spawn area
            List<int> usedIdx = new List<int>();
            foreach (var p in Globals.PlayersToSpawn)
            {
                int idx;
                do {
                    idx = Random.Range(0, spawnPoints.Count);
                } while (usedIdx.Contains(idx));
                
                usedIdx.Add(idx);
                
                var randomPosition = spawnPoints[idx].position;
                var randomRotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
                var newPlayer = Instantiate(playerPrefab, randomPosition, randomRotation);
                newPlayer.sprite.transform.Rotate(-randomRotation.eulerAngles);
                newPlayer.bars.transform.Rotate(-randomRotation.eulerAngles);
                
                newPlayer.Initialize(p);
            }
            
            // count down - sets the time scale to 1 at the end of the countdown
            countdown.SetActive(true);
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
            levelUp.gameObject.SetActive(true);
            
            // show the current leader board, sorted by the death order of the round that just finished
            for (var i = _deathOrder.Count - 1; i >= 0; i--)
            {
                Instantiate(levelUp.playerScore, levelUp.playerContainer).Show(_deathOrder[i], _deathOrder.Count - i);
            }

            // generate a list of upgrades
            var upgrades = PlayerUpgrades.GenerateUpgrades(Globals.PlayersToSpawn.Count);
            
            // make the first player select his upgrade
            levelUp.playerStats.SetStats(_deathOrder[0]);
            _nextPlayerToSelectUpgrade = 0;

            // show all upgrades
            foreach (var upgrade in upgrades)
            {
                var upgradeObj = Instantiate(levelUp.upgradePrefab, levelUp.upgradeContainer);
                upgradeObj.Show(upgrade);
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

            levelUp.playerStats.SetStats(_deathOrder[_nextPlayerToSelectUpgrade]);
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
        
        private static void DestroyAll<T>() where T : Component
        {
            foreach (var p in GameObject.FindObjectsByType<T>(FindObjectsSortMode.None))
            {
                Destroy(p.gameObject);
            }
        }
    }
}