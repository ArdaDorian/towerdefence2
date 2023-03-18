using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower tower;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    Vector2Int coordinates = new Vector2Int();  

    GridManager gridManager;
    Pathfinder pathFinder;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder= FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        if(gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates) && isPlaceable) 
        {
            bool isPlaced = tower.CreateTower(tower,transform.position);
            isPlaceable = !isPlaced;
            if (isPlaced)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
        }
    }
}
