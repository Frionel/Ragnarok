using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private DestinationMark _markPrefab;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    private DestinationMark _currentMark;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToDestination();
        }
    }

    void MoveToDestination()
    {
        var clickRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(clickRay, out RaycastHit hit))
        {
            _navMeshAgent.SetDestination(hit.point);

            UpdateSprite(hit.point);
            SpawnMark(hit.point);
        }
    }

    void UpdateSprite(Vector3 point)
    {
        var direction = point.z > transform.position.z ? 0 : 1;
        _animator.SetInteger("Direction", direction);

        var currentScale = _animator.transform.localScale;
        var goingRight = point.x > transform.position.x; 
        _animator.transform.localScale = new Vector3(Math.Abs(currentScale.x) * (goingRight ? -1 : 1), currentScale.y, currentScale.z);
    }

    void SpawnMark(Vector3 point)
    {
        if (_currentMark != null)
        {
            Destroy(_currentMark.gameObject);
        }
        else
        {
            _animator.SetTrigger("StartMoving");
        }
        
        _currentMark = Instantiate(_markPrefab, point, Quaternion.identity);
        _currentMark.Init(this);
    }

    public void OnReachedDestination()
    {
        _currentMark = null;
        _animator.SetTrigger("StopMoving");
    }
}
