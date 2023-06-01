using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineDrawer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private OnDraw _onDraw;
    [SerializeField] private float _minActiveDistance;
    [SerializeField] private Camera _camera;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private RectTransform _rectTransform;
    private readonly List<Vector2> _fingerPositions = new List<Vector2>(100);
    private bool _isPointerDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
        CreateLine(TransformPoint(eventData.position));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
        _onDraw.Invoke(_fingerPositions.ToArray());
        _fingerPositions.Clear();
        _lineRenderer.positionCount = 2;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isPointerDown == false || ActiveDistance(TransformPoint(eventData.position)) == false) return;
        UpdateLine(TransformPoint(eventData.position));
    }

    private bool ActiveDistance(Vector2 currentPoint) =>
        Vector2.Distance(_fingerPositions[_fingerPositions.Count - 1], currentPoint) >= _minActiveDistance;

    private void UpdateLine(Vector2 newPosition)
    {
        _fingerPositions.Add(newPosition);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newPosition);
    }

    private void CreateLine(Vector2 fingerPoint)
    {
        _fingerPositions.Clear();
        _fingerPositions.Add(fingerPoint);
        _fingerPositions.Add(fingerPoint);
        _lineRenderer.SetPosition(0, _fingerPositions[0]);
        _lineRenderer.SetPosition(1, _fingerPositions[1]);
    }

    private Vector3 TransformPoint(Vector2 position)
    {
        // RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, position, _camera, out var point);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, position, _camera, out var point);
        return point;
    }
}