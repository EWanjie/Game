using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using static HelpStation;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor;
using static PersonObject;
using static CursorManager;

public class PersonObject : MonoBehaviour
{
    private —haracter Òharacter = new —haracter();

    private float dyingTimer;
    private float dyingSpeed;
    private const float normalDyingSpeed = 1f;
    private const float watingDyingSpeed = 2 * normalDyingSpeed;

    private const int timeReserve = 10;
    private const int maxLife = 100;
    private int saveDoctor;

    [SerializeField] BarManager barManager;

    private void Awake()
    {
        barManager = GetComponentInChildren<BarManager>();
    }

    private void Start()
    {
        RandomDoctor();

        saveDoctor = 100 / (Òharacter.doctorsList.Count + 1);
        Òharacter.life = saveDoctor + timeReserve;
        
        dyingSpeed = normalDyingSpeed;
        dyingTimer = dyingSpeed;

        barManager.UpdateHealthBar(Òharacter.life);
    }

    private void Update()
    {
        if (Òharacter.isTreatment)
            return;

        barManager.UpdateHealthBar(Òharacter.life);

        if (Òharacter.life <= 0 || Òharacter.doctorsList.Count == 0) // ÛÌË˜ÚÓÊÂÌËÂ ÔÂÒÓÌ‡Ê‡
        {
            Annihilate—haracter();
            return;
        }

        dyingTimer -= Time.deltaTime;

        if (dyingTimer < 0f)
        {
            dyingTimer += dyingSpeed;
            Òharacter.life--;        
        }
    }

    public void SetWating(bool wating)
    {
        dyingSpeed = wating ? watingDyingSpeed : normalDyingSpeed;
    }

    public void SetEndHealing()
    {
        barManager.SetTransparency(Òharacter.isTreatment);
        Òharacter.isTreatment = false;
    }

    public void SetTreatment(StationType stationType)
    {
        barManager.SetTransparency(Òharacter.isTreatment);
        Òharacter.isTreatment = true;
        
        int cuttuntStation = (int)stationType;
        bool isDoctor = false;

        foreach (int doctor in Òharacter.doctorsList) // Ì‡ıÓ‰ËÏ ÌÛÊÌÓ„Ó ‰ÓÍÚÓ‡, ÂÒÎË ÓÌ ÂÒÚ¸
        {
            if (cuttuntStation == doctor)
            {
                Òharacter.doctorsList.Remove(doctor);
                isDoctor = true;
                break;
            }
        }

        if (isDoctor) // ÎÂ˜ËÏ ÔÂÒÓÌ‡Ê‡
        {
            Òharacter.life += saveDoctor;

            if (Òharacter.life > maxLife)
                Òharacter.life = maxLife;
        }
        else // Û·Ë‚‡ÂÏ ÔÂÒÓÌ‡Ê‡
        {
            Òharacter.life -= saveDoctor / 2;
        }
    }

    private void Annihilate—haracter()
    {
        GameManager.Instance.Save—haracter(Òharacter.life);

        Destroy(gameObject, 1);
    }
    private void RandomDoctor()
    {
        int size = GameManager.coutnStations;
        int max = GameManager.max;

        int randomPick = Random.Range(1, max);
        string binaryNmb = Convert.ToString(randomPick, 2);

        string fullString = new string('0', size - binaryNmb.Length);
        fullString += binaryNmb;

        Debug.Log(fullString);

        for (int i = 0; i < fullString.Length; i++)
        {
            if (fullString[i] == '1')
            {
                Òharacter.doctorsList.Add(i);
            }
        }
    }

    public class —haracter
    {
        public int life;
        public bool isTreatment = false;
        public List<int> doctorsList = new List<int>();
    }
}
