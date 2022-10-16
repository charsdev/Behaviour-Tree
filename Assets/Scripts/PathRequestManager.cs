using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathRequestManager : MonoBehaviour
{
	private Queue<PathRequest> _pathRequestQueue = new Queue<PathRequest>();
	[SerializeField] private PathRequest _currentPathRequest;
	private AStar _pathfinding;
	[SerializeField] private bool _isProcessingPath;

	public static PathRequestManager Instance;

	private void Awake()
	{
		Instance = this;
		_pathfinding = GetComponent<AStar>();
	}

	public static void RequestPath(GameObject root, GameObject goal, Action<List<GameObject>, bool> callback)
	{
		Node pathStart = RaycastAboveNode(root);
		Node pathEnd = RaycastAboveNode(goal);
		PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
		Instance._pathRequestQueue.Enqueue(newRequest);
		Instance.TryProcessNext();
	}

	private void TryProcessNext()
    {
        if (_isProcessingPath || _pathRequestQueue.Count == 0)
        {
            return;
        }

        _currentPathRequest = _pathRequestQueue.Dequeue();
        _isProcessingPath = true;
        _pathfinding.GetPath(_currentPathRequest.pathStart, _currentPathRequest.pathEnd);
    }

    private static Node RaycastAboveNode(GameObject GO)
	{
		foreach (Collider2D c in Physics2D.OverlapPointAll(GO.transform.position))
		{
            if (c.tag != "Node")
            {
                continue;
            }

            return c.GetComponent<Node>();
        }

		return null;
	}

	public void FinishedProcessingPath(List<GameObject> path, bool success)
	{
		_currentPathRequest.callback(path, success);
		_isProcessingPath = false;
		TryProcessNext();
	}

	public struct PathRequest
	{
		public Node pathStart;
		public Node pathEnd;
		public Action<List<GameObject>, bool> callback;

		public PathRequest(Node _start, Node _end, Action<List<GameObject>, bool> _callback) 
		{
			pathStart = _start;
			pathEnd = _end;
			callback = _callback;
		}
	}
}
