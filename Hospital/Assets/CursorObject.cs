using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CursorManager;

public class CursorObject : MonoBehaviour
{

    [SerializeField] private CursorManager.CursorType cursorType;
    private CursorManager.CursorType currentType;

    private void Start()
    {
        currentType = cursorType;
    }

    public void SetMove(bool status)
    {
        currentType = status ? cursorType : CursorManager.CursorType.Arrow;
    }

    private void OnMouseEnter()
    {
        CursorManager.Instance.SetActiveCursorType(currentType);
    }

    private void OnMouseExit() 
    {
        CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);
    }
}
