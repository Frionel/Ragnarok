using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private Vector3 _playerOffset;
    
    void Awake()
    {
        _playerOffset = transform.position - _player.transform.position;
    }

    void Update()
    {
        transform.position = _playerOffset + _player.transform.position;
    }
}
