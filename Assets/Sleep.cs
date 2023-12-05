using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup AlphaController;

    [SerializeField]
    private ChangeCube m_ChangeCube;

    private float Alpha = 0;

    private void Start()
    {
        m_ChangeCube.StartUp();
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Alpha = 0;
    }
    private void Update()
    {
        Alpha += Time.deltaTime * 2;
        AlphaController.alpha = Alpha;
    }
}
