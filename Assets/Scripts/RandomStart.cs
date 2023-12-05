using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStart : MonoBehaviour
{
    public GameObject[] Cubes;
    [SerializeField]
    private ChangeCube CubeController;
    // Start is called before the first frame update
    void Start()
    {
        RandomizeCubes();
    }

    public void RandomizeCubes()
    {
        for (int i = 0; i < Cubes.Length; i++)
        {
            int type = Random.Range(0, 6);
            int NoImporvement = 0; // 0 represents no improvement
            CubeController.SetCube(Cubes[i], type, NoImporvement);
            CubeController.NewCubeSelected((type.ToString() + "0"));
        }
    }
}
