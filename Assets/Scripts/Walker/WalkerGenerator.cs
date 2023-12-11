using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerGenerator : MonoBehaviour
{
    public enum Grid
    {
        EMPTY,
        FLOOR,
        WALL        
    }

    //Variables
    private Grid[,] WalkerGrid;
    private List<WalkerObject> Walkers;


    [SerializeField]
    private int MapWidth = 10, 
        MapHeight = 10,
        MaximumWalkers = 10,
        TileCount = default;

    [SerializeField]
    private float FillPercentage = 0.24f, WaitTime = 0.05f;

    [SerializeField]
    private GameObject CubePrefab, EdgePrefab, GameMap, UIToShow ;
    [SerializeField]
    private ChangeCube CubeController;

    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        WalkerGrid = new Grid[MapWidth, MapHeight];
        Walkers = new List<WalkerObject>();

        Vector2Int TileCenter = new Vector2Int(WalkerGrid.GetLength(0) / 2, WalkerGrid.GetLength(1) / 2);

        WalkerObject curWalker = new WalkerObject(new Vector2(TileCenter.x, TileCenter.y), GetDirection(), 0.5f);
        WalkerGrid[TileCenter.x, TileCenter.y] = Grid.FLOOR;
        GenerateNewTile(new Vector2(TileCenter.x * 2 - MapWidth, TileCenter.y * 2 - MapHeight), TileCenter);
        Walkers.Add(curWalker);

        TileCount++;

        CreateFloors();
    }

    Vector2 GetDirection()
    {
        int choice = Mathf.FloorToInt(UnityEngine.Random.value * 3.99f);

        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    void CreateFloors()
    {
        while ((float)TileCount / (float)WalkerGrid.Length < FillPercentage)
        {
            foreach (WalkerObject curWalker in Walkers)
            {
                Vector2Int curPos = new Vector2Int((int)curWalker.Position.x, (int)curWalker.Position.y);

                if (WalkerGrid[curPos.x, curPos.y] != Grid.FLOOR)
                {
                    GenerateNewTile(new Vector2(curPos.x * 2 - MapWidth, curPos.y * 2 - MapHeight), curPos);
                    TileCount++;
                    WalkerGrid[curPos.x, curPos.y] = Grid.FLOOR;
                }
            }

            //Walker Methods
            ChanceToRemove();
            ChanceToRedirect();
            ChanceToCreate();
            UpdatePosition();
        }
        CreateWalls();
    }

    void ChanceToRemove()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange && Walkers.Count > 1)
            {
                Walkers.RemoveAt(i);
                break;
            }
        }
    }

    void ChanceToRedirect()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange)
            {
                WalkerObject curWalker = Walkers[i];
                curWalker.Direction = GetDirection();
                Walkers[i] = curWalker;
            }
        }
    }

    void ChanceToCreate()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange && Walkers.Count < MaximumWalkers)
            {
                Vector2 newDirection = GetDirection();
                Vector2 newPosition = Walkers[i].Position;

                WalkerObject newWalker = new WalkerObject(newPosition, newDirection, 0.9f);
                Walkers.Add(newWalker);
            }
        }
    }

    void UpdatePosition()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            WalkerObject FoundWalker = Walkers[i];
            FoundWalker.Position += FoundWalker.Direction;
            FoundWalker.Position.x = Mathf.Clamp(FoundWalker.Position.x, 1, WalkerGrid.GetLength(0) - 2);
            FoundWalker.Position.y = Mathf.Clamp(FoundWalker.Position.y, 1, WalkerGrid.GetLength(1) - 2);
            Walkers[i] = FoundWalker;
        }
    }

    void CreateWalls()
    {
        for (int x = 0; x < WalkerGrid.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < WalkerGrid.GetLength(1) - 1; y++)
            {
                if (WalkerGrid[x, y] == Grid.FLOOR)
                {
                    bool hasCreatedWall = false;

                    if (WalkerGrid[x + 1, y] == Grid.EMPTY)
                    {
                        GenerateNewEdge(new Vector2((x + 1) * 2 - MapWidth, y * 2 - MapHeight));
                        WalkerGrid[x + 1, y] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (WalkerGrid[x - 1, y] == Grid.EMPTY)
                    {
                        GenerateNewEdge(new Vector2((x - 1) * 2 - MapWidth, y  * 2 - MapHeight));                        
                        WalkerGrid[x - 1, y] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (WalkerGrid[x, y + 1] == Grid.EMPTY)
                    {
                        GenerateNewEdge(new Vector2(x * 2 - MapWidth, (y + 1) * 2 - MapHeight));
                        WalkerGrid[x, y + 1] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (WalkerGrid[x, y - 1] == Grid.EMPTY)
                    {
                        GenerateNewEdge(new Vector2(x * 2 - MapWidth, (y - 1) * 2 - MapHeight));
                        WalkerGrid[x, y - 1] = Grid.WALL;
                        hasCreatedWall = true;
                    }                    
                }
            }
        }
    }

    void GenerateNewTile(Vector2 Pos, Vector2 Noise)
    {
        GameObject NewMesh = Instantiate(CubePrefab, new Vector3(Pos.x, 0, Pos.y), Quaternion.identity);
        NewMesh.transform.SetParent(GameMap.transform);
        NewMesh.GetComponent<FlipAndChange>().SetUI(UIToShow);
        NewMesh.GetComponentInChildren<OnHover>().SetUI(UIToShow);
        int type = Random.Range(0, 6);
        //int type = PerlinGrid[(int)Noise.x, (int)Noise.y];
        int NoImporvement = 0; // 0 represents no improvement
        CubeController.SetCube(NewMesh, type, NoImporvement);
        CubeController.NewCubeSelected((type.ToString() + "0"));
    }
    void GenerateNewEdge(Vector2 Pos)
    {
        GameObject NewMesh = Instantiate(EdgePrefab, new Vector3(Pos.x, 0, Pos.y), Quaternion.identity);
        NewMesh.transform.SetParent(GameMap.transform);
    }

}