using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _system;
    [SerializeField] private ParticlesPool particlesPool;
    [SerializeField] private float _lifeTime;
    private readonly Timer _timer = new Timer();

    public void SetParticle(Vector3 position)
    {
        transform.position = position;
        _system.Play();
        _timer.StartTimer(_lifeTime, () => particlesPool.Return(this));
    }

    private void Update() => _timer.UpdateTimer();
}