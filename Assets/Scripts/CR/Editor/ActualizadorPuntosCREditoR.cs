using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActualizadorPuntosCR))]
public class ActualizadorPuntosCREditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        ActualizadorPuntosCR actualizadorPuntosCR = (ActualizadorPuntosCR)target;
      
        actualizadorPuntosCR.DibujarCurva();

        if(GUILayout.Button("Borrar actual")){
            actualizadorPuntosCR.BorrarActual();
        }

        if(GUILayout.Button("Borrar del actual hasta el final")){
            actualizadorPuntosCR.BorrarFinal();
        }

    }

}