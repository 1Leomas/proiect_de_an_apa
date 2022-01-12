using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public GameObject apple;
    public GameObject enemy_prefab;
    public static GameObject goldApple_prefab;

    public static List<GameObject> enemy;
    public static List<GameObject> enemy_target;

    public static int score;
    public static int final_score;
    public static int record_score;

    public static float initial_player_speed;
    public static float player_speed = 34;

    public static float enemySpeed;

    public static Material RedMaterial;
    public static Material BlackMaterial;
    public static Material GoldMaterial;

    public static float gold_apple_timer;
    private float gold_apple_time_destroy;
    private bool gold_apple_on_map;

    /*public GameObject wallUp;
    public GameObject wallDown;
    public GameObject wallLeft;
    public GameObject wallRight;*/

    public static float[] map_limits_x;
    public static float map_limits_y;
    public static float[] map_limits_z;

    private PathFinding gridMap;

    private int widthGrid;
    private int heightGrid;
    

    void Start()
    {
        /*map_limits_x[0] = wallLeft.transform.position.x;
        map_limits_x[1] = wallRight.transform.position.x;
        map_limits_z[0] = wallDown.transform.position.z;
        map_limits_z[1] = wallUp.transform.position.z;*/
        //map_limits_y = 1.0f;

        /*Debug.Log("x: " + map_limits_x[0] + " - " + map_limits_x[1]);
        Debug.Log("z: " + map_limits_z[0] + " - " + map_limits_z[1]);*/

        map_limits_x = new float[2];
        map_limits_z = new float[2];

        map_limits_x[0] = 3.0f;
        map_limits_x[1] = 79.40491f;

        map_limits_y = 0f;

        map_limits_z[0] = 1.319799f;
        map_limits_z[1] = 48.14886f;

        RedMaterial = Resources.Load("Material/red_metalic", typeof(Material)) as Material;
        BlackMaterial = Resources.Load("Material/Black", typeof(Material)) as Material;
        GoldMaterial = Resources.Load("Material/Color_yellow", typeof(Material)) as Material;

        initial_player_speed = 24;
        player_speed = initial_player_speed;

        enemySpeed = 5;

        enemy = new List<GameObject>(20);
        enemy_target = new List<GameObject>(20);

        score = 0;
        final_score = 0;

        widthGrid = 42;
        heightGrid = 25;
        gridMap = new PathFinding(widthGrid, heightGrid);

        apple.transform.position = RandomPosition();
        apple.SetActive(true);

        gold_apple_time_destroy = 5f;
        gold_apple_timer = gold_apple_time_destroy;
        gold_apple_on_map = false;

        record_score = PlayerPrefs.GetInt("Record_score", 0);

        //Debug.Log("Record " + record_score);
    }

    void Update()
    {

        if (MoveHead.ate == true)
        {
            //enemy speed
            if (score % 5 == 0)
            {
                //enemySpeed += 0.05f;
            }

            //create gold ball
            if (score % 10 == 0)
            {
                goldApple_prefab = CreateBall(Color.yellow);
                gold_apple_on_map = true;
            }

            //create black ball
            if (score % 3 == 0)
            {
                enemy_target.Add(Create_enemy_target());
                enemy.Add(CreateBall(Color.black));
            }

            MoveHead.ate = false;
        }

        if (gold_apple_on_map)
        {
            gold_apple_timer-=Time.deltaTime;
        }
        if(gold_apple_timer < 0)
        {
            Destroy(goldApple_prefab);
            gold_apple_on_map = false;
            gold_apple_timer = gold_apple_time_destroy;
        }


        //move black balls 
        for (int i = 0; i < enemy.Count; i++)
        {
            //enemy follow targhet
            enemy[i].transform.position = Vector3.MoveTowards(enemy[i].transform.position, enemy_target[i].transform.position, Time.deltaTime * enemySpeed);
            enemy[i].transform.LookAt(enemy_target[i].transform);

            //cand enemy ajunge la targhet ii dam alta pozitie lu targhet
            if (Vector3.Distance(enemy[i].transform.position, enemy_target[i].transform.position) < 1)
            {
                enemy_target[i].transform.position = RandomPosition();
                
            }
        }





    }
    
    //pozitia inamicului nou
    private Vector3 NewRandomPosition()
    {
        Vector3 pos = RandomPosition();
        
        for (int i = 0; i < enemy.Count; i++)
        {
            //creem doar daca random pozition este mai departe de  altii enemy cu 5
            if (enemy[i] != null && Vector3.Distance(pos, enemy[i].transform.position) < 5)
            {
                return NewRandomPosition();
            }
        }
        
        return pos;
    }
    
   private Vector3 RandomPosition()
   {
        int x = Random.Range(0, widthGrid);
        int z = Random.Range(0, heightGrid);


        Vector3 pos = new Vector3(gridMap.GetGrid().GetWorldPosition(x, z).x, map_limits_y, gridMap.GetGrid().GetWorldPosition(x, z).z);
        return pos;
    }

    private Vector3 create_new_enemy_position()
    {
        Vector3 pos = RandomPosition();
        if (Vector3.Distance(pos,  MoveHead.moveObject.transform.position) < 5)
        {
            return create_new_enemy_position();
        }
        return pos;
    }

    private GameObject CreateBall(Color color)
    {
        GameObject sphere;

        if(color.Equals(Color.black))
        {
            sphere = GameObject.Instantiate(enemy_prefab);
            sphere.transform.position = create_new_enemy_position();
            sphere.gameObject.SetActive(true);
            sphere.name = "Enemy_" + enemy.Count;
            sphere.tag = "Enemy";
        }
        else if (color.Equals(Color.red))
        {
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(2.12219f, 2.12219f, 2.12219f);
            //var cubeRenderer = sphere.GetComponent<Renderer>();
            //var meshRender = sphere.GetComponent<MeshRenderer>();
            sphere.transform.position = RandomPosition();
            //meshRender.material = RedMaterial;
            sphere.name = "Apple";
        }
        else if (color.Equals(Color.yellow))
        {
            /*sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(2.12219f, 2.12219f, 2.12219f);
            var cubeRenderer = sphere.GetComponent<Renderer>();
            var meshRender = sphere.GetComponent<MeshRenderer>();
            meshRender.material = GoldMaterial;*/

            sphere = Instantiate(GameObject.Find("Gold_apple"));

            sphere.transform.position = RandomPosition();
            sphere.name = "Apple_gold";
            sphere.tag = "GoldApple";
        }
        else
        {
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(2.12219f, 2.12219f, 2.12219f);
            var cubeRenderer = sphere.GetComponent<Renderer>();
            var meshRender = sphere.GetComponent<MeshRenderer>();
            cubeRenderer.material.SetColor("_Color", color);
            sphere.transform.position = RandomPosition();
            sphere.name = "sphere_idk";
        }
            
        return sphere;
    }

    private GameObject Create_enemy_target()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        sphere.transform.position = RandomPosition();
        sphere.name = "Empty_" + enemy_target.Count;
        return sphere;
    }
}
