using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HelpStation;

public class HelpStation : MonoBehaviour
{
    public static HelpStation Instance { get; private set; }

    [SerializeField] private Station station;

    public Station StationGS { get { return station; } }

    public enum StationType
    {
        Surgeon,
        Therapist,
        Psychologist,
        WatingRoom
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        station.scale = (Vector2)transform.localScale;
        station.coordinates = (Vector2)transform.position;
        FindColor(station.isFree, station.move);
    }

    private void Update()
    {
        FindColor(station.isFree, station.move);
    }

    private void FindColor(bool isFree, bool move)
    {
        Color startColor;
        if (!isFree)
        {
            startColor = new Color(1, 0, 0, 1);
        }
        else if (move)
        {
            startColor = new Color(1, 1, 0, 1);
        }
        else
        {
            switch (station.stationType)
            {
                case StationType.Surgeon:
                    startColor = new Color(0, 1, 0, 1);
                    break;

                case StationType.Therapist:
                    startColor = new Color(1, 1, 1, 1);
                    break;

                case StationType.Psychologist:
                    startColor = new Color(0, 0, 0, 1);
                    break;

                default:
                    startColor = new Color(0, 0, 1, 1);
                    break;
            }

            
        }

        SpriteRenderer sprite;
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = startColor;
    }

    [System.Serializable]
    public class Station
    {
        public bool isFree = true;  
        public StationType stationType;
        public float duration;
        [System.NonSerialized] public Vector2 coordinates = Vector2.zero;
        [System.NonSerialized] public Vector2 scale = Vector2.zero;
        [System.NonSerialized] public bool move = false;
    }

}
