using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreadorPuntosCR))]
public class CreadorPuntosCREditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        CreadorPuntosCR creadorPuntosCR = (CreadorPuntosCR)target;

        // Modificar el tamaño de los puntos y la curva
        creadorPuntosCR.tam = EditorGUILayout.Slider("Tamaño", creadorPuntosCR.tam, .1f, 2f);
        creadorPuntosCR.GetComponent<LineRenderer>().startWidth = creadorPuntosCR.tam;
        creadorPuntosCR.GetComponent<LineRenderer>().endWidth = creadorPuntosCR.tam;

        foreach (GameObject punto in creadorPuntosCR.puntos)
        {
            if(punto!=null)
            {
                punto.transform.localScale = Vector3.one * creadorPuntosCR.tam;
                punto.GetComponent<CustomHandlesSinAsas>().size = creadorPuntosCR.tam;
            }
        }
        
        creadorPuntosCR.DibujarCurva();
        creadorPuntosCR.DibujarMalla();

        if(GUILayout.Button("Crear Punto")){
            creadorPuntosCR.CrearPunto();
            creadorPuntosCR.DibujarCurva();
            creadorPuntosCR.DibujarMalla();
        }

    }

}