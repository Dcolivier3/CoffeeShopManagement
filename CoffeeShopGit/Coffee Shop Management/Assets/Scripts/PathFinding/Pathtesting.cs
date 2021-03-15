using System.Collections;
using System.Collections.Generic;
using Jessy.CodeMonkey.Utils;
using UnityEngine;

public class Pathtesting : MonoBehaviour {
    private PathFinding pathFinding;
    //[SerializeField] private PathfindingVisual pathfindingVisual;
    
    void Start () {
        pathFinding = new PathFinding (10, 10);
        //pathfindingVisual.SetGrid (pathFinding.GetGrid ());
    }

    private void Update () {
        if (Input.GetMouseButtonDown (0)) {
            Debug.Log ("yeet");
            Vector3 mouseWorldPos = GridUtils.GetMouseWorldPosition ();
            pathFinding.GetGrid ().GetXY (mouseWorldPos, out int x, out int y);
            List<PathNode> path = pathFinding.FindPath (0, 0, x, y); // start node == 0:0 pathfrom mouse pos x:y to 0:0 then reverse it to get correct path
            if (path != null) {
                Debug.Log ("not null");
                for (int i = 0; i < path.Count - 1; i++) {
                    Debug.DrawLine (new Vector3 (path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3 (path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 2f);

                }
            }

        }
         if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = GridUtils.GetMouseWorldPosition();
            pathFinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathFinding.GetNode(x, y).SetIsWalkable(!pathFinding.GetNode(x, y).isWalkable);
        }
    }

}