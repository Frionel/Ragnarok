using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpMapController : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Transform _spawnPlayer;
    [SerializeField] private MapController _destinationMapPrefab;
    [SerializeField] private int _destinationWarpId;

    public int Id => _id;
    public Transform SpawnPlayer => _spawnPlayer;
    public Action<WarpMapController> EnteredWarp;
    public MapController DestinationMapPrefab => _destinationMapPrefab;
    public int DestinationWarpId => _destinationWarpId;
}
