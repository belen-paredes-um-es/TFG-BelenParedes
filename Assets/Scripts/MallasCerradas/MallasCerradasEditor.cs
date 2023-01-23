using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MallasCerradas))]
public class MallasCerradasEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        MallasCerradas mallas  = (MallasCerradas)target;

        mallas.alto = EditorGUILayout.IntSlider("Alto", mallas.alto, 1, 30);
        mallas.radio = EditorGUILayout.Slider("Radio", mallas.radio, 0.1f, 2.0f);
        mallas.k1 = EditorGUILayout.Slider("K1", mallas.k1, 0.1f, 10.0f);
        mallas.k2 = EditorGUILayout.Slider("K2", mallas.k2, 0.1f, 10.0f);

        if(GUILayout.Button("Dibujar malla plana")){
            mallas.isPoligono = false;
            mallas.DibujarMalla();
        }
        if(GUILayout.Button("Dibujar malla poligonal")){
            mallas.isPoligono = true;
            mallas.DibujarMalla();
        }
    }
}