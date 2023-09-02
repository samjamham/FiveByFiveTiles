using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCube : MonoBehaviour
{
    private static GameObject CubeToChange;
    [SerializeField]
    private GameObject[] TypesOfCube;

    public void NewCubeSelected(int type)
    {
        CubeToChange.GetComponent<FlipAndChange>().NewMeshSelected(TypesOfCube[type]); 
    }

    public void SetCube(GameObject InCube)
    {
        CubeToChange = InCube;
    }
}
