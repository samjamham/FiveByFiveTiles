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
            int type = Random.Range(0, 6);
            int NoImporvement = 0; // 0 represents no improvement
            RandomCubes.SetCube(Cubes[i], type, NoImporvement); 
            RandomCubes.NewCubeSelected((type.ToString() + "0"));
        }
    }
}
