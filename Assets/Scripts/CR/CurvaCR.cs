using UnityEditor;
using UnityEngine;


public class CurvaCR : EditorWindow
{
    [MenuItem("GameObject/3D Object/Curva Catmull-Rom")]
    static void Curva()
    {
        GameObject gameObject = new GameObject("CurvaCR");
        gameObject.AddComponent<CreadorPuntosCR>();
        
        // Añadimos el line renderer y el script evaluador
        gameObject.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = !lineRenderer.enabled;
        gameObject.AddComponent<EvaluadorCR>();
        
        // Añadimos el sccript de las mallas
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<MallasCerradas>();

        // Empezar con dos puntos y dos de control
        CreadorPuntosCR creadorPuntosCR = gameObject.GetComponent<CreadorPuntosCR>();
        creadorPuntosCR.CrearPunto();
        creadorPuntosCR.CrearPunto();
        creadorPuntosCR.CrearPunto();
        creadorPuntosCR.CrearPunto();
        creadorPuntosCR.DibujarCurva();
        creadorPuntosCR.DibujarMalla();

    }

    

    
}
