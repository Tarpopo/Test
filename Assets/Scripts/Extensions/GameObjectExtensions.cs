using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    public static int GetActiveCount(this IEnumerable<GameObject> gameObjects) =>
        gameObjects.Count(gameObject => gameObject.activeSelf);

    public static void Enable(this GameObject gameObject) => gameObject.SetActive(true);

    public static void Disable(this GameObject gameObject) => gameObject.SetActive(false);
}