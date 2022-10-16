using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Grid : MonoBehaviour
{
    public GameObject Cube;
    public int HorizontalCells = 6;
    public int VerticalsCells = 4;
    private Vector3 _startPos;
    private readonly float _spacingX = 0.5f;
    private readonly float _spacingY = 0.5f;
    public Node[,] CellsArray;
    public List<List<Node>> nodes = new List<List<Node>>();
    public List<Node> Fields = new List<Node>();
    public List<Node> Cells = new List<Node>();
    public static Grid Instance;
    public GameObject GridGameObject;
    public Vector2 GridWorldSize;

    private void Awake()
    {
        Instance = this;
        CellsArray = new Node[HorizontalCells, VerticalsCells];
    }

    public void MakeGrid(int horizontal, int vertical)
    {
        _startPos = new Vector2(transform.position.x - GridWorldSize.x/2, transform.position.y + GridWorldSize.y/2);
        CellsArray = new Node[HorizontalCells, VerticalsCells];
        GameObject clone;
        Vector3 clonePos;
        Sprite sprite = Cube.GetComponent<SpriteRenderer>().sprite;

        var offset = new Vector2(
            sprite.bounds.center.x - sprite.bounds.max.x,
            sprite.bounds.center.y + sprite.bounds.max.y
        );

        GridGameObject = new GameObject("Grid");

        for (int x = 0; x < horizontal; x++)
        {
            for (int y = 0; y < vertical; y++)
            {
                clonePos = new Vector3(
                   _startPos.x + (x * _spacingX) - offset.x,
                   _startPos.y + (y * -_spacingY) - offset.y,
                   _startPos.z
                );
                clone = Instantiate(Cube, clonePos, Quaternion.identity);
                clone.name = $"{y}x{x}";
                clone.tag = "Node";
                clone.AddComponent<BoxCollider2D>();
                var node = clone.AddComponent<Node>();
                node.Col = y;
                node.Row = x;
                node.Position = clone.transform.position;
                clone.transform.SetParent(GridGameObject.transform);
                CellsArray[x, y] = node;
                Cells.Add(node);
            }
        }

        for (int x = 0; x < horizontal; x++)
        {
            for (int y = 0; y < vertical; y++)
            {
                GetAdjacents(ref CellsArray[x, y]);
            }
        }

        Fields = Cells;

        for (int x = 0; x < HorizontalCells; x++)
        {
            for (int y = 0; y < VerticalsCells; y++)
            {
                foreach (var n in Fields)
                {
                    CellsArray[x, y] = n;
                }
            }
        }
    }

    public void GetAdjacents(ref Node current)
    {
        List<Node> neighbours = new List<Node>();

        for (int ix = current.Row - 1; ix <= current.Row + 1; ix++)
        {
            for (int iy = current.Col - 1; iy <= current.Col + 1; iy++)
            {
                if (ix >= 0 && ix < CellsArray.GetLength(0) && iy >= 0 && iy < CellsArray.GetLength(1))
                {
                    neighbours.Add(CellsArray[ix, iy]);
                }
            }
        }

        for (int ix = 0; ix < neighbours.Count; ix++)
        {
            if (neighbours[ix] == current)
            {
                neighbours.Remove(current);
                break;
            }
        }

        current.Adjacents = neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + (GridWorldSize.x / 2)) / GridWorldSize.x;
        float percentY = (worldPosition.y + (GridWorldSize.y / 2)) / GridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((GridWorldSize.x) * percentX);
        int y = Mathf.RoundToInt((GridWorldSize.y) * percentY);

        return CellsArray[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, GridWorldSize.y, 0));
    }
}