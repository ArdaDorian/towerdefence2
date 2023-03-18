using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    List<Node> path = new List<Node>();

    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemy;

    Pathfinder pathFinder;
    GridManager gridManager;

    void Awake()
    {
        enemy= GetComponent<Enemy>();
        pathFinder = FindObjectOfType<Pathfinder>();
        gridManager=FindObjectOfType<GridManager>();
    }


    void OnEnable()
    {
        ReturnPath();
        RecalculatePath(true);
    }

    void ReturnPath()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void RecalculatePath(bool restartPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (restartPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();   
        path = pathFinder.BuildNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);

            float travelPercent = 0;

            transform.LookAt(endPos);

            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
