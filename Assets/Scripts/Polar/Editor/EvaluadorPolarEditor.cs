using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EvaluadorPolar))]
public class EvaluadorPolarEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        EvaluadorPolar eval = (EvaluadorPolar)target;

        // Mensaje de ayuda
        EditorGUILayout.HelpBox("Escribe a continuación una ecuación en polar. El ángulo debe estar en función de 't'." + "\r\n" +
                                "Puedes utilizar las variables 'a' y 'b' para modificar la función en tiempo real.",
                                MessageType.Info);
        // Lo que recibo del cuadro de texto lo almaceno en texto oficial
        eval.textoOf = EditorGUILayout.TextField("Ecuación", eval.textoOf); 

        // Cuando se pulse el botón "Dibujar curva" se registrará la ecuación escrita, sino, se irá almacenando ecuaciones a medio que dan error
        if(GUILayout.Button("Dibujar curva")){
            eval.nuevaEcuacion();
        }
    
        // Si el texto está vacío, no dibujo ninguna curva
        if(eval.texto==""){
            eval.eliminarCurva();
        } 
        // Sino, actualizo la ultima curva escrita
        else {
            eval.dibujarCurva();
        }
        
        // Modifico con un slider los valores de las posibles variables a y b, del numero de vueltas y el de puntos
        eval.var1 = EditorGUILayout.IntSlider("a", eval.var1, 1, 10);
        eval.var2 = EditorGUILayout.IntSlider("b", eval.var2, 1, 10);

        // Modificar el tamaño de la línea
        eval.tam = EditorGUILayout.Slider("Tamaño", eval.tam, .1f, 2f);
        eval.GetComponent<LineRenderer>().startWidth = eval.tam;
        eval.GetComponent<LineRenderer>().endWidth = eval.tam;

        // Numero de vueltas y número de puntos
        eval.numVueltas = EditorGUILayout.IntSlider("Nº vueltas", eval.numVueltas, 1, 12);
        eval.numPuntos = EditorGUILayout.IntSlider("Nº puntos", eval.numPuntos, 1, 101);

        

        
    }
}
