using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using System.Linq;

public class GameManagement : MonoBehaviour
{

    public static float totalScore;
    public static int level;
    public static int lives;
    public static int totalLevels;
    public static int toOneUp;
    static float minX = -2.71f;
    static float maxX = 2.66f;
    static float minY = -1.23f;
    static float maxY = 4.74f;
    public static int gamesPlayed;
    float randomX;
    float randomY;
    GameObject planetOriginal;
    public static GameManagement thisObject;
    public GameObject playerPlanet;
    public GameObject planet_01;
    public GameObject planet_02;
    public GameObject planet_03;
    public GameObject planet_04;
    public GameObject planet_05;
    public GameObject planet_06;
    public GameObject planet_07;
    public GameObject planet_08;
    List<GameObject> planetList;
    static AudioSource sound;

    // Use this for initialization
    void Start()
    {
        planetList = new List<GameObject>() { planet_01, planet_02, planet_03, planet_04, planet_05, planet_06, planet_07, planet_08 };
        sound = GetComponent<AudioSource>();
        DontDestroyOnLoad(GameObject.Find("GameManagement"));
        Advertisement.Initialize("3535676", true);
    }

    void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "WorldCollider")
        {
            if(GameObject.FindGameObjectsWithTag("RoamingPlanet").Length <= 0)
            {
                LevelComplete();
            }
            if(GameObject.FindGameObjectsWithTag("PlayerPlanet").Length <= 0)
            {
                CreateNewPlayerPlanet();
            }
        }
    }

    public static void Score(float score)
    {
        totalScore += score;
        sound.Play();
        if (totalScore > toOneUp)
        {
            lives++;
            toOneUp *= (int)totalScore * 2;
        }
    }

    public void LevelComplete()
    {
        if (SceneManager.GetActiveScene().name == "WorldCollider")
        {
            //Checks to make sure that new planets don't overlap existing ones
            for (int i = 0; i <= level; i++)
            {
                planetOriginal = planetList[Random.Range(0, 8)];
                //Runs until enough planets are spawned for the current level.
                do
                {
                    randomX = Random.Range(minX, maxX);
                    randomY = Random.Range(minY, maxY);
                    Instantiate(planetOriginal, new Vector3(randomX, randomY, 0), Quaternion.identity);
                } while (Physics.CheckSphere(new Vector3(randomX, randomY, 0), 0.495f));
            }
            level++;
            CreateNewPlayerPlanet();
        }
    }

    public void CreateNewPlayerPlanet()
    {
        
        if(lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        playerPlanet.GetComponent<SpriteRenderer>().sprite = GameObject.FindGameObjectWithTag("RoamingPlanet").GetComponent<SpriteRenderer>().sprite;
        Instantiate(playerPlanet, new Vector3(0, -3, 0), Quaternion.identity);
    }
}