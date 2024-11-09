using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Grid : MonoBehaviour
{
    // Сетка карты
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tile;
    [SerializeField] private Transform _camera;
  
    [SerializeField] public TextAsset _LevelMap;

    [SerializeField] private Sprite _spriteFloor;
    [SerializeField] private Sprite _spriteCeeling1Left;
    [SerializeField] private Sprite _spriteCeeling1Right;
    [SerializeField] private Sprite _spriteCeeling1Up;
    [SerializeField] private Sprite _spriteCeeling1Bottom;
    [SerializeField] private Sprite _spriteCeelingCorner;
    [SerializeField] private Sprite _spriteCeeling2Vertical;
    [SerializeField] private Sprite _spriteCeeling2Horisontal;
    [SerializeField] private Sprite _spriteCeeling3Left;
    [SerializeField] private Sprite _spriteCeeling3Right;

    private void Start()
    {
        GenerateGrid();
    }


    void GenerateGrid()
    {

        _camera.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
        string[] dataLines = _LevelMap.text.Split('\n');

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                char tileType = char.Parse(dataLines[y].Split(',')[x]);

                //spawnedTile.Init(isOffset); 

                SpriteRenderer renderer = spawnedTile.GetComponent<SpriteRenderer>();

                if (tileType == 'C')
                    //renderer.sprite = Resources.Load<Sprite>("Tile0");
                    renderer.sprite = _spriteCeelingCorner;
                else if (tileType == 'B')
                    renderer.sprite = _spriteCeeling1Bottom;
                else if (tileType == 'R')
                    renderer.sprite = _spriteCeeling1Right;
                else if (tileType == 'L')
                    renderer.sprite = _spriteCeeling1Left;
                else if (tileType == 'U')
                    renderer.sprite = _spriteCeeling1Up;
                else if (tileType == 'v')
                    renderer.sprite = _spriteCeeling2Vertical;
                else if (tileType == '0')
                    renderer.sprite = _spriteFloor;
                else if (tileType == 'g')
                    renderer.sprite = _spriteCeeling3Left;
            }
        }

       

    }



}

/*
public class Grid : MonoBehaviour
{
    // Сетка карты
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tile;
    [SerializeField] private Transform _camera;
    [SerializeField] public TextAsset _LevelMap;

    [SerializeField] private Sprite _spriteFloor;
    [SerializeField] private Sprite _spriteCeeling1Left;
    [SerializeField] private Sprite _spriteCeeling1Right;
    [SerializeField] private Sprite _spriteCeeling1Up;
    [SerializeField] private Sprite _spriteCeeling1Bottom;
    [SerializeField] private Sprite _spriteCeelingCorner;
    [SerializeField] private Sprite _spriteCeeling2Vertical;
    [SerializeField] private Sprite _spriteCeeling2Horisontal;
    [SerializeField] private Sprite _spriteCeeling3Left;
    [SerializeField] private Sprite _spriteCeeling3Right;
)
    {
        string[] dataLines = _LevelMap.text.Split('\n');

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                //var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                //spawnedTile.Init(isOffset); 

                char tileType = char.Parse(dataLines[y].Split(',')[x]);

                // Создание тайла
                //GameObject tile = Instantiate(_tile, new Vector3(x, 0, y), Quaternion.identity);

                // Настройка тайла (например, сприта)
                SpriteRenderer renderer = spawnedTile.GetComponent<SpriteRenderer>();
                if (renderer)
                {
                    // Выбор сприта в зависимости от типа тайла
                    // Пример:

                    if (tileType == 'C')
                        //renderer.sprite = Resources.Load<Sprite>("Tile0");
                        renderer.sprite = _spriteCeelingCorner;
                    else if (tileType == 'B')
                        renderer.sprite = _spriteCeeling1Bottom;
                    else if (tileType == 'R')
                        renderer.sprite = _spriteCeeling1Right;
                    else if (tileType == 'L')
                        renderer.sprite = _spriteCeeling1Left;
                    else if (tileType == 'U')
                        renderer.sprite = _spriteCeeling1Up;
                    else if (tileType == 'v')
                        renderer.sprite = _spriteCeeling2Vertical;
                    else if (tileType == '0')
                        renderer.sprite = _spriteFloor;
                    else if (tileType == 'l')
                        renderer.sprite = _spriteCeeling3Left;
                }
            }
        }

        _camera.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

    }

    // Update is called once per frame
    */
