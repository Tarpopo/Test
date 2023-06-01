using UnityEngine;

public static class RigidbodyExtensions
{
    public static void SetGravity(this Rigidbody rigidbody, bool active)
    {
        rigidbody.useGravity = active;
        rigidbody.isKinematic = !active;
    }
}