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
    private �haracter �haracter = new �haracter();

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

        saveDoctor = 100 / (�haracter.doctorsList.Count + 1);
        �haracter.life = saveDoctor + timeReserve;
        
        dyingSpeed = normalDyingSpeed;
        dyingTimer = dyingSpeed;

        barManager.UpdateHealthBar(�haracter.life);
    }

    private void Update()
    {
        if (�haracter.isTreatment)
            return;

        barManager.UpdateHealthBar(�haracter.life);

        if (�haracter.life <= 0 || �haracter.doctorsList.Count == 0) // ����������� ���������
        {
            Annihilate�haracter();
            return;
        }

        dyingTimer -= Time.deltaTime;

        if (dyingTimer < 0f)
        {
            dyingTimer += dyingSpeed;
            �haracter.life--;        
        }
    }

    public void SetWating(bool wating)
    {
        dyingSpeed = wating ? watingDyingSpeed : normalDyingSpeed;
    }

    public void SetEndHealing()
    {
        barManager.SetTransparency(�haracter.isTreatment);
        �haracter.isTreatment = false;
    }

    public void SetTreatment(StationType stationType)
    {
        barManager.SetTransparency(�haracter.isTreatment);
        �haracter.isTreatment = true;
        
        int cuttuntStation = (int)stationType;
        bool isDoctor = false;

        foreach (int doctor in �haracter.doctorsList) // ������� ������� �������, ���� �� ����
        {
            if (cuttuntStation == doctor)
            {
                �haracter.doctorsList.Remove(doctor);
                isDoctor = true;
                break;
            }
        }

        if (isDoctor) // ����� ���������
        {
            �haracter.life += saveDoctor;

            if (�haracter.life > maxLife)
                �haracter.life = maxLife;
        }
        else // ������� ���������
        {
            �haracter.life -= saveDoctor / 2;
        }
    }

    private void Annihilate�haracter()
    {
        GameManager.Instance.Save�haracter(�haracter.life);

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
                �haracter.doctorsList.Add(i);
            }
        }
    }

    public class �haracter
    {
        public int life;
        public bool isTreatment = false;
        public List<int> doctorsList = new List<int>();
    }
}
