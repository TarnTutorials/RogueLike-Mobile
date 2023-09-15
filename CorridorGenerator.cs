using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorGenerator : MapGenerator
{
    [SerializeField] private int corridorLength, corridorCount;
    [SerializeField] private float roomPercent;

    protected override void ProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();
        GenerateCorridors(floorPos, potentialRoomPos);
        HashSet<Vector2Int> roomPos = CreateRooms(potentialRoomPos);
        List<Vector2Int> deadEnds = FindDeadEnds(floorPos);
        CreateRoomsAtDeadEnd(deadEnds, roomPos);
        floorPos.UnionWith(roomPos);
        tilemapPainter.PaintFloor(floorPos);
        WallGenerator.CreateWalls(floorPos, tilemapPainter);
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPos)
    {
        foreach (var position in deadEnds)
        {
            if(roomPos.Contains(position)== false)
            {
                var room = RandomWalk(position, iterations, walkLength, startRandomly);
                roomPos.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindDeadEnds(HashSet<Vector2Int> floorPos)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach( var position in floorPos)
        {
            int neighbourCount = 0;
            foreach(var direction in Direction2D.directionList)
            {
                if(floorPos.Contains(position + direction))
                {
                    neighbourCount++;
                }
            }
            if(neighbourCount == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPos)
    {
        HashSet<Vector2Int> roomPos =  new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPos.Count * roomPercent);
        
        List<Vector2Int> roomToCreate = potentialRoomPos.OrderBy(x => System.Guid.NewGuid()).Take(roomToCreateCount).ToList();
        foreach(var roomPosition in roomToCreate)
        {
            var roomFloor = RandomWalk(roomPosition, iterations, walkLength, startRandomly);
            roomPos.UnionWith(roomFloor);
        }
        return roomPos;
    }

    private void GenerateCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialRoomPos)
    {
        var currentPos = startPos;
        potentialRoomPos.Add(currentPos);
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = GenerationAlgorithm.RandomCorridor(currentPos, corridorLength);
            currentPos = corridor[corridor.Count-1];
            potentialRoomPos.Add(currentPos);
            floorPos.UnionWith(corridor);
        }
    }
}
