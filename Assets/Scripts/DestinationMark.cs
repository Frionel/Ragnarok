
using UnityEngine;
using DG.Tweening;

public class DestinationMark : MonoBehaviour
{
    private PlayerController _player;
    
    public void Init(PlayerController player)
    {
        _player = player;
    }
    
    void Start()
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.3f).SetEase(Ease.InQuad));
        seq.Append(transform.DOScale(transform.localScale, 0.3f).SetEase(Ease.InQuad));
        seq.SetLoops(-1);
        seq.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject)
        {
            _player.OnReachedDestination();
            Destroy(gameObject);
        }
    }
}
