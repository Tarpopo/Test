using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Spike : BaseTrap
{
    [SerializeField] private Collider _collider;
    [SerializeField] private float _coolDown;
    [SerializeField] private float _moveDuration;
    [SerializeField] private Transform _cone;
    [SerializeField] private Vector3 _deactivePosition;

    private void OnEnable() => StartCoroutine(SpikeRoutine());

    private void OnDisable() => StopAllCoroutines();

    private IEnumerator SpikeRoutine()
    {
        while (true)
        {
            yield return Helpers.GetWait(_coolDown);
            yield return _cone.DOLocalMove(_deactivePosition, _moveDuration).WaitForCompletion();
            _collider.enabled = false;
            yield return Helpers.GetWait(_coolDown);
            _collider.enabled = true;
            yield return _cone.DOLocalMove(Vector3.zero, _moveDuration).WaitForCompletion();
        }
    }
}