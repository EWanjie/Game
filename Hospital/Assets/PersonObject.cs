using UnityEngine;
using static HelpStation;

public class PersonObject : MonoBehaviour
{

    public enum StationType
    {
        Surgeon,
        Therapist,
        Psychologist,
        WatingRoom
    }

    public class Station
    {
        public float life;


        public StationType stationType;
        public float duration;
        [System.NonSerialized] public Vector2 coordinates = Vector2.zero;
        [System.NonSerialized] public Vector2 scale = Vector2.zero;
        [System.NonSerialized] public bool move = false;
    }
}
