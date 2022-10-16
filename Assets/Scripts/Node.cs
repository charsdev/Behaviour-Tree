using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	public Node Father;
	public List<Node> Adjacents;
	public int Col;
	public int Row;
	public Vector3 Position;
	public bool Walkable = true;
	public Renderer Renderer;
	public int Weight;
	public int Heuristic;

	public void Start()
	{
		Renderer = GetComponent<Renderer>();
		Weight = Random.Range(1, 10);
	}
}
