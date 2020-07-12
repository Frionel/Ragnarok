using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawnController : MonoBehaviour
{
    [Serializable]
    public class SpotConfig
    {
        [Serializable]
        public class Range
        {
            public int Start;
            public int End;
        }
        
        public Transform Parent;
        public Range SpawnFrequency = new Range {Start = 2, End = 5};
        public int MaxAmountAllowed = 3;
    }
    
    [SerializeField] List<SpotConfig> _spawnSpots;
    [SerializeField] List<GameObject> _enemiesPrefabs;
    
    void Start()
    {
        StartSpawning();
    }

    void StartSpawning()
    {
        foreach (var spot in _spawnSpots)
        {
            StartCoroutine(Spawn(spot));
        }
    }

    IEnumerator Spawn(SpotConfig spot)
    {
        while (true)
        {
            while (spot.Parent.childCount >= spot.MaxAmountAllowed)
            {
                yield return new WaitForSeconds(1);
            }
            
            yield return new WaitForSeconds(Random.Range(spot.SpawnFrequency.Start, spot.SpawnFrequency.End));

            var enemyIndex = Random.Range(0, _enemiesPrefabs.Count);
            var enemyPrefab = _enemiesPrefabs[enemyIndex];
            
            var enemy = Instantiate(enemyPrefab, spot.Parent);
            enemy.GetComponent<NavMeshAgent>().Warp(spot.Parent.position);
        }
    }
}
