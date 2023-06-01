using UnityEngine;

public class RigidbodyMove : IMove
{
    private readonly Rigidbody2D _rigidbody2D;

    public RigidbodyMove(Rigidbody2D rigidbody2D) => _rigidbody2D = rigidbody2D;

    public void Move(Vector2 direction, float moveSpeed) =>
        _rigidbody2D.MovePosition(_rigidbody2D.position + direction * moveSpeed);

    public void StopMove() => _rigidbody2D.velocity = Vector2.zero;
}