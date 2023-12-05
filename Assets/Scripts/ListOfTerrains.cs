using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfTerrains : MonoBehaviour
{
    [SerializeField]
    public GameObject[] TypesOfCube;

    [SerializeField]
    public GameObject[,] TypesOfCube2D;

    private void Awake()
    {
        TypesOfCube2D = new GameObject[6, 10];

        TypesOfCube2D[0,0] = Resources.Load("0_0_Plain") as GameObject;
        TypesOfCube2D[0,1] = Resources.Load("0_1_Plain_Farm") as GameObject;
        TypesOfCube2D[0,7] = Resources.Load("0_7_Plain_House") as GameObject;
        TypesOfCube2D[0,8] = Resources.Load("0_8_Plain_City") as GameObject;

        TypesOfCube2D[1,0] = Resources.Load("1_0_Desert") as GameObject;

        TypesOfCube2D[2,0] = Resources.Load("2_0_Hills") as GameObject;

        TypesOfCube2D[3,0] = Resources.Load("3_0_Moutains") as GameObject;

        TypesOfCube2D[4,0] = Resources.Load("4_0_Forest") as GameObject;

        TypesOfCube2D[5,0] = Resources.Load("5_0_water") as GameObject;
        TypesOfCube2D[5,4] = Resources.Load("5_4_water_Fish") as GameObject;
    }
}
