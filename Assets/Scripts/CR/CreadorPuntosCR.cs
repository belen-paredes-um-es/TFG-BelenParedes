using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreadorPuntosCR : MonoBehaviour
{
    public List<GameObject> puntos = new List<GameObject>();
    private int contador;
    [HideInInspector]
    public LineRenderer lineRenderer;
    private Vector3 posUltimoPunto;

    //Gizmos
    public bool enableGizmos = true;
    
    // Para que los puntos cambie su tam en el custom inspector
    [HideInInspector]
    public float tam = 0.1f;
    
    // Para dibujar la curva de bezier y catmull rom
    EvaluadorCR evaluadorCR;
    // EvaluadorCR evaluadorCR;

    // Update is called once per frame
    public void CrearPunto()
    {
        // Crear el objeto y renombrar
        GameObject sphere = new GameObject("punto" + contador);
        sphere.AddComponent<ActualizadorPuntosCR>();
        
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
        sphere.AddComponent<CustomHandlesSinAsas>();

        // Añadir a la lista de puntos de la curva
        puntos.Add(sphere);
        contador ++;

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
        lineRenderer.SetPosition(0,  gameObject.transform.position);

        evaluadorCR = gameObject.GetComponent<EvaluadorCR>();
        evaluadorCR.DibujarCurva();
    }

    public void DibujarMalla(){

        // Para que al mover las asas se actualicen en el momento 
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        evaluadorCR = gameObject.GetComponent<EvaluadorCR>();
        evaluadorCR.DibujarMalla();
    }

    void OnDrawGizmos() // Aparece continuamente
    {
        if (enableGizmos){
            foreach (GameObject punto in puntos)
            {
                if(punto == puntos[0] || punto == puntos[puntos.Count-1]){
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireSphere(punto.transform.position, tam/2);

                } 
                else if (punto != null){
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(punto.transform.position, tam/2);
                }       
            }
        }
    }
    
}