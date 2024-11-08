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
    private Vector2[] moveAnimation = {Vector2.zero, Vector2.zero};

    private Vector2 difference = Vector2.zero;

    private TreatmentType treatment = TreatmentType.Start;

    public enum TreatmentType
    {
        Start,
        Process,
        End
    }    

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

    private void OnMouseDown() //нажатие
    {
        if(treatment == TreatmentType.Process) return;

        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;

        Priority();
    }

    private void OnMouseDrag() //удерживание
    {
        if (treatment == TreatmentType.Process) return;

        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    private void Priority() // на передний план
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

    private void OnCollisionStay2D(Collision2D collision) // объект в области действия станции
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
                }
                else // объект стоит на станции
                {
                    transform.localScale = moveAnimation[0];

                    otherCollision.move = false;
                    otherCollision.isFree = false;
                    transform.position = otherCollision.coordinates;

                    GetComponent<CursorObject>().SetMove(otherCollision.isFree);

                    treatment = TreatmentType.Process;
                    StartCoroutine(Pause(otherCollision.duration));
                }
            }
            else if (!Input.GetMouseButton(0)) // если станция занята, но на нее пытаюстя поставить объект
            {
                if (treatment == TreatmentType.End) // лечение окончено
                {
                    treatment = TreatmentType.Start;
                    otherCollision.isFree = true;

                    GetComponent<CursorObject>().SetMove(otherCollision.isFree);
                }

                Push(otherCollision.coordinates, otherCollision.scale);
            }

        }     
            
    }

    private void OnCollisionExit2D(Collision2D collision) // объект вышел из области действия станции
    {
        if (collision.gameObject.CompareTag("character")) 
        {
            gameObject.GetComponent<Renderer>().sortingOrder--;     
        }

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

    IEnumerator Pause(float time) // лечение. пауза.
    {
        yield return new WaitForSeconds(time);
        treatment = TreatmentType.End;
    }

    public void Push(Vector2 coordinates, Vector2 scale) // выталкивание объекта из станции
    {
        Vector2 newPosition = Vector2.zero;
        Vector2 totalScale = (Vector2)transform.localScale;
        newPosition[0] = coordinates[0] + scale[0]/2;
        newPosition[1] = coordinates[1] - scale[1]/2;
        transform.position = newPosition;
    }

}
