using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]

public class CustomHandles : MonoBehaviour
{
    [HideInInspector]
    public float size = 1f;
    [HideInInspector]
    public float offsetArriba = 1f; // quiero que las asas estén a cierta distancia
    [HideInInspector]
    public float offsetAbajo = 1f; // quiero que las asas estén a cierta distancia
    [HideInInspector]
    public Vector3 desplazamientoArriba = Vector3.zero;
    [HideInInspector]
    public Vector3 desplazamientoAbajo = Vector3.zero;
    

}