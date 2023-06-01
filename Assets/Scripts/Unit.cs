using System;
using DG.Tweening;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable
{
    public event Action OnTakeDamage;
    public event Action<Unit> OnDead;
    public event Action<Unit> OnTakeUnit;
    [SerializeField] private float _duration;
    [SerializeField] private UnitPool _unitPool;
    [SerializeField] private ParticlesPool _particles;

    private void Start() => _particles.Load();

    public void MoveToLocalPoint(Vector3 point) => transform.DOLocalMove(point, _duration);

    public void StackUnit(Transform parent)
    {
        transform.SetParent(parent);
        _particles.Get().SetParticle(transform.position);
    }

    public void TakeDamage(int damage)
    {
        OnTakeDamage?.Invoke();
        OnDead?.Invoke(this);
        _unitPool.Return(this);
        _particles.Get().SetParticle(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Unit>(out var unit) == false) return;
        OnTakeUnit?.Invoke(unit);
    }
}