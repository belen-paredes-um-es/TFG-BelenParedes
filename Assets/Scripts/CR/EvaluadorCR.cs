using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class EvaluadorCR : MonoBehaviour
{
    [SerializeField]
    public int nMax=10;
    [HideInInspector]
    public int n=10;
    [HideInInspector]
    public float ten = 0.2f;

    CreadorPuntosCR creadorPuntosCR;
    MallasCerradas mallasCerradas;
    LineRenderer curva;
    Vector3 p0,p1,p2,p3;
    float q0,q1,q2,q3;
    int contador;
    float t;

    public void DibujarMalla(){
        mallasCerradas = gameObject.GetComponent<MallasCerradas>();
        mallasCerradas.DibujarMalla();
    }

    public void DibujarCurva(){

        creadorPuntosCR = gameObject.GetComponent<CreadorPuntosCR>();
        contador = 1;

        // si no tengo mínimo 4 puntos no puedo hacer nada (con sus asas)
        if (creadorPuntosCR.puntos.Count >= 4){

            // DibujarCurva(creadorPuntosCR.puntos);
            foreach (GameObject punto in creadorPuntosCR.puntos)
            {
                // Si no es el ultimo punto:
                if(creadorPuntosCR.puntos.IndexOf(punto) < (creadorPuntosCR.puntos.Count-3)){
                    DibujarCurva2(punto,contador);
                    contador++;
                } 
                // NO TIENE SENTIDO CERRAR LOS CATMUL ROM
            }
        }
        else { // no dibujo nignuna curva
            curva = creadorPuntosCR.GetComponent<LineRenderer>();
            curva.positionCount=1;
        }

    }

    private void DibujarCurva2(GameObject punto, int contador){

        creadorPuntosCR = gameObject.GetComponent<CreadorPuntosCR>();        

        curva = creadorPuntosCR.GetComponent<LineRenderer>();
        curva.positionCount=(n*contador)+1;

        // Obtengo los 4 puntos necesarios para dibujar la curva
        p0 = punto.transform.position;
        p1 = creadorPuntosCR.puntos[creadorPuntosCR.puntos.IndexOf(punto)+1].transform.position;
        p2 = creadorPuntosCR.puntos[creadorPuntosCR.puntos.IndexOf(punto)+2].transform.position; 
        p3 = creadorPuntosCR.puntos[creadorPuntosCR.puntos.IndexOf(punto)+3].transform.position;

        // valor del tiempo
        t = 0;

        // por cada punto de la curva de bezier
        for(int i=0; i <= n; i++){

            // obtengo un nuevo valor del tiempo
            t = ((float) i) / ((float) n);

            // Calculo el vector de cada punto de la curva de bezier
            Vector3 Q = (-ten*t + 2*ten*(Mathf.Pow(t, 2))-ten*(Mathf.Pow(t, 3))) * p0 + 
                        (1 + (ten-3)*(Mathf.Pow(t, 2)) + (2-ten)*(Mathf.Pow(t, 3))) * p1 +
                        (ten*t + (3-2*ten )*(Mathf.Pow(t, 2)) + (ten-2)*(Mathf.Pow(t, 3))) * p2 + 
                        (-ten*(Mathf.Pow(t, 2)) + ten*(Mathf.Pow(t, 3))) * p3;
                        
            // creo ese punto en el line renderer
            curva.SetPosition((contador-1)*n+i, Q);
            
        }

    }

}
