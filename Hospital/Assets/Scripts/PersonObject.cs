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
    private Ñharacter ñharacter = new Ñharacter();

    private float dyingTimer;
    private float dyingSpeed;
    private const float normalDyingSpeed = 1f;
    private const float watingDyingSpeed = 2 * normalDyingSpeed;

    private const int timeReserve = 10;
    private const int maxLife = 100;
    private int saveDoctor;

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

        saveDoctor = 100 / (ñharacter.doctorsList.Count + 1);
        ñharacter.life = saveDoctor + timeReserve;
        
        dyingSpeed = normalDyingSpeed;
        dyingTimer = dyingSpeed;

        barManager.UpdateHealthBar(ñharacter.life);
    }

    private void Update()
    {
        if (ñharacter.isTreatment)
            return;

        barManager.UpdateHealthBar(ñharacter.life);

        if (ñharacter.life <= 0 || ñharacter.doctorsList.Count == 0) // óíè÷òîæåíèå ïåğñîíàæà
        {
            AnnihilateÑharacter();
            return;
        }

        dyingTimer -= Time.deltaTime;

        if (dyingTimer < 0f)
        {
            dyingTimer += dyingSpeed;
            ñharacter.life--;        
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData) //èíôîğìàöèÿ î äîêòîğàõ
    {
        if (ñharacter.isTreatment)
            return;

        if (eventData.clickCount == 2)
        {
            chatManager.Show(true);
        }
    }

    public void SetTreatment(HelthType stationType) // íà÷àëî ëå÷åíèÿ
    {
        chatManager.Show(false);
        barManager.SetTransparency(ñharacter.isTreatment);
        ñharacter.isTreatment = true;
        
        int cuttuntStation = (int)stationType;
        bool isDoctor = false;

        foreach (int doctor in ñharacter.doctorsList) // íàõîäèì íóæíîãî äîêòîğà, åñëè îí åñòü
        {
            if (cuttuntStation == doctor)
            {
                ñharacter.doctorsList.Remove(doctor);
                isDoctor = true;
                break;
            }
        }

        if (isDoctor) // ëå÷èì ïåğñîíàæà
        {
            chatManager.SetDoctors(ñharacter.doctorsList);

            ñharacter.life += saveDoctor;

            if (ñharacter.life > maxLife)
                ñharacter.life = maxLife;
        }
        else // óáèâàåì ïåğñîíàæà
        {
            ñharacter.life -= saveDoctor / 2;
        }
    }

    public void SetWating(bool wating) // ëå÷åíèå
    {
        dyingSpeed = wating ? watingDyingSpeed : normalDyingSpeed;
    }

    public void SetEndHealing() // îêîí÷àíèå ëå÷åíèÿ
    {
        barManager.SetTransparency(ñharacter.isTreatment);
        ñharacter.isTreatment = false;
    }

    private void AnnihilateÑharacter() //óíè÷òîæåíèå ïåğñîíàæà
    {
        GameManager.Instance.SaveÑharacter(ñharacter.life);

        Destroy(gameObject, 1);
    }

    public void OnForeground() // ïåğåäíèé ïëàí ïåğñîíàæà è èíôîğìàöèè
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

    private void RandomDoctor() // ğàíäîìíûé íàáîğ äîêòîğîâ
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
                ñharacter.doctorsList.Add(i);
            }
        }

        chatManager.SetDoctors(ñharacter.doctorsList);
    }

    public class Ñharacter
    {
        public int life;
        public bool isTreatment = false;
        public List<int> doctorsList = new List<int>();
    }
}
