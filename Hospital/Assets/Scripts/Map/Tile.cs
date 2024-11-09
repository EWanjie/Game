using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _hightlight;
   
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    // Update is called once per frame
    void OnMouseEnter()
    {
        _hightlight.SetActive(true);
        Debug.Log($"Tile");
    }

   void OnMouseExit()
    {
        _hightlight.SetActive(false);
    }
}
