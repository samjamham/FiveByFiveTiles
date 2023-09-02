using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    [SerializeField]
    private GameObject UIToShow;

    private void OnMouseEnter()
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
        if (!transform.parent.GetComponent<FlipAndChange>().IsMidFlip)
        {
            UIToShow.GetComponentInChildren<ChangeCube>().SetCube(this.transform.parent.gameObject);
            UIToShow.SetActive(false);
            UIToShow.SetActive(true);
        }
           
    }

    public void SetUI(GameObject NewUI)
    {
        UIToShow = NewUI;
    }


}
