using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EvaluadorBezier))]
public class EvaluadorBezierEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        EvaluadorBezier evaluadorBezier  = (EvaluadorBezier)target;

        // Modificar el numero de puntos para la curva de bezier
        evaluadorBezier.n = EditorGUILayout.IntSlider("Nº puntos", evaluadorBezier.n, 1, evaluadorBezier.nMax);

        // Algoritmo de las curvas de bezier
        evaluadorBezier.algoritmo = EditorGUILayout.Popup("Algoritmo", evaluadorBezier.algoritmo, evaluadorBezier.algoritmos);

        // everytime
        evaluadorBezier.DibujarCurva();

    }
}
