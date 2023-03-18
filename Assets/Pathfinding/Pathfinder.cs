using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int endCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }
    public Vector2Int EndCoordinates { get { return endCoordinates; } }

    Node currentNode;
    Node startNode;
    Node endNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions  = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    GridManager gridManager;
  
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            endNode = grid[endCoordinates];

        }

    }

    void Start()
    {
        BuildNewPath();
    }

    void ExploreNeighbors()
    {
        List <Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = currentNode.coordinates + direction;

            if(grid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(grid[neighborCoordinates]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo= currentNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        endNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while (frontier.Count > 0 && isRunning)
        {
            currentNode = frontier.Dequeue();
            currentNode.isExplored = true;
            ExploreNeighbors();

            if (currentNode.coordinates == endCoordinates)
            {
                isRunning= false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List <Node> path = new List<Node>();
        Node currentNode = endNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo!= null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public List<Node> BuildNewPath()
    {
        return BuildNewPath(startCoordinates);
    }

    public List<Node> BuildNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool prevState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = BuildNewPath();
            grid[coordinates].isWalkable = prevState;

            if(newPath.Count <= 1) 
            {
                BuildNewPath();
                return true;
            }

        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath",false , SendMessageOptions.DontRequireReceiver);
    }
}
