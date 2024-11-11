using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HelpStation;

public class HelpStation : MonoBehaviour
{
    public static HelpStation Instance { get; private set; }

    [SerializeField] private Station station;

    public Station StationGS { get { return station; } }
   
    /*
    public enum StationType
    {
        Surgeon,
        Therapist,
        Psychologist,
        WatingRoom
    }
    */

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        station.scale = (Vector2)transform.localScale;
        station.coordinates = (Vector2)transform.position;

        Sprite sprite = TypeStation.Instance.GetSprite((int)station.stationType);
        GetComponent<SpriteRenderer>().sprite = sprite;

        //Destroy(GetComponent<PolygonCollider2D>());
        //gameObject.AddComponent<PolygonCollider2D>();

        //gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
    }


    private void Update()
    {
        FindColor(station.isFree, station.move);
    }

    private void FindColor(bool isFree, bool move)
    {
        Color startColor = new Color(1, 1, 1, 1);

        if (!isFree)
        {
            startColor = new Color(1, 0, 0, 1);
        }
        else if (move)
        {
            startColor = new Color(1, 1, 0, 1);
        }

        SpriteRenderer sprite;
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = startColor;
    }

    [System.Serializable]
    public class Station
    {
        public bool isFree = true;
        public float duration = 10;

        //public StationType stationType;
        public TypeStation.HelthType stationType;
        //public Sprite textureStation;


        [System.NonSerialized] public Vector2 coordinates = Vector2.zero;
        [System.NonSerialized] public Vector2 scale = Vector2.zero;
        [System.NonSerialized] public bool move = false;
    }

}
