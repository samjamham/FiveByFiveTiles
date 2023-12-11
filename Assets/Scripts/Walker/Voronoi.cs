using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class VoronoiNoise
{
    private List<Vector2> Points = new List<Vector2>();
    private List<int> TerrainTypeArray = new List<int>();
    private int mWidth, mHeight;
    private int[,] Grid;

    public int[,] GenerateNew(int Width, int Height, float percent)
    {
        mWidth = Width; mHeight = Height;
        Grid = new int[mWidth, mHeight];
        CreatePoints((int)((mWidth * mHeight) * percent));
        CreateTerrains();
        CreateSites();

        Points.Clear();
        TerrainTypeArray.Clear();
        return Grid;
    }

    int DistanceSqrd(Vector2 point, int x, int y)
    {
        float xd = x - point.x;
        float yd = y - point.y;
        return (int)((xd * xd) + (yd * yd));
    }

    void CreatePoints(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Points.Add(new Vector2(Random.Range(2, mWidth), Random.Range(2, mHeight)));
        }
    }

    void CreateTerrains()
    {
        for (int i = 0; i < Points.Count; i++)
        {
            int NewTerrain = Random.Range(0, 6); //Random.ColorHSV(0.1f,0.9f);
            TerrainTypeArray.Add(NewTerrain);
        }
    }

    void CreateSites()
    {
        int d;
        for (int hh = 0; hh < mHeight; hh++)
        {
            for (int ww = 0; ww < mWidth; ww++)
            {
                int TerrainIndex = -1, dist = int.MaxValue;
                for (int PointIndex = 0; PointIndex < Points.Count; PointIndex++)
                {
                    d = DistanceSqrd(Points[PointIndex], ww, hh);
                    if (d < dist)
                    {
                        dist = d;
                        TerrainIndex = PointIndex;
                    }
                }

                if (TerrainIndex > -1)
                Grid[ww, hh] = TerrainTypeArray[TerrainIndex];
                else
                    Debug.Log("major error please fix");
            }
        }
    }
}
