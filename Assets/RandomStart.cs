using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStart : MonoBehaviour
{
    public GameObject[] Cubes;
    [SerializeField]
    private ChangeCube RandomCubes;
    // Start is called before the first frame update
    void Start()
    {
       for (int i = 0; i < Cubes.Length; i++)
        {
            RandomCubes.SetCube(Cubes[i]);
            RandomCubes.NewCubeSelected(Random.Range(0, 8));
        }
    }
}
