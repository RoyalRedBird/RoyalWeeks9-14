using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileScript : MonoBehaviour
{

    public Tilemap tileMap;

    public Tile grass;
    public Tile stone;

    public Knight knight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int gridPos = tileMap.WorldToCell(mousePos);

            Debug.Log(gridPos);

            TileBase tileSelect = tileMap.GetTile(gridPos);

            if(tileSelect == stone)
            {

                Debug.Log("This is a stone tile, but now it isn't.");
                //tileMap.SetTile(gridPos, grass);
                knight.UpdateDestination(mousePos);

            }
            else if(tileSelect == grass)
            {

                Debug.Log("This is a grass tile, but now it isn't.");
                //tileMap.SetTile(gridPos, stone);

            }

        }
        
    }
}
