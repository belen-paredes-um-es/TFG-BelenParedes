using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class EvaluadorBezier : MonoBehaviour
{
    // Cerrado o no
    public bool cerrado = false;
    //numero de puntos de la curva de bezier
    [SerializeField]
    public int nMax=10;
    [HideInInspector]
    public int n=10;

    CreadorPuntosBezier creadorPuntosBezier;
    MallasCerradas mallasCerradas;
    CustomHandles customHandlesActual;
    CustomHandles customHandlesSiguiente;
    LineRenderer curva;
    Vector3 p0,p1,p2,p3;
    int contador,iteraciones, newN;
    float t;
    
    [HideInInspector]
    public int algoritmo = 0; //0:Directo, 1:Subdivisión;
    [HideInInspector]
    public string[] algoritmos = new string[2]{"Directo","Subdivisón"};

    //lista de puntos Subdivisón
    private List<Vector3> lista = new List<Vector3>();

    public void DibujarMalla(){
        mallasCerradas = gameObject.GetComponent<MallasCerradas>();
        mallasCerradas.DibujarMalla();
    }

    public void DibujarCurva(){

        creadorPuntosBezier = gameObject.GetComponent<CreadorPuntosBezier>();
        contador = 1;
        lista.Clear();

        // si no tengo mínimo 2 puntos no puedo hacer nada (con sus asas)
        if (creadorPuntosBezier.puntos.Count >= 2){
            foreach (GameObject punto in creadorPuntosBezier.puntos)
            {
                // si no es el ultimo punto:
                if(creadorPuntosBezier.puntos.IndexOf(punto) < (creadorPuntosBezier.puntos.Count-1)){
                    DibujarCurva2(punto, creadorPuntosBezier.puntos[creadorPuntosBezier.puntos.IndexOf(punto)+1], contador);
                    contador++;

                } 
                // Si soy el ultimo elemento
                else {
                    //Si quiero cerrar la curva
                    if(cerrado){
                        // llamo a la función con el ultimo y el primero
                        DibujarCurva2(punto, creadorPuntosBezier.puntos[0], contador);
                    }
                }
            }
        }
        else { // no dibujo nignuna curva
            curva = creadorPuntosBezier.GetComponent<LineRenderer>();
            curva.positionCount=1;
        }

    }

    // Para elegir el algoritmo
    private void DibujarCurva2(GameObject actual, GameObject siguiente, int contador){
        if (algoritmo==0){
            DibujarCurvaCubica(actual, siguiente, contador);
        } 
        if (algoritmo==1) {
            DibujarCurvaDeCasteljau(actual, siguiente, contador);
        }
    }

    private void DibujarCurvaCubica(GameObject actual, GameObject siguiente, int contador){

        creadorPuntosBezier = gameObject.GetComponent<CreadorPuntosBezier>();

        customHandlesActual = actual.GetComponent<CustomHandles>();
        customHandlesSiguiente = siguiente.GetComponent<CustomHandles>();

        // actual
        p0 = customHandlesActual.transform.position; 
        // asa derecha de actual
        p1 = customHandlesActual.transform.position + new Vector3 (0,1,0) * customHandlesActual.offsetArriba + customHandlesActual.desplazamientoArriba; 
        // asa izquierda de siguiente
        p2 = customHandlesSiguiente.transform.position + new Vector3 (0,-1,0) * customHandlesSiguiente.offsetAbajo + customHandlesSiguiente.desplazamientoAbajo; 
        // siguiente
        p3 = customHandlesSiguiente.transform.position; 
        

        curva = creadorPuntosBezier.GetComponent<LineRenderer>();
        curva.positionCount=(n*contador)+1;

        // valor del tiempo
        t = 0;
        // por cada punto de la curva de bezier
        for(int i=0; i <= n; i++){
            // obtengo un nuevo valor del tiempo
            t = ((float) i) / ((float) n);
            // Calculo el vector de cada punto de la curva de bezier
            Vector3 Q = (Mathf.Pow((1.0f - t), 3)) * p0
                        + 3 * (Mathf.Pow((1.0f - t), 2)) * t * p1 
                        + 3 * (1.0f - t) * (Mathf.Pow(t, 2)) * p2
                        + (Mathf.Pow(t, 3)) * p3;
            // creo ese punto en el line renderer
            curva.SetPosition((contador-1)*n+i, Q);
        }
    }

    private void DibujarCurvaDeCasteljau(GameObject actual, GameObject siguiente, int contador){
        
        // Vacío la lista ya que con cada iteración voy a crear una serie de puntos distinta
        lista.Clear();

        creadorPuntosBezier = gameObject.GetComponent<CreadorPuntosBezier>();
        customHandlesActual = actual.GetComponent<CustomHandles>();
        customHandlesSiguiente = siguiente.GetComponent<CustomHandles>();

        // Actual
        p0 = customHandlesActual.transform.position; 
        // Asa derecha de actual
        p1 = customHandlesActual.transform.position + new Vector3 (0,1,0) * customHandlesActual.offsetArriba + customHandlesActual.desplazamientoArriba; 
        // Asa izquierda de siguiente
        p2 = customHandlesSiguiente.transform.position + new Vector3 (0,-1,0) * customHandlesSiguiente.offsetAbajo + customHandlesSiguiente.desplazamientoAbajo; 
        // Siguiente
        p3 = customHandlesSiguiente.transform.position; 
    
        curva = creadorPuntosBezier.GetComponent<LineRenderer>();
        
        // ANTIGUA IDEA
        // curva.positionCount=((int)(Mathf.Pow(2f,n))*contador)+1;
        // aqui n son las iteraciones, por lo que el tamaño de la curva será 2^n+1
        // 1 iteracion -> 2
        // 2 iteraciones -> 5
        // 3 iteraciones -> 9

        // NUEVA IDEA 
        // para n = 4 quiero saber 2^x = 4 -> 2
        // para n = 5 quiero saber 2^x = 4 -> 2,algo floor = 2
        // para n = 8 quiero saber 2^x = 8 -> 3
        // Hago las iteraciones posibles para llegar al numero que me piden sin pasarme
        iteraciones = (int)(Mathf.Floor(Mathf.Log(n, 2))); //Ceil
        newN = (int)(Mathf.Pow(2, iteraciones));
        curva.positionCount=(newN*contador)+1;

        int it = 0;

        // Primera posición
        lista.Add(p0);

        // Llamo a la función iterativa
        CasteljauIterativo(p0,p1,p2,p3,it+1);

        // Ultima posición
        lista.Add(p3);

        // IDEA ANTIGUA
        // for(int j=0; j < ((int)(Mathf.Pow(2f,n)))+1; j++){
        //     curva.SetPosition((contador-1)*n+j, lista[j]);
        // }

        // NUEVA IDEA
        // Una vez terminada la lista, añado cada punto al lineRendere
        for(int i=0; i <= newN; i++){
            curva.SetPosition((contador-1)*newN+i, lista[i]);
        }
    }

    private void CasteljauIterativo(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, int it){
        if(it <= iteraciones){

            Vector3 m0,m1,m2,q0,q1,B;

            m0 = (p0+p1)/2;
            m1 = (p1+p2)/2;
            m2 = (p2+p3)/2;

            q0 = (m0+m1)/2;
            q1 = (m1+m2)/2;

            B = (q0+q1)/2;

            CasteljauIterativo(p0,m0,q0,B, it+1);
            lista.Add(B);
            CasteljauIterativo(B,q1,m2,p3, it+1);

        }
    }


}
