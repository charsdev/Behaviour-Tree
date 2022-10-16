using UnityEngine;

public static class Utilities
{
    public static void ClearArray<T>(T[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = default;
        }
    }

    public static GameObject GetClosestGameObjectByTag(string tag, Transform point)
    {
        GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestMine = null;
        float minDist = Mathf.Infinity;

        foreach (var go in gameObjectsWithTag)
        {
            float dist = Vector3.Distance(point.position, go.transform.position);

            if (dist < minDist)
            {
                closestMine = go;
                minDist = dist;
            }
        }

        return closestMine;
    }
}