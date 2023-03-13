using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpreadTilemap : MonoBehaviour
{
    // Tilemap Object
    [SerializeField] private Tilemap floor;
    [SerializeField]
    private Tilemap wall;
    // Using Tilemap asset
    [SerializeField]
    private TileBase floorTile;
    [SerializeField]
    private TileBase wallTile;


    // SpreadTile
    public void SpreadFloorTilemap (HashSet<Vector2Int> positions)
    {
        SpreadTile(positions, floor, floorTile);
    }

    public void SpreadWallTilemap(HashSet<Vector2Int> positions) 
    {
        SpreadTile(positions, wall, wallTile);
    }

    private void SpreadTile(HashSet<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach(var position in positions)
        {
            tilemap.SetTile((Vector3Int)position, tile);
        }
    }

    public void ClearAllTiles()
    {
        floor.ClearAllTiles();
        wall.ClearAllTiles();  
    }

    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
