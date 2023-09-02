using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAndChange : MonoBehaviour
{
    [SerializeField]
    private GameObject UIToShow;


    bool ToDestroy = false;
    GameObject CurrentMesh;
    public bool IsMidFlip { private set; get; } = false;

    private float DesiredAngle = 0;
    float r;

    public void NewMeshSelected(GameObject InMesh)
    {
        if(InMesh != CurrentMesh && !IsMidFlip)
        {
            CurrentMesh = InMesh;
            GameObject NewMesh = Instantiate(InMesh, this.transform.position, this.transform.rotation);
            NewMesh.transform.localRotation = Quaternion.Euler(0, 0, 180);
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
}
