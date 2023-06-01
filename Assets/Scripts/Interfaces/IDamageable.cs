using System;

public interface IDamageable
{
    public event Action OnTakeDamage;
    void TakeDamage(int damage);
}