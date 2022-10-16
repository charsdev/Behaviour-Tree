using System.Collections.Generic;
using UnityEngine;
using System;

public class AStar : MonoBehaviour
{
    private PathRequestManager requestManager;
    public void Awake() => requestManager = GetComponent<PathRequestManager>();
    public void OpenNode(List<Node> open, Node n) => open.Add(n);
    public void CloseNode(List<Node> closed, Node n) => closed.Add(n);
    public Node GetNode(List<Node> open) => open[0];

    public int GetDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.Row - b.Row);
        int distY = Mathf.Abs(a.Col - b.Col);

        return distX > distY
            ? (Grid.Instance.HorizontalCells * distY) + (Grid.Instance.VerticalsCells * (distX - distY))
            : (Grid.Instance.HorizontalCells * distX) + (Grid.Instance.VerticalsCells * (distY - distX));
    }

    private List<GameObject> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Father;
        }

        var gameObjects = ObtainGameObject(path);
        gameObjects.Reverse();

        return gameObjects;
    }

    public List<GameObject> ObtainGameObject(List<Node> path)
    {
        List<GameObject> newPath = new List<GameObject>();

        for (int i = 0; i < path.Count; i++)
        {
            newPath.Add(path[i].gameObject);
        }

        return newPath;
    }

    public void GetPath(Node root, Node goal)
    {
        List<GameObject> waypoints = new List<GameObject>();
        List<Node> closed = new List<Node>();
        List<Node> open = new List<Node>();
        bool pathSuccess = false;

        OpenNode(open, root);
        while (open.Count > 0)
        {
            Node currentNode = GetNode(open);
            for (int i = 1; i < open.Count; i++)
            {
                if ((open[i].Weight < currentNode.Weight
                    || open[i].Weight == currentNode.Weight)
                    && open[i].Heuristic < currentNode.Heuristic)
                {
                    currentNode = open[i];
                }
            }

            open.Remove(currentNode);
            CloseNode(closed, currentNode);

            if (currentNode == goal)
            {
                pathSuccess = true;
                break;
            }

            for (int i = 0; i < currentNode.Adjacents.Count; i++)
            {
                Node n = currentNode.Adjacents[i];

                if (!n.Walkable || closed.Contains(n))
                {
                    continue;
                }

                int newCostToNeighbour = n.Weight + GetDistance(currentNode, n);

                if (newCostToNeighbour < n.Weight || !open.Contains(n))
                {
                    n.Father = currentNode;
                    n.Weight = newCostToNeighbour;
                    n.Heuristic = GetDistance(n, goal);

                    if (!open.Contains(n))
                    {
                        OpenNode(open, n);
                    }
                }
            }
        }
        if (pathSuccess)
        {
            waypoints = RetracePath(root, goal);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }
}


