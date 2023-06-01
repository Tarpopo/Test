using UnityEngine;

public abstract class BaseTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable) == false) return;
        damageable.TakeDamage(1);
    }
}