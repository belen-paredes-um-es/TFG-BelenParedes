using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreadorPuntosBezier : MonoBehaviour
{
    public List<GameObject> puntos = new List<GameObject>();
    private int contador;
    [HideInInspector]
    public LineRenderer lineRenderer;
    private Vector3 posUltimoPunto;

    //Gizmos
    public bool enableGizmos = true;
    
    // Para que los puntos cambien su tam en el custom inspector
    [HideInInspector]
    public float tam = 0.1f;
    
    // Para dibujar la curva de bezier y catmull rom
    EvaluadorBezier evaluadorBezier;
    // EvaluadorCR evaluadorCR;

    // Update is called once per frame
    public void CrearPunto()
    {
        // Crear el objeto y renombrar
        GameObject sphere = new GameObject("punto" + contador);
        sphere.AddComponent<ActualizadorPuntosBezier>();
        

        //Crear los hijos del punto
        CrearHijosDelPunto(sphere, 1);
        CrearHijosDelPunto(sphere, 2);

        // Añadirlo como hijo
        sphere.transform.parent = this.transform;

        // Posición -> al lado del ultimo punto
        if(puntos.Count!=0){
            posUltimoPunto = puntos[puntos.Count-1].transform.position;
            sphere.transform.position = new Vector3(posUltimoPunto.x + 4, posUltimoPunto.y, posUltimoPunto.z);
        } else {
            posUltimoPunto = this.transform.position;
            sphere.transform.position = posUltimoPunto;
        }
        
        // Añadir el script del handle
        sphere.AddComponent<CustomHandles>();

        // Añadir a la lista de puntos de la curva
        puntos.Add(sphere);
        contador ++;

    }

    public void CrearHijosDelPunto(GameObject spherePadre, int numHijo){

        // Crear el primer hijo y renombrar
        GameObject sphere = new GameObject("punto" + contador + numHijo);

        // Añadir el script del handle
        sphere.AddComponent<CustomHandles>();

        // Añadirlo como hijo
        sphere.transform.parent = spherePadre.transform;

        //Darle una forma y posición distinta
        sphere.transform.localScale = new Vector3(tam, tam, tam);
        if(numHijo == 1){
            sphere.transform.position = new Vector3(0, 1, 0);
        }else{
            sphere.transform.position = new Vector3(0, -1, 0);
        }
        
    }

    // Solo borro el punto actual
    public void BorrarActual(GameObject puntoActual){

        //Elimino el elemento de la lista
        puntos.Remove(puntoActual);

        // actualizo curvas
        DibujarCurva();
        DibujarMalla();

        //Destruyo el objeto
        DestroyImmediate(puntoActual);        
    }

    // Borro el punto actual y todos los que quedan hasta el final
    public void BorrarFinal(GameObject puntoActual){

        //Obtengo el indice del punto actual
        int indice = puntos.IndexOf(puntoActual);

        //Me guardo el valor inicial de puntos que había antes de borrar
        int numInicialPuntos = puntos.Count;

        // por cada punto desde el actual hasta el final
        for(int i = indice; i < numInicialPuntos; i++){
            BorrarActual(puntos[indice]);
        }

        // actualizo curvas
        DibujarCurva();
        DibujarMalla();
    }

    public void DibujarCurva(){

        // Para que al mover las asas se actualicen en el momento 
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        evaluadorBezier = gameObject.GetComponent<EvaluadorBezier>();
        evaluadorBezier.DibujarCurva();

    }

    public void DibujarMalla(){

        // Para que al mover las asas se actualicen en el momento 
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        evaluadorBezier = gameObject.GetComponent<EvaluadorBezier>();
        evaluadorBezier.DibujarMalla();
    }


    void OnDrawGizmos() // Aparece continuamente
    {
        if (enableGizmos){
            foreach (GameObject punto in puntos)
            {
                if (punto != null){
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(punto.transform.position, tam/2);
                    
                }
                
            }
        }

        
    }
    
}