using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static HelpStation;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor;
using static CursorManager;
using Random = UnityEngine.Random;
using static MovingObject;


public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterList;
    [SerializeField] private int countOfPerson;

    public static GameManager Instance { get; private set; }

    public static int coutnStations = Enum.GetNames(typeof(TypeStation.HelthType)).Length - 1;
    public static int max = (int)Math.Pow(2, coutnStations);

    private GameObject newCaracrer;
    private bool isNew = false;
    private bool isFirst = false;

    private bool nextTimer = true;

    private float frameTimer;
    private int currentFrame = 0;
    private const float animationSpeed = 0.05f;
    private float randomMove;
    private float randomStop;

    private int currentPersonCount = 0;
    private int spamPersonCount = 0;

    private int lifeStatus = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void Save—haracter(int life)
    {
        currentPersonCount++;
        
        lifeStatus += life;

        if (currentPersonCount == countOfPerson) // <=================================================
        {
            Debug.Log(lifeStatus / countOfPerson); // SCORE
            Debug.Log("EndGame"); // END MEME 
        }
    }
    private void Start()
    {
        frameTimer = animationSpeed;
        isNew = true;
        isFirst = true;
    }

    private void Update()
    {
        if(spamPersonCount == countOfPerson)
            return;

        if (isNew)
        {
            frameTimer -= Time.deltaTime;

            if (frameTimer < 0f)
            {
                if (isFirst)
                {
                    spamPersonCount++;

                    int randomSprite = Random.Range(0, characterList.Count);
                    newCaracrer = Instantiate(characterList[randomSprite]);

                    newCaracrer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    newCaracrer.transform.position = new Vector3(8f, -0.5f, 0);

                    newCaracrer.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    randomStop = Random.Range(6, 8);
                    randomMove = Random.Range(-10, 10);
                    randomMove /= 100;
                    isFirst = false;
                }

                frameTimer += animationSpeed;

                Vector3 newPosition = (Vector3)newCaracrer.transform.position;
                newPosition[0] -= 0.1f;
                newPosition[1] += randomMove;

                newCaracrer.transform.position = newPosition;
                
                if (newPosition[0] < randomStop)
                {
                    isNew = false;
                    StartCoroutine(Pause());
                }                    
            }
        }
    }

    IEnumerator Pause() 
    {
        float time = Random.Range(5, 10);
        yield return new WaitForSeconds(time);

        isNew = true;
        isFirst = true;
    }
}
