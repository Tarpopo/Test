using Scriptables.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/" + nameof(OnDraw))]
public class OnDraw : BaseEventSO<Vector2[]>
{
}