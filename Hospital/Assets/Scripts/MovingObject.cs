using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UIElements;
using static CursorManager;
using static HelpStation;

public class MovingObject : MonoBehaviour
{
    private float frameTimer;
    private int currentFrame = 0;
    private const float animationSpeed = 0.2f;
    private const float coefficient = 0.85f;
    private Vector3[] moveAnimation = {Vector3.zero, Vector3.zero};

    private bool wating = false;

    private Vector3 difference = Vector3.zero;

    private TreatmentType treatment = TreatmentType.Start;

    private Station stationСrutch;

    ChatManager chatManager;

    public enum TreatmentType
    {
        Start,
        Process
    }

    private void Awake()
    {
        chatManager = GetComponentInChildren<ChatManager>();
    }

/*
    
        private void Start()
        {
           frameTimer = animationSpeed;
           moveAnimation[0] = (Vector2)transform.localScale;
           moveAnimation[1] = coefficient*(Vector2)transform.localScale;
        }

        private void Update() // движение персонажа
        {
            if (treatment == TreatmentType.Process) return;

            frameTimer -= Time.deltaTime;

            if (frameTimer < 0f)
            {
                frameTimer += animationSpeed;
                currentFrame = (currentFrame + 1) % 2;
                transform.localScale = moveAnimation[currentFrame];
            }
        }
*/    

    private void OnMouseDown() //нажатие
    {
        if (treatment == TreatmentType.Process)
        {
            if (!wating) return;
            treatment = TreatmentType.Start;
        }

        difference = (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector3)transform.position;

        GetComponent<PersonObject>().OnForeground(); 
    }

    private void OnMouseDrag() //удерживание
    {
        if (treatment == TreatmentType.Process) return;

        transform.position = (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

 
    private void OnMouseUp() //поднятие клавиши, костыль
    {
        if (treatment == TreatmentType.Process) return;

        if (stationСrutch != null && stationСrutch.move)
        {
            stationСrutch.move = false;
            stationСrutch.isFree = false;
            treatment = TreatmentType.Process;
            //transform.localScale = moveAnimation[0];

            OnCentre(stationСrutch);

            if (stationСrutch.stationType == TypeStation.HelthType.WatingRoom) // станция = комната ожидания
            {
                wating = true;
                GetComponent<PersonObject>().SetWating(wating);
            }
            else
            {
                StartCoroutine(Pause(stationСrutch)); // станция = кабинет доктора
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision) // объект в области действия станции
    {

        if (treatment == TreatmentType.Process)
            return;

        if (collision.gameObject.CompareTag("helpStation"))
        {
            HelpStation otherRigidbody = collision.gameObject.GetComponent<HelpStation>();
            Station otherCollision = otherRigidbody.StationGS;

            if (otherCollision.isFree) // если станция свободна
            {
                if (Input.GetMouseButton(0)) // движение мимо станции
                {
                    otherCollision.move = true;
                    stationСrutch = otherCollision;
                }
                else // поставить объект на станцию
                {
                    otherCollision.move = false;
                    otherCollision.isFree = false;
                    treatment = TreatmentType.Process;
                    //transform.localScale = moveAnimation[0];

                    OnCentre(otherCollision);

                    if (otherCollision.stationType == TypeStation.HelthType.WatingRoom) // станция = комната ожидания
                    {
                        wating = true;
                        GetComponent<PersonObject>().SetWating(wating);
                    }
                    else
                    {
                        StartCoroutine(Pause(otherCollision)); // станция = кабинет доктора
                    }                                      
                }
            }
            else // если станция занята
            {
                if(wating) // забрать из комнаты ожидания
                {
                    wating = false;
                    otherCollision.isFree = true;
                    GetComponent<PersonObject>().SetWating(wating);
                }
                else // попытка поставить второй объект
                {
                    Push(otherCollision);
                }
                
            }

        }     
            
    }

    private void OnTriggerExit2D(Collider2D collision) // объект вышел из области действия станции
    {
        if (collision.gameObject.CompareTag("helpStation"))
        {
            HelpStation otherRigidbody = collision.gameObject.GetComponent<HelpStation>();
            Station otherCollision = otherRigidbody.StationGS;

            if (otherCollision.isFree)
            {
                otherCollision.move = false;
            }
        }
    }

    IEnumerator Pause(Station station) // лечение. пауза.
    {
        GetComponent<PersonObject>().SetTreatment(station.stationType);

        bool cursor = false;
        GetComponent<CursorObject>().SetMove(cursor);

        yield return new WaitForSeconds(station.duration);

        cursor = !cursor;
        GetComponent<CursorObject>().SetMove(cursor);

        Push(station);

        station.isFree = true;
        treatment = TreatmentType.Start;

        GetComponent<PersonObject>().SetEndHealing();
    }

    public void OnCentre(Station station) // постановка в центр
    {
        Vector3 newVec = (Vector3)transform.position;
        Vector3 totalScale = (Vector3)transform.localScale;
        Vector3 scale = (Vector3)station.scale;

        newVec[0] = station.coordinates.x;
        newVec[1] = station.coordinates.y + scale[1] + totalScale[1];
        transform.position = newVec;
    }

    public void Push(Station station) // выталкивание объекта из станции
    {
        Vector3 newPosition = (Vector3)transform.position;
        Vector3 totalScale = (Vector3)transform.localScale;

        Vector3 coordinates = (Vector3)station.coordinates;
        Vector3 scale = (Vector3)station.scale;

        newPosition[0] = coordinates[0] + 5 * (scale[0] + totalScale[0]);
        //newPosition[1] = coordinates[1] - totalScale[1];
        transform.position = newPosition;
    }

}
