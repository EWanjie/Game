using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static HelpStation;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static int coutnStations = Enum.GetNames(typeof(TypeStation.HelthType)).Length - 1;
    public static int max = (int)Math.Pow(2, coutnStations);

    private int lifeStatus = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void Save—haracter(int life)
    {
        lifeStatus += life;
    }

    void Start()
    {
    }

    void Update()
    {
        
    }
}
