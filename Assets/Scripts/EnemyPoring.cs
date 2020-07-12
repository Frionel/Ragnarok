using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyPoring : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private float AttackRange = 1.0f;
    [SerializeField] private SphereCollider _detectCollider;

    private PlayerController _player;
    private IEnumerator _moveRandomlyEnumerator;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        StartMovingRandomly();
    }

    void StartMovingRandomly()
    {
        _moveRandomlyEnumerator = MoveRandomly();
        StartCoroutine(_moveRandomlyEnumerator);
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            
            var randomPos = transform.position + new Vector3(Random.Range(-10, 10), 0.0f, Random.Range(-10, 10));
            GoToPosition(randomPos);
            yield return new WaitForSeconds(2);
            _navMeshAgent.isStopped = true;
        }
    }

    void GoToPosition(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);
        _navMeshAgent.isStopped = false;
        UpdateSprite(destination);
    }
    
    void UpdateSprite(Vector3 point)
    {
        var direction = point.z > transform.position.z ? 0 : 1;
        _animator.SetInteger("Direction", direction);

        var currentScale = _animator.transform.localScale;
        var goingRight = point.x > transform.position.x; 
        _animator.transform.localScale = new Vector3(Math.Abs(currentScale.x) * (goingRight ? -1 : 1), currentScale.y, currentScale.z);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _player.gameObject)
        {
            var dist = Vector3.Distance(_player.transform.position, transform.position);

            if (dist < AttackRange)
            {
                Destroy(gameObject);
            }
            else
            {
                StopCoroutine(_moveRandomlyEnumerator);
                GoToPosition(_player.transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player.gameObject)
        {
            StartMovingRandomly();
        }
    }
}
