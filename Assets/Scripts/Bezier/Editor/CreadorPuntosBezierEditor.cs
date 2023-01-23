using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreadorPuntosBezier))]
public class CreadorPuntosBezierEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        CreadorPuntosBezier creadorPuntosBezier = (CreadorPuntosBezier)target;

        //ACTUALIZAR TAMAÑOS
        creadorPuntosBezier.tam = EditorGUILayout.Slider("Tamaño", creadorPuntosBezier.tam, .1f, 2f);
        creadorPuntosBezier.GetComponent<LineRenderer>().startWidth = creadorPuntosBezier.tam;
        creadorPuntosBezier.GetComponent<LineRenderer>().endWidth = creadorPuntosBezier.tam;

        foreach (GameObject punto in creadorPuntosBezier.puntos)
        {
            if(punto!=null)
            {
                punto.transform.localScale = Vector3.one * creadorPuntosBezier.tam;
                punto.GetComponent<CustomHandles>().size = creadorPuntosBezier.tam;
            }
        }
        
        creadorPuntosBezier.DibujarCurva();
        creadorPuntosBezier.DibujarMalla();

        if(GUILayout.Button("Crear Punto")){
            creadorPuntosBezier.CrearPunto();
            creadorPuntosBezier.DibujarCurva();
            creadorPuntosBezier.DibujarMalla();
        }

    }

}