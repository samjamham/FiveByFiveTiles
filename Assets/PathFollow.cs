using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_Points;
    [SerializeField]
    private float moveSpeed = 0.000001f;

    private int PointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = m_Points[PointIndex].transform.position;
    }
    private void Update()
    {

    }
    // Update is called once per frame
    [ExecuteInEditMode]
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, m_Points[PointIndex].position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, m_Points[PointIndex].position) <= 0.1)            
        {
            PointIndex++;
            if (PointIndex >= m_Points.Length)
            {
                PointIndex = 0;
            }

            transform.LookAt(m_Points[PointIndex].position);
        }
    }
}
