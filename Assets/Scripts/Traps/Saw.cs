using UnityEngine;

public class Saw : BaseTrap
{
    [SerializeField] private Vector3 _rotateAxis;
    [SerializeField] private float _rotateSpeed;

    private void Update() => transform.Rotate(_rotateAxis * (_rotateSpeed * Time.deltaTime));
}