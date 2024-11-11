using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CursorManager;

public class TypeStation : MonoBehaviour
{
    public static TypeStation Instance { get; private set; }

    [SerializeField] public List<HelthStation> helthStetionList;
    public enum HelthType
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

    public Sprite GetSprite(int helthType)
    {
        foreach (HelthStation obj in helthStetionList)
        {
            if (helthType == (int)obj.stationType)
            {
                return obj.textureStation;
            }
        }
        return null;
    }

    [System.Serializable]
    public class HelthStation
    {
        public HelthType stationType;
        public Sprite textureStation;
    }
}