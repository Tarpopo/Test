using UnityEngine;

public interface IMove
{
    void Move(Vector2 direction, float moveSpeed);
    void StopMove();
}