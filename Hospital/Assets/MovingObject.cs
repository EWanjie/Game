using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
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

    public enum TreatmentType
    {
        Start,
        Process
    }

    /*
        private void Start()
        {
           frameTimer = animationSpeed;
           moveAnimation[0] = (Vector2)transform.localScale;
           moveAnimation[1] = coefficient*(Vector2)transform.localScale;
        }

        private void Update() // �������� ���������
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

    private void OnMouseDown() //�������
    {
        if (treatment == TreatmentType.Process)
        {
            if (!wating) return;
            treatment = TreatmentType.Start;
        }

        difference = (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector3)transform.position;

        Priority();
    }

    private void OnMouseDrag() //�����������
    {
        if (treatment == TreatmentType.Process) return;

        transform.position = (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    private void Priority() // �� �������� ����
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
    
    private void OnTriggerStay2D(Collider2D collision) // ������ � ������� �������� �������
    {

        if (treatment == TreatmentType.Process)
            return;

        if (collision.gameObject.CompareTag("helpStation"))
        {
            HelpStation otherRigidbody = collision.gameObject.GetComponent<HelpStation>();
            Station otherCollision = otherRigidbody.StationGS;

            if (otherCollision.isFree) // ���� ������� ��������
            {
                if (Input.GetMouseButton(0)) // �������� ���� �������
                {
                    otherCollision.move = true;
                }
                else // ��������� ������ �� �������
                {
                    otherCollision.move = false;
                    otherCollision.isFree = false;
                    treatment = TreatmentType.Process;
                    //transform.localScale = moveAnimation[0];

                    OnCentre(otherCollision);

                    if (otherCollision.stationType == StationType.WatingRoom) // ������� = ������� ��������
                    {
                        wating = true;                       
                    }
                    else
                    {
                        StartCoroutine(Pause(otherCollision)); // ������� = ������� �������
                    }                                      
                }
            }
            else // ���� ������� ������
            {
                if(wating) // ������� �� ������� ��������
                {
                    wating = false;
                    otherCollision.isFree = true;
                }
                else // ������� ��������� ������ ������
                {
                    Push(otherCollision);
                }
                
            }

        }     
            
    }

    private void OnTriggerExit2D(Collider2D collision) // ������ ����� �� ������� �������� �������
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

    IEnumerator Pause(Station station) // �������. �����.
    {
        bool cursor = false;
        GetComponent<CursorObject>().SetMove(cursor);  

        yield return new WaitForSeconds(station.duration);

        cursor = !cursor;
        GetComponent<CursorObject>().SetMove(cursor);

        Push(station);

        station.isFree = true;
        treatment = TreatmentType.Start;
    }

    public void OnCentre(Station station) // ���������� � �����
    {
        Vector3 newVec = (Vector3)transform.position;
        newVec[0] = station.coordinates.x;
        newVec[1] = station.coordinates.y;
        transform.position = newVec;
    }

    public void Push(Station station) // ������������ ������� �� �������
    {
        Vector3 newPosition = (Vector3)transform.position;
        Vector3 totalScale = (Vector3)transform.localScale;

        Vector3 coordinates = (Vector3)station.coordinates;
        Vector3 scale = (Vector3)station.scale;

        newPosition[0] = coordinates[0] + scale[0]/2;
        newPosition[1] = coordinates[1] - scale[1]/2;
        transform.position = newPosition;
    }

}
