using UnityEditor;
using UnityEngine;


public class CurvaBezier : EditorWindow
{
    [MenuItem("GameObject/3D Object/Curva Bezier")]
    static void Curva()
    {
        GameObject gameObject = new GameObject("CurvaBezier");
        // Añadimos el script crador de puntos
        gameObject.AddComponent<CreadorPuntosBezier>();
        // Añadimos el line renderer
        gameObject.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = !lineRenderer.enabled;
        // Añadimos el script evaluador
        gameObject.AddComponent<EvaluadorBezier>();

        // Añadimos el sccript de las mallas
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<MallasCerradas>();

        // Empezar con dos puntos
        CreadorPuntosBezier creadorPuntosBezier = gameObject.GetComponent<CreadorPuntosBezier>();
        creadorPuntosBezier.CrearPunto();
        creadorPuntosBezier.CrearPunto();
        creadorPuntosBezier.DibujarCurva();
        creadorPuntosBezier.DibujarMalla();
    }

    

    
}
