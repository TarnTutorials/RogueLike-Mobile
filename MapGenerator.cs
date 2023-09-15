using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : DungeonGeneration
{
    [SerializeField] protected int iterations = 10;
    [SerializeField] public int walkLength = 10;
    [SerializeField] public bool startRandomly = true;

    protected override void ProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = RandomWalk(startPos, iterations, walkLength, startRandomly);
        tilemapPainter.Clear();
        tilemapPainter.PaintFloor(floorPos);
        WallGenerator.CreateWalls(floorPos, tilemapPainter);
    }

    protected HashSet<Vector2Int> RandomWalk( Vector2Int start, int times, int length, bool randomStart)
    {
        var currentPos = start;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for(int i = 0; i < times; i++)
        {
            var path = GenerationAlgorithm.RandomWalk(currentPos, length);
            floorPos.UnionWith(path);
            if(randomStart)
            {
                currentPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
            }
        }
        return floorPos;
    }
}
