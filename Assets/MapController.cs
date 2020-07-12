using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private List<WarpMapController> _warps;

    private bool _initWarped;
    public Action<MapController, int> EnteredWarp;

    void Awake()
    {
        if (!_initWarped)
        {
            WarpTo(0);
        }
        
        foreach (var warp in _warps)
        {
            warp.EnteredWarp += x =>
            {
                EnteredWarp?.Invoke(x.DestinationMapPrefab, x.DestinationWarpId);
            };
        }
    }

    public void WarpTo(int warpId)
    {
        var warp = _warps.Find(x => x.Id == warpId);
        _player.NavMeshAgent.Warp(warp.SpawnPlayer.position);
        _initWarped = true;
    }
}
