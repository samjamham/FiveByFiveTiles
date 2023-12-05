using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCube : MonoBehaviour
{
    private static GameObject pCubeToChange;
    private int mTerrainType;
    private int mImprovementType;
    
    public List<GameObject>[] ButtonSelection;    
    public GameObject ButtonList;

    public void StartUp()
    {
        ButtonSelection = new List<GameObject>[7];
        ButtonSelection[0] = new List<GameObject>();
        ButtonSelection[1] = new List<GameObject>();
        ButtonSelection[2] = new List<GameObject>();
        ButtonSelection[3] = new List<GameObject>();
        ButtonSelection[4] = new List<GameObject>();
        ButtonSelection[5] = new List<GameObject>();
        ButtonSelection[6] = new List<GameObject>();

        foreach (Transform element in ButtonList.GetComponent<Transform>())
        {
             switch (element.name.Substring(0, 1))
            {
                case "0":
                    ButtonSelection[0].Add(element.gameObject);
                    break;                
                case "1":
                    ButtonSelection[1].Add(element.gameObject);
                    break;              
                case "2":
                    ButtonSelection[2].Add(element.gameObject);
                    break;                
                case "3":
                    ButtonSelection[3].Add(element.gameObject);
                    break;                
                case "4":
                    ButtonSelection[4].Add(element.gameObject);
                    break;              
                case "5":
                    ButtonSelection[5].Add(element.gameObject);
                    break;
                default:
                    ButtonSelection[6].Add(element.gameObject);
                    break;
             }
        }

    }

    public void NewCubeSelected(string NewTerrainAndImprovement)
    {
        int TerrainType = int.Parse(NewTerrainAndImprovement.Substring(0,1)), ImprovementType = int.Parse(NewTerrainAndImprovement.Substring(1, 1));
        pCubeToChange.GetComponent<FlipAndChange>().NewMeshSelected(TerrainType, ImprovementType);
    }

    public void SetCube(GameObject InCube, int Ttype, int Itype)
    {
        pCubeToChange = InCube;
        mTerrainType = Ttype;
        mImprovementType = Itype;
        SetButtons();
    }
    public void SetButtons()
    {
        if (ButtonSelection == null)
            return;
        for (int i = 0; i < ButtonSelection.GetLength(0); i++)
        {
            for (int j = 0; j < ButtonSelection[i].Count; j++)
            {
                ButtonSelection[i][j].SetActive(false);
            }
        }
            switch (mTerrainType) 
        {

            case 0:
                //Show Plains
                foreach(GameObject element in ButtonSelection[0])
                { 
                    element.SetActive(true);
                    if (mImprovementType != 7 ^ mImprovementType == 8)
                    {
                        if (element.name.Substring(1,1) == "8")
                            element.SetActive(false);
                    }
                }

                break;
            case 1:
                //Show Desert
                foreach (GameObject element in ButtonSelection[1])
                {
                    element.SetActive(true);
                }
                break;
            case 2:
                //Show Hills
                foreach (GameObject element in ButtonSelection[2])
                {
                    element.SetActive(true);
                }
                break;
            case 3:
                //Show Mountains
                foreach (GameObject element in ButtonSelection[3])
                {
                    element.SetActive(true);
                }
                break;
            case 4:
                //Show Forest
                foreach (GameObject element in ButtonSelection[4])
                {
                    element.SetActive(true);
                }
                break;
            case 5:
                //Show Water
                foreach (GameObject element in ButtonSelection[5])
                {
                    element.SetActive(true);
                }
                break;
            default:
                foreach (GameObject element in ButtonSelection[6])
                {
                    element.SetActive(true);
                }
                break;
        }

    }
}
