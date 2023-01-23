using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizadorPuntosCR : MonoBehaviour
{
    CreadorPuntosCR creadorPuntosCR;
    MallasCerradas malla;

    public void DibujarCurva(){
        creadorPuntosCR = gameObject.transform.parent.gameObject.GetComponent<CreadorPuntosCR>();
        creadorPuntosCR.DibujarCurva();
        creadorPuntosCR.DibujarMalla();
    }

    public void BorrarActual(){
        creadorPuntosCR = gameObject.transform.parent.gameObject.GetComponent<CreadorPuntosCR>();
        creadorPuntosCR.BorrarActual(gameObject);
    }

    public void BorrarFinal(){
        creadorPuntosCR = gameObject.transform.parent.gameObject.GetComponent<CreadorPuntosCR>();
        creadorPuntosCR.BorrarFinal(gameObject);
    }
    
}