using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing_pathFinding : MonoBehaviour
{
    PathFinding pathFinding;

    private void Start()
    {
        pathFinding = new PathFinding(21, 12);
    }

    private void Update()
    {
        pathFinding.GetGrid().GetXY(MoveHead.moveObject.transform.position, out int x, out int y);
        List<PathNode> path = pathFinding.FindPath(0, 0, x, y);

        //Debug.Log(x + " " + y);
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                //Debug.DrawLine(new Vector3(path[i].x, 1f, path[i].y) , new Vector3(path[i + 1].x, 1f, path[i + 1].y) , Color.green);

                Debug.DrawLine(pathFinding.GetGrid().GetWorldPosition(path[i].x, path[i].y), pathFinding.GetGrid().GetWorldPosition(path[i + 1].x, path[i + 1].y), Color.green);

                //Debug.Log("[" + path[i].x + ", " + path[i].y + "] [" + path[i + 1].x + ", " + path[i + 1].y + "]");
            }
        }


        
    }
}
