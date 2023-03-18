using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabelManager : MonoBehaviour
{
    [SerializeField] Color defaultColor  = Color.white;
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = Color.blue;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    GridManager gridManager;

    void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();
        DisplayCoordinates();   
    }

    void Start()
    {
        label.enabled = false;
    }

    void Update() 
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            ChangeName();
        }

        SetLabelColor();
        ToogleLabels();
    }

    void ToogleLabels()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            label.enabled= !label.IsActive();
        }
    }

    void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) { return; }
        
        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }


    }

    void DisplayCoordinates()
    {
        if (gridManager == null) { return; }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void ChangeName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
