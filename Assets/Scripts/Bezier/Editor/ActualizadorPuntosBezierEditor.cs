using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActualizadorPuntosBezier))]
public class ActualizadorPuntosBezierEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        ActualizadorPuntosBezier actualizadorPuntosBezier = (ActualizadorPuntosBezier)target;
      
        actualizadorPuntosBezier.DibujarCurva();

        if(GUILayout.Button("Borrar actual")){
            actualizadorPuntosBezier.BorrarActual();
        }

        if(GUILayout.Button("Borrar del actual hasta el final")){
            actualizadorPuntosBezier.BorrarFinal();
        }

    }

}