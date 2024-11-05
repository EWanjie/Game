using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    
    public static CursorManager Instance {  get; private set; }

    [SerializeField] private List<CursorAnimation> cursorAnimationList;

    private CursorAnimation cursorAnimation;

    private int frameCount; 
    private int currentFrame;
    private bool isButton = false;
    private float frameTimer;
    private CursorType rememberCursor;

    public enum CursorType
    {
        Arrow,
        Grab,
        Move
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetActiveCursorType(CursorType.Arrow);
        rememberCursor = CursorType.Arrow;
    }

    private void Update()
    {

        if (Input.GetMouseButton(0) != isButton)
        {

            isButton = Input.GetMouseButton(0);

            if (isButton) // Зажато
            {
                rememberCursor = cursorAnimation.cursorType;
                SetActiveCursorType(CursorType.Move);
            }
            else //Отжато
            {
                SetActiveCursorType(rememberCursor);
            }

        }


        frameTimer -= Time.deltaTime;

        if (frameTimer < 0f)
        {
            frameTimer += cursorAnimation.frameRate;
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offset, CursorMode.Auto);
        }

    }

    public void SetActiveCursorType(CursorType cursorType)
    {
        if (isButton && cursorType != CursorType.Move) 
        {
            rememberCursor = cursorType;
        } 
        else
        {
            SerActiveCursorAnimation(GetCursorAnimation(cursorType));
        }    
    }

    private CursorAnimation GetCursorAnimation(CursorType cursorType)
    {
        foreach (CursorAnimation cursorAnimation in cursorAnimationList)
        {
            if (cursorType == cursorAnimation.cursorType)
            {
                return cursorAnimation;
            }
        }

        return null;
    }

    private void SerActiveCursorAnimation (CursorAnimation cursorAanimation)
    {
        this.cursorAnimation = cursorAanimation;
        currentFrame = 0;
        frameTimer = cursorAnimation.frameRate;
        frameCount = cursorAnimation.textureArray.Length;
    }

    
    [System.Serializable]
    public class CursorAnimation
    {
        public CursorType cursorType;
        public Texture2D[] textureArray;
        public float frameRate;
        public Vector2 offset;
    }

}
