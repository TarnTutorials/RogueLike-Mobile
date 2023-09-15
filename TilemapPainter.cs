using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPainter : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap, floorDecorTileMap;
    [SerializeField] private TileBase[] floorTile, wallTile, dsDecorTile, lsDecorTile, lgDecorTile, dgDecorTile;
    private int tileStyle; //0 Dark Stone, 1 Light Stone, 2 Light Grass, 3 Dark Grass


    public void PaintFloor(IEnumerable<Vector2Int> floorPos)
    {
        tileStyle = Random.Range(0, floorTile.Length);
        PaintTiles(floorPos, floorTilemap, floorTile[tileStyle]);
        PaintDecor(floorPos, floorDecorTileMap, tileStyle);
    }

    private void PaintDecor(IEnumerable<Vector2Int> positions, Tilemap tilemap, int tileStyle)
    {
        foreach(var position in positions)
        {
            if(Random.value < 0.2)
            {
                int decorTile = 0;
                TileBase tile = dsDecorTile[decorTile];
                switch (tileStyle)
                {
                case 0:
                    decorTile = Random.Range(0, dsDecorTile.Length);
                    tile = dsDecorTile[decorTile];
                    break;
                case 1:
                    decorTile = Random.Range(0, lsDecorTile.Length);
                    tile = lsDecorTile[decorTile];
                    break;
                case 2:
                    decorTile = Random.Range(0, lgDecorTile.Length);
                    tile = lgDecorTile[decorTile];
                    break;
                case 3:
                    decorTile = Random.Range(0, dgDecorTile.Length);
                    tile = dgDecorTile[decorTile];
                    break;
                }
                PaintSingleTile(tilemap, tile, position);
            }
        }
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void PaintSingleWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTile[tileStyle], position);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        floorDecorTileMap.ClearAllTiles();
    }

}
