using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Vector3Extensions
{
    public static Vector3 WithX(this Vector3 value, float x)
    {
        value.x = x;
        return value;
    }

    public static Vector3 WithY(this Vector3 value, float y)
    {
        value.y = y;
        return value;
    }

    public static Vector3 WithZ(this Vector3 value, float z)
    {
        value.z = z;
        return value;
    }

    public static Vector3 AddX(this Vector3 value, float x)
    {
        value.x += x;
        return value;
    }

    public static Vector3 AddY(this Vector3 value, float y)
    {
        value.y += y;
        return value;
    }

    public static Vector3 AddZ(this Vector3 value, float z)
    {
        value.z += z;
        return value;
    }

    public static Vector2 To2(this Vector3 value) =>
        value;

    public static Vector3 ToPlaneX(this Vector3 value) =>
        new Vector3(0, value.y, value.z);

    public static Vector3 ToPlaneY(this Vector3 value) =>
        new Vector3(value.x, 0, value.z);

    public static Vector3 ToPlaneZ(this Vector3 value) =>
        new Vector3(value.x, value.y, 0);

    public static float Angle(this Vector3 from, Vector3 to) =>
        Vector3.Angle(from, to);

    public static Vector3 Project(this Vector3 vector, Vector3 normal) =>
        Vector3.Project(vector, normal);

    // public static Vector3 Abs(this Vector3 vector) =>
    //     new Vector3(vector.x.Abs(), vector.y.Abs(), vector.z.Abs());
    public static T GetClosestElement<T>(this IEnumerable<T> list, Vector3 point) where T : ITransform
    {
        var minDistance = Mathf.Infinity;
        T minPoint = default(T);
        foreach (var element in list)
        {
            var distance = Vector3.Distance(element.Transform.position, point);
            if (distance > minDistance) continue;
            minDistance = distance;
            minPoint = element;
        }

        return minPoint;
    }


    public static Vector3 ProjectOnPlane(this Vector3 vector, Vector3 normal) =>
        Vector3.ProjectOnPlane(vector, normal);

    public static float GetLength(this Vector3[] path)
    {
        float length = 0f;
        for (int i = 1; i < path.Length; i++)
            length += Vector3.Distance(path[i - 1], path[i]);
        return length;
    }

    public static Vector3 RotateAroundAxis(this Vector3 vector, float angle, Vector3 axis)
    {
        var quaternion = Quaternion.AngleAxis(angle, axis);
        return quaternion * vector;
    }

    public static IEnumerator LerpValue(this Vector3 startValue, Vector3 endValue, float startDelay, float duration,
        UnityAction<Vector3, float> onValueUpdated, UnityAction onUpdated, UnityAction onEnd = null)
    {
        float elapsed = 0;
        yield return new WaitForSeconds(startDelay);
        while (elapsed <= duration)
        {
            onValueUpdated?.Invoke(Vector3.Lerp(startValue, endValue, elapsed / duration), elapsed / duration);
            onUpdated?.Invoke();
            elapsed += Time.deltaTime;
            yield return null;
        }

        onValueUpdated?.Invoke(Vector3.Lerp(startValue, endValue, 1), 1);
        onEnd?.Invoke();
    }

    public static Vector3 ConvertToXZVector(this Vector3 vector) => new Vector3(vector.x, 0, vector.y);
    public static Vector3 ConvertToXYVector(this Vector3 vector) => new Vector3(vector.x, vector.z, 0);
}