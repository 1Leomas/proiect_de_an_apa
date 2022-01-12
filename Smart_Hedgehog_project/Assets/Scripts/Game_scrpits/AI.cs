using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Gizmos = Popcron.Gizmos;

public class AI : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject AIObject;
    
    public PathFinding gridMap;
    List<PathNode> path;
    private bool isMoving;

    private GameObject tmp;

    private int widthGrid;
    private int heightGrid;

    private float timer;
    private float timer_default;

    float min_distance;
    int enemy_min_index;

    private void Start()
    {

        tmp = new GameObject("AI_target");
        tmp.transform.localScale = Vector3.one;
        if (targetObject != null)
        {
            widthGrid = 42; //37 22 2.3
            heightGrid = 25;
            gridMap = new PathFinding(widthGrid, heightGrid);

            if (AI_controller.AI_mode == true)
            {
                gridMap.GetGrid().GetXY(AIObject.transform.position, out int xObject, out int yObject);
                gridMap.GetGrid().GetXY(targetObject.transform.position, out int xTraget, out int yTraget);

                path = gridMap.FindPath(xObject, yObject, xTraget, yTraget);

                isMoving = true;
            }  
        }
        timer_default = 0.5f;
        timer = timer_default;

        //Gizmos.Material = tmp.GetComponent<Material>();
        

    }

    private void Update()
    {
        if (AI_controller.AI_mode == true)
        {
            //Gizmos.Enabled = true;

            timer -= Time.deltaTime;

            if (path != null)
            {
                if (isMoving && path.Count != 0)
                {
                    tmp.transform.position = gridMap.GetGrid().GetWorldPosition(path[1].x, path[1].y);
                    //Debug.Log("\tCautam pe 222: [" + tmp.transform.position.x + " - " + tmp.transform.position.z + "]");
                    //Debug.Log("DISTANCE: " + Vector3.Distance(AIObject.transform.position, tmp.transform.position));

                    AIObject.transform.position = Vector3.MoveTowards(AIObject.transform.position, tmp.transform.position, Time.deltaTime * GameConfig.player_speed * 0.9f);
                    AIObject.transform.LookAt(tmp.transform.position);

                    if (Vector3.Distance(AIObject.transform.position, tmp.transform.position) <= 0.1f)
                    {
                        isMoving = false;
                    }

                }
                else if (isMoving == false && path.Count != 0)
                {
                    if (MoveHead.superPower == false && GameConfig.enemy.Count > 0)
                    {
                        for (int i = 0; i < GameConfig.enemy.Count; i++)
                        {

                            gridMap.GetGrid().GetXY(GameConfig.enemy[i].transform.position, out int xEnemy, out int yEnemy);

                            gridMap.GetNode(xEnemy, yEnemy).SetIsWalkable(false);

                            for (int j = 1; j <= 2; j++)
                            {
                                //stanga
                                if (xEnemy - j >= 0)
                                    gridMap.GetNode(xEnemy - j, yEnemy).SetIsWalkable(false);
                                //stanga-jos
                                if (xEnemy - j >= 0 && yEnemy - j >= 0)
                                    gridMap.GetNode(xEnemy - j, yEnemy - j).SetIsWalkable(false);
                                //stanga-sus
                                if (xEnemy - j >= 0 && yEnemy + j < heightGrid)
                                    gridMap.GetNode(xEnemy - j, yEnemy + j).SetIsWalkable(false);

                                //dreapta
                                if (xEnemy + j < widthGrid)
                                    gridMap.GetNode(xEnemy + j, yEnemy).SetIsWalkable(false);
                                //dreapta-jos
                                if (xEnemy + j < widthGrid && yEnemy - j > 0)
                                    gridMap.GetNode(xEnemy + j, yEnemy - j).SetIsWalkable(false);
                                //dreapta-sus
                                if (xEnemy + j < widthGrid && yEnemy + j < heightGrid)
                                    gridMap.GetNode(xEnemy + j, yEnemy + j).SetIsWalkable(false);

                                //jos
                                if (yEnemy - j >= 0)
                                    gridMap.GetNode(xEnemy, yEnemy - j).SetIsWalkable(false);
                                //sus
                                if (yEnemy + j < heightGrid)
                                    gridMap.GetNode(xEnemy, yEnemy + j).SetIsWalkable(false);
                            }

                            if (xEnemy - 1 >= 0 && yEnemy + 2 < heightGrid)
                                gridMap.GetNode(xEnemy - 1, yEnemy + 2).SetIsWalkable(false);
                            if (xEnemy + 1 < widthGrid && yEnemy + 2 < heightGrid)
                                gridMap.GetNode(xEnemy + 1, yEnemy + 2).SetIsWalkable(false);

                            if (xEnemy + 2 < widthGrid && yEnemy + 1 < heightGrid)
                                gridMap.GetNode(xEnemy + 2, yEnemy + 1).SetIsWalkable(false);
                            if (xEnemy + 2 < widthGrid && yEnemy - 1 >= 0)
                                gridMap.GetNode(xEnemy + 2, yEnemy - 1).SetIsWalkable(false);

                            if (xEnemy - 1 >= 0 && yEnemy - 2 >= 0)
                                gridMap.GetNode(xEnemy - 1, yEnemy - 2).SetIsWalkable(false);
                            if (xEnemy + 1 < widthGrid && yEnemy - 2 >= 0)
                                gridMap.GetNode(xEnemy + 1, yEnemy - 2).SetIsWalkable(false);

                            if (xEnemy - 2 >= 0 && yEnemy + 1 < heightGrid)
                                gridMap.GetNode(xEnemy - 2, yEnemy + 1).SetIsWalkable(false);
                            if (xEnemy - 2 >= 0 && yEnemy - 1 >= 0)
                                gridMap.GetNode(xEnemy - 2, yEnemy - 1).SetIsWalkable(false);

                            if (xEnemy - 2 >= 0 && yEnemy - 1 >= 0)
                                gridMap.GetNode(xEnemy - 2, yEnemy - 1).SetIsWalkable(false);


                            //if (xEnemy - 3 >= 0) gridMap.GetNode(xEnemy - 3, yEnemy).SetIsWalkable(false);

                            Vector3 directia = GameConfig.enemy[i].transform.position + GameConfig.enemy[i].transform.forward * 8;
                            
                            gridMap.GetGrid().GetXY(directia, out int xForward, out int yForward);

                            if ((xForward >= 0 && yForward >= 0) && (xForward < widthGrid && yForward < heightGrid))
                            {
                                gridMap.GetNode(xForward, yForward).SetIsWalkable(false);

                                if(xForward - 1  >= 0 && yForward + 1 < heightGrid )
                                    gridMap.GetNode(xForward - 1, yForward + 1).SetIsWalkable(false);
                                if (yForward + 1 < heightGrid)
                                    gridMap.GetNode(xForward, yForward + 1).SetIsWalkable(false);
                                if (xForward + 1 < widthGrid && yForward + 1 < heightGrid)
                                    gridMap.GetNode(xForward + 1, yForward + 1).SetIsWalkable(false);
                                if (xForward - 1 >= 0)
                                    gridMap.GetNode(xForward - 1, yForward).SetIsWalkable(false);
                                if (xForward + 1 < widthGrid)
                                    gridMap.GetNode(xForward + 1, yForward).SetIsWalkable(false);
                            }



                        }
                    }
                    else if (MoveHead.superPower == true && GameConfig.enemy.Count > 0)//daca superputerea e activa
                    {
                        //1. find lowest distance from AI object to enemy
                        min_distance = Vector3.Distance(AIObject.transform.position, GameConfig.enemy[0].transform.position);
                        enemy_min_index = 0;
                        for (int i = 1; i < GameConfig.enemy.Count; i++)
                        {
                            if (Vector3.Distance(AIObject.transform.position, GameConfig.enemy[i].transform.position) < min_distance)
                            {
                                min_distance = Vector3.Distance(AIObject.transform.position, GameConfig.enemy[i].transform.position);
                                enemy_min_index = i;
                            }
                        }
                        //trebuie de calculat daca el ajunge pana la bila cu super puterea activata
                        //Debug.Log("Distanta care reusim: " + MoveHead.timer*GameConfig.snake_speed);
                        //Debug.Log("Distanta necesara: " + Vector3.Distance(AIObject.transform.position, GameConfig.enemy[enemy_min_index].transform.position));

                        //2. targetObject = enemy with lowest distance
                        if (GameConfig.enemy.Count > 0 && MoveHead.timer * GameConfig.player_speed > Vector3.Distance(AIObject.transform.position, GameConfig.enemy[enemy_min_index].transform.position))
                            targetObject = GameObject.Find("Enemy_" + enemy_min_index);
                        else
                            targetObject = GameObject.Find("Food");
                    }


                    if (GameObject.Find("Apple_gold") != null && GameConfig.gold_apple_timer * GameConfig.player_speed > Vector3.Distance(AIObject.transform.position, GameObject.Find("Apple_gold").transform.position))
                    {
                        targetObject = GameObject.Find("Apple_gold");
                    }
                    else if (MoveHead.superPower == false || GameConfig.enemy.Count == 0)
                    {
                        targetObject = GameObject.Find("Food");
                    }
                    gridMap.GetGrid().GetXY(AIObject.transform.position, out int xObject, out int yObject);
                    gridMap.GetGrid().GetXY(targetObject.transform.position, out int xTraget, out int yTraget);

                    path = gridMap.FindPath(xObject, yObject, xTraget, yTraget);

                    isMoving = true;

                    if (timer < 0)
                    {
                        for (int i = 0; i < gridMap.GetGrid().GetWidth(); i++)
                        {
                            for (int j = 0; j < gridMap.GetGrid().GetHeight(); j++)
                            {
                                gridMap.GetNode(i, j).isWalkable = true;
                            }
                        }
                        timer = timer_default;
                    }

                }
            }
            else if (Vector3.Distance(AIObject.transform.position, targetObject.transform.position) > 0.01f)
            {
                targetObject = GameObject.Find("Food");
                gridMap.GetGrid().GetXY(AIObject.transform.position, out int xObject, out int yObject);
                gridMap.GetGrid().GetXY(targetObject.transform.position, out int xTraget, out int yTraget);

                path = gridMap.FindPath(xObject, yObject, xTraget, yTraget);

                //Debug.Log("path.Count: " + path.Count);
                //Debug.Log("Cautam pe : " + pathFinding.GetGrid().GetWorldPosition(path[0].x, path[0].y).x + " " + pathFinding.GetGrid().GetWorldPosition(path[0].x, path[0].y).z);

                if (timer < 0)
                {
                    for (int i = 0; i < gridMap.GetGrid().GetWidth(); i++)
                    {
                        for (int j = 0; j < gridMap.GetGrid().GetHeight(); j++)
                        {
                            gridMap.GetNode(i, j).isWalkable = true;
                        }
                    }
                    timer = timer_default;
                }
            }
        }
        //else Gizmos.Enabled = false;

    }


    //private void OnRenderObject()
    private void OnDrawGizmos()
    {
        if(path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(gridMap.GetGrid().GetWorldPosition(path[i].x, path[i].y), 0.5f);

                //Gizmos.Sphere(gridMap.GetGrid().GetWorldPosition(path[i].x, path[i].y), 0.5f, Color.green,false,50);
            }
        }
        if(gridMap != null)
        {
            for (int i = 0; i < gridMap.GetGrid().GetWidth(); i++)
            {
                for (int j = 0; j < gridMap.GetGrid().GetHeight(); j++)
                {
                    if (gridMap.GetNode(i, j).isWalkable == false)
                    {
                        //Gizmos.Cube(gridMap.GetGrid().GetWorldPosition(i, j), AIObject.transform.rotation, Vector3.one / 2, Color.black);

                        Gizmos.color = Color.black;
                        Gizmos.DrawCube(gridMap.GetGrid().GetWorldPosition(i, j), Vector3.one / 2);

                    }
                }
            }
        }
        
        
    }
}
