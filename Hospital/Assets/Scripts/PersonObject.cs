using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using static HelpStation;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor;
using static PersonObject;
using static CursorManager;
using static TypeStation;

public class PersonObject : MonoBehaviour, IPointerClickHandler
{
    private �haracter �haracter = new �haracter();

    private float dyingTimer;
    private float dyingSpeed;
    private const float normalDyingSpeed = 1f;
    private const float watingDyingSpeed = 2 * normalDyingSpeed;

    private const int timeReserve = 10;
    private const int maxLife = 100;
    private int saveDoctor;

    private bool isExist = true;

    private BarManager barManager;
    private ChatManager chatManager;
    private CanvasManager canvasManager;

    private void Awake()
    {
        barManager = GetComponentInChildren<BarManager>();
        chatManager = GetComponentInChildren<ChatManager>();
        canvasManager = GetComponentInChildren<CanvasManager>();
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
        if (�haracter.isTreatment || !isExist)
            return;

        dyingTimer -= Time.deltaTime;

        if (dyingTimer < 0f)
        {
            dyingTimer += dyingSpeed;
            �haracter.life--;        
        }

        barManager.UpdateHealthBar(�haracter.life);

        if (�haracter.life <= 0 || �haracter.doctorsList.Count == 0) // ����������� ���������
        {
            isExist = false;
            Annihilate�haracter();
            return;
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData) //���������� � ��������
    {
        if (�haracter.isTreatment)
            return;

        if (eventData.clickCount == 2)
        {
            chatManager.Show(true);
        }
    }

    public void SetTreatment(HelthType stationType) // ������ �������
    {
        Color startColor = new Color(1, 1, 1, 0);
        SpriteRenderer sprite;
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = startColor;

        chatManager.Show(false);
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
            chatManager.SetDoctors(�haracter.doctorsList);

            �haracter.life += saveDoctor;

            if (�haracter.life > maxLife)
                �haracter.life = maxLife;
        }
        else // ������� ���������
        {
            �haracter.life -= saveDoctor / 2;
        }
    }

    public void SetWating(bool wating) // �������
    {
        dyingSpeed = wating ? watingDyingSpeed : normalDyingSpeed;
    }

    public void SetEndHealing() // ��������� �������
    {
        barManager.SetTransparency(�haracter.isTreatment);

        Color startColor = new Color(1, 1, 1, 1);
        SpriteRenderer sprite;
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = startColor;

        �haracter.isTreatment = false;
    }

    private void Annihilate�haracter() //����������� ���������
    {
        GameManager.Instance.Save�haracter(�haracter.life);

        Destroy(gameObject, 0.5f);
    }

    public void OnForeground() // �������� ���� ��������� � ����������
    {
        Active();
        canvasManager.Active();
        chatManager.Active();
    }

    private void Active()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (sprite)
            sprite.sortingOrder = 100;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("character"))
        {
            SpriteRenderer otherSprite = obj.GetComponent<SpriteRenderer>();
            if (obj != gameObject && otherSprite)
            {
                otherSprite.sortingOrder = 0;
            }
        }
    }

    private void RandomDoctor() // ��������� ����� ��������
    {
        int size = GameManager.coutnStations;
        int max = GameManager.max;

        int randomPick = Random.Range(1, max);
        string binaryNmb = Convert.ToString(randomPick, 2);

        string fullString = new string('0', size - binaryNmb.Length);
        fullString += binaryNmb;

        for (int i = 0; i < fullString.Length; i++)
        {
            if (fullString[i] == '1')
            {
                �haracter.doctorsList.Add(i);
            }
        }

        chatManager.SetDoctors(�haracter.doctorsList);
    }

    public class �haracter
    {
        public int life;
        public bool isTreatment = false;
        public List<int> doctorsList = new List<int>();
    }
}
