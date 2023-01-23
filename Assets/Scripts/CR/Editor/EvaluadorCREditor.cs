using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EvaluadorCR))]
public class EvaluadorCREditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        EvaluadorCR evaluadorCR  = (EvaluadorCR)target;

        // Modificar el numero de puntos para la curva de CR
        evaluadorCR.n = EditorGUILayout.IntSlider("Nº puntos", evaluadorCR.n, 1, evaluadorCR.nMax);

        // Modificar el punto de tensión
        evaluadorCR.ten = EditorGUILayout.Slider("Tensión", evaluadorCR.ten, 0, 1);

        // everytime
        evaluadorCR.DibujarCurva();

    }
}
