using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawPositionSetter : MonoBehaviour
{
    [SerializeField] private float _distanceScale;
    [SerializeField] private List<Unit> _units;
    [SerializeField] private OnDraw _onDraw;

    private void OnEnable() => _onDraw.Subscribe(SetItemsPositions);

    private void OnDisable() => _onDraw.Unsubscribe(SetItemsPositions);

    private void Awake()
    {
        foreach (var unit in _units)
        {
            unit.OnDead += RemoveUnit;
            unit.OnTakeUnit += AddUnit;
        }
    }

    private void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
        unit.OnDead -= RemoveUnit;
        unit.OnTakeUnit -= AddUnit;
    }

    private void AddUnit(Unit unit)
    {
        if (_units.Contains(unit)) return;
        unit.StackUnit(transform);
        _units.Add(unit);
        unit.OnDead += RemoveUnit;
        unit.OnTakeUnit += AddUnit;
    }

    private void SetItemsPositions(Vector2[] points)
    {
        if (points.Length < _units.Count || _units.Count <= 0) return;
        var step = points.Length / _units.Count;
        var currentStep = step;
        foreach (var unit in _units)
        {
            unit.MoveToLocalPoint(GetObjectPosition(unit.transform, points[currentStep]));
            currentStep = Math.Clamp(currentStep + step, 0, points.Length - 1);
        }
    }

    private Vector3 GetObjectPosition(Transform objectTransform, Vector2 point)
    {
        var position = point * _distanceScale;
        return new Vector3(position.x, objectTransform.localPosition.y, position.y);
    }
}