using UnityEditor;
using UnityEngine;


public class CurvaPolar : EditorWindow
{
    [MenuItem("GameObject/3D Object/Curva Polar")]
    static void Curva()
    {
        GameObject gameObject = new GameObject("CurvaPolar");
        gameObject.AddComponent<EvaluadorPolar>();
        gameObject.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.enabled = !lineRenderer.enabled;
        
        // Añadimos el sccript de las mallas
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<MallasCerradas>();
    }

    
}