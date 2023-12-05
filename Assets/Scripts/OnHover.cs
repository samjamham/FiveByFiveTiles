using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour
{
    [SerializeField]
    private GameObject UIToShow;


    private void OnMouseOver()
    {
        if (!transform.parent.GetComponent<FlipAndChange>().IsMidFlip)
            GetComponent<Outline>().enabled = true;
    }
    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
    }
    private void OnMouseDown()
    {
        if (GetComponent<Outline>().enabled && EventSystem.current.IsPointerOverGameObject() == false)
        {
            UIToShow.GetComponentInChildren<ChangeCube>().SetCube(this.transform.parent.gameObject, 
                transform.parent.GetComponent<FlipAndChange>().GetTerrainType(),
                transform.parent.GetComponent<FlipAndChange>().GetImprovementType());

            UIToShow.SetActive(false);
            UIToShow.SetActive(true);
        }
           
    }

    public void SetUI(GameObject NewUI)
    {
        UIToShow = NewUI;
    }
}
