using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    private float Zoom;
    private float ZoomMultiplier = 1f;
    private float minZoom = 1f;
    private float maxZoom = 20f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;

    [SerializeField]
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        Zoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DoCameraMovement();
    }
    private void Update()
    {
        DoCameraZoom();
    }

    void DoCameraZoom()
    {
        float Scroll = Input.mouseScrollDelta.y;
        Zoom -= Scroll * ZoomMultiplier;
        Zoom = Mathf.Clamp(Zoom, minZoom, maxZoom);

        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, Zoom, ref velocity, smoothTime);
    }
    void DoCameraMovement()
    {
        Vector3 CameraPosition = this.transform.position;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * 0.2f;

        transform.Translate(input);
    }



}
