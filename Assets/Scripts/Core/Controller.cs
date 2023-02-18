using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Core
{
    public class Controller : MonoBehaviour
    {
        public Transform spawnArea;
        public Player playerPrefab;

        private List<PlayerData> _deathOrderIndexes;
    
    
        // Start is called before the first frame update
        private void Start()
        {
            StartRound();
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        private void StartRound()
        {
            _deathOrderIndexes = new List<PlayerData>();

            if (Globals.PlayersToSpawn == null)
            {
                return;
            }
            
            // spawn players
            foreach (var p in Globals.PlayersToSpawn)
            {
                var dx = spawnArea.localScale.x / 2;
                var dy = spawnArea.localScale.y / 2;
                
                var randomPosition = new Vector3(Random.Range(-dx, dx), Random.Range(-dy, dy), 0);
                var randomRotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
                var newPlayer = Instantiate(playerPrefab, randomPosition, randomRotation);

                newPlayer.Initialize(p);
            }
        }

        private void EndRound()
        {
            
        }

        public void AnnounceDeath(Player p)
        {
            _deathOrderIndexes.Add(p.GetData());
            Destroy(p);
        }
    }
}
