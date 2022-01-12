using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHead : MonoBehaviour
{
    public static GameObject moveObject;

    public GameObject ariciObject;
    public GameObject ariciGoldObject;

    public ParticleSystem deathParticles;
    public ParticleSystem deathParticlesEnemy;
    public ParticleSystem deathParticlesArici;
    public ParticleSystem deathParticlesGold;

    private ParticleSystem DefParticles1;
    private ParticleSystem DefParticles2;
    private ParticleSystem DefParticles3;

    public static bool ate;
    public static bool superPower;

    public float powerTime;
    public static float timer;

    Material GoldMaterial;

    MeshRenderer goldArici;
    MeshRenderer meshRenderer;
    Material oldMaterial;

    private PathFinding gridMap;

    private int widthGrid;
    private int heightGrid;

    public AudioSource EatSound;
    public AudioSource Enemy;
    public AudioSource EnemyExp;
    public AudioSource Gold;

    private bool lose;
    private float timeToLoseScene;
    private float timeLose;

    void Start()
    {
        powerTime = 3.0f;
        timer = powerTime;

        moveObject = GameObject.Find("Cap");

        GoldMaterial = Resources.Load("Material/Color_yellow", typeof(Material)) as Material;

        goldArici = ariciGoldObject.GetComponent<MeshRenderer>();
        meshRenderer = ariciObject.GetComponent<MeshRenderer>();
        
        oldMaterial = meshRenderer.material;

        widthGrid = 42;
        heightGrid = 25;
        gridMap = new PathFinding(widthGrid, heightGrid);

        ate = false;
        superPower = false;

        lose = false;
        timeLose = 2f;
        timeToLoseScene = timeLose;
    }

    void Update()
    {
        if (timer < 0)
        {
            superPower = false;
            timer = powerTime;
            //meshRenderer.material = oldMaterial;

            ariciObject.SetActive(true);
            ariciGoldObject.SetActive(false);

            GameConfig.player_speed -= 5f;
            WallUpStatusPower.animation_wall_turn_off = true;
        }
        if (superPower)
        {
            timer -= Time.deltaTime;
        }

        if (timeToLoseScene < 0)
        {
            GameConfig.enemy.Clear();
            GameConfig.enemy_target.Clear();

            GameConfig.player_speed = GameConfig.initial_player_speed;
            Application.LoadLevel("lose_scene");
            
            timeToLoseScene = timeLose;
            //lose = false;
        }

        if (lose == true)
        {
            timeToLoseScene -= Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food")
        {

            DefParticles1 = Instantiate(deathParticles, collision.gameObject.transform.position, Quaternion.identity);

            Destroy(DefParticles1.gameObject, 2f);

            collision.gameObject.transform.position = new_position_with_range();

            GameConfig.player_speed += 0.05f;
            GameConfig.score++;
            ate = true;

            EatSound.Play();
        }
        if(collision.gameObject.tag == "Enemy")
        {
                if (MoveHead.superPower == true)
            {
                //GameConfig.score++;

                Destroy(collision.gameObject);

                DefParticles2 = Instantiate(deathParticlesEnemy, collision.gameObject.transform.position + new Vector3(0,1.3f,0), Quaternion.identity);

                Destroy(DefParticles2.gameObject, 3f);

                int index = GameConfig.enemy.IndexOf(collision.gameObject);

                Destroy(GameObject.Find("Empty_" + index));

                GameConfig.enemy.Remove(collision.gameObject);
                GameConfig.enemy_target.Remove(GameConfig.enemy_target[index]);

                for (int i = index; i < GameConfig.enemy.Count; i++)
                {
                    GameConfig.enemy[i].name = ("Enemy_" + i.ToString());
                    GameConfig.enemy_target[i].name = ("Empty_" + i.ToString());
                }

                ate = true;
                EnemyExp.Play();
            }
            else
            {
                Enemy.Play();
                lose = true;

                ariciObject.SetActive(false);

                //Destroy(collision.gameObject);
                Instantiate(deathParticlesArici, collision.gameObject.transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
                
                Debug.Log("GAME OVER");
                GameConfig.final_score = GameConfig.score;
                //GameConfig.score = 0;

            }
        }
        if (collision.gameObject.tag == "GoldApple")
        {
            
            Destroy(GameConfig.goldApple_prefab);
            DefParticles3 = Instantiate(deathParticlesGold, collision.gameObject.transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
            Destroy(DefParticles3.gameObject, 2f);

            superPower = true;
            //meshRenderer.sharedMaterial = goldArici.material; //culoarea capului se modifica
            ariciObject.SetActive(false);
            ariciGoldObject.SetActive(true);

            GameConfig.player_speed += 5f;

            GameConfig.score++;
            Gold.Play();
        }
    }


    private Vector3 RandomPosition()
    {
        int x = Random.Range(0, widthGrid);
        int z = Random.Range(0, heightGrid);


        Vector3 pos = new Vector3(gridMap.GetGrid().GetWorldPosition(x, z).x, 0, gridMap.GetGrid().GetWorldPosition(x, z).z);
        return pos;
    }

    private Vector3 new_position_with_range()
    {
        Vector3 pos = RandomPosition();

        bool finded = false;

        while(finded == false)
        {
            pos = RandomPosition();
            if(GameConfig.enemy.Count > 0)
                for (int i = 0; i < GameConfig.enemy.Count; i++)
                {
                    if (Vector3.Distance(pos, GameConfig.enemy[i].transform.position) > 5 && Vector3.Distance(pos, transform.position) > 8)
                    {
                        finded = true;
                        continue;
                    }
                    else
                    {
                        finded = false;
                        break;
                    }
                }
            else finded = true;
        }
        
        return pos;
    }

}
