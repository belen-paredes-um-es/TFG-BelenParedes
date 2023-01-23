using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizadorPuntosBezier : MonoBehaviour
{
    CreadorPuntosBezier creadorPuntosBezier;

    public void DibujarCurva(){
        creadorPuntosBezier = gameObject.transform.parent.gameObject.GetComponent<CreadorPuntosBezier>();
        creadorPuntosBezier.DibujarCurva();
        creadorPuntosBezier.DibujarMalla();
    }

    public void BorrarActual(){
        creadorPuntosBezier = gameObject.transform.parent.gameObject.GetComponent<CreadorPuntosBezier>();
        creadorPuntosBezier.BorrarActual(gameObject);
    }

    public void BorrarFinal(){
        creadorPuntosBezier = gameObject.transform.parent.gameObject.GetComponent<CreadorPuntosBezier>();
        creadorPuntosBezier.BorrarFinal(gameObject);
    }
    
}