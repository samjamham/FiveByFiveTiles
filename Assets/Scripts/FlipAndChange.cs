using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAndChange : MonoBehaviour
{
    [SerializeField]
    private GameObject UIToShow;


    private bool ToDestroy = false;
    //private GameObject CurrentMesh;
    private int TerrainType = -1;
    private int ImprovementType = -1;
    public bool IsMidFlip { private set; get; } = false;

    private float DesiredAngle = 0;
    float r;

    public void NewMeshSelected(int NewTerrain, int NewImprovment)
    {
        if(NewTerrain != TerrainType 
            || NewImprovment != ImprovementType
            && !IsMidFlip)
        {
            TerrainType = NewTerrain;
            ImprovementType = NewImprovment;
            GameObject NewMesh = Instantiate(GetComponentInParent<ListOfTerrains>().TypesOfCube2D[NewTerrain,NewImprovment], this.transform.position, this.transform.rotation);
            int randomRotation = Random.Range(0, 4) * 90;
            NewMesh.transform.localRotation = Quaternion.Euler(0, randomRotation, 180);
            NewMesh.transform.SetParent(this.transform);
            NewMesh.GetComponent<OnHover>().SetUI(UIToShow);
            DesiredAngle += 185f;
            ToDestroy = true;
            IsMidFlip = true;
        }
    }

    private void FixedUpdate()
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, DesiredAngle, ref r, 0.5f);

        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (ToDestroy && IsWithinRange(transform.rotation.eulerAngles.z, DesiredAngle % 360))
        {
            Destroy(GetComponent<Transform>().GetChild(0).gameObject);
            ToDestroy = false;
            IsMidFlip = false;
            DesiredAngle -= 5f;
        }
    }
    private bool IsWithinRange(float Value, float target)
    {
        if (target - 5 < Value && target + 5 > Value)
            return true;
        else
            return false;
    }

    public int GetTerrainType()
    {
        return TerrainType;
    }
    public int GetImprovementType()
    {
        return ImprovementType;
    }
    public void SetUI(GameObject NewUI)
    {
        UIToShow = NewUI;
    }
}
