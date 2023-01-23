using UnityEditor;
using UnityEngine;


public class CurvaParametricas : EditorWindow
{
    [MenuItem("GameObject/3D Object/Curva Parametricas")]
    static void Curva()
    {
        GameObject gameObject = new GameObject("CurvaParametricas");
        gameObject.AddComponent<EvaluadorParametricas>();
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