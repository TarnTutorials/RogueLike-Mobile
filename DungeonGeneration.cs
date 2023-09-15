using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DungeonGeneration : MonoBehaviour
{
    [SerializeField] protected TilemapPainter tilemapPainter = null;
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tilemapPainter.Clear();
        ProceduralGeneration();
    }

    protected abstract void ProceduralGeneration();
}
