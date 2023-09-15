using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TilemapPainter tilemapPainter)
    {
        var basicWallPos = FindWallsInDirections(floorPos, Direction2D.diagonalList);
        foreach( var position in basicWallPos)
        {
            tilemapPainter.PaintSingleWall(position);
        }
    }

    public static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPos, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (var position in floorPos)
        {
            foreach (var direction in directionList)
            {
                var neighborPos = position + direction;
                if(floorPos.Contains(neighborPos) == false)
                {
                    wallPos.Add(neighborPos);
                }
            }
        }
        return wallPos;
    }
}
