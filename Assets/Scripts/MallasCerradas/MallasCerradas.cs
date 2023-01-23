using System;
using UnityEngine;
using System.Collections;
 
public class MallasCerradas : MonoBehaviour {

    [HideInInspector]
    public GameObject go;
    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;
    [HideInInspector]
    public Mesh myMesh;
    [HideInInspector]
    public int ancho, alto, nPuntos;
    [HideInInspector]
    public float radio = 2.0f,k1=1f,k2=1f;
    Vector3 vector0,vector1,vector2,vector3;
    CreadorPuntosBezier creadorPuntosBezier;
    LineRenderer lineRenderer;
    float ang, an;
    Vector3 w,u,v;
    int indice, contador;
    [HideInInspector]
    public bool isPoligono = false;
   
    public void DibujarMalla () {

        creadorPuntosBezier = gameObject.GetComponent<CreadorPuntosBezier>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        ancho = lineRenderer.positionCount;


        // Crear una nueva malla
        myMesh = new Mesh();

        // Rellenar los vértices
        nPuntos = (alto+1)*(ancho); // hay un punto más QUE EL ALTO
        newVertices = new Vector3[nPuntos]; //Creo el array de vértices
        indice = 0;
        newUV = new Vector2[nPuntos]; //
        
        // Recorriendo el alto y ancho voy rellenando los vértices con la posición de cada punto
        for(int i = 0; i < ancho; i++){
            // Y añado los puntos que tenga por encima
            for(int j = 0; j <= alto; j++){
                newVertices[indice] = new Vector3(lineRenderer.GetPosition(i).x,lineRenderer.GetPosition(i).y+j,lineRenderer.GetPosition(i).z);
                newUV[indice] = new Vector2(k1*i/(ancho-1), k2*j/alto);//
                indice++;
            }   
        }

        // Rellenar los triángulos
        newTriangles = new int[alto*(ancho-1)*6];  // por cada cuadrado hay 6 referencias para dibujar los triangulos, NOS DAN UN PUNTO MÁS QUE DE CUADRADOS
        contador = 0;

        // Recorriendo el alto y ancho voy añadiendo las 6 referencias de los dos triángulos de cada cuadrado
        for(int i = 0; i < (ancho-1); i++){
            for(int j = 0; j < alto; j++){
                vector0 = new Vector3 (lineRenderer.GetPosition(i).x,lineRenderer.GetPosition(i).y+j,lineRenderer.GetPosition(i).z);
                vector1 = new Vector3 (lineRenderer.GetPosition(i+1).x,lineRenderer.GetPosition(i+1).y+j,lineRenderer.GetPosition(i+1).z);
                vector2 = new Vector3 (lineRenderer.GetPosition(i).x,lineRenderer.GetPosition(i).y+(j+1),lineRenderer.GetPosition(i).z);
                vector3 = new Vector3 (lineRenderer.GetPosition(i+1).x,lineRenderer.GetPosition(i+1).y+(j+1),lineRenderer.GetPosition(i+1).z);
                
                //  Lower left triangle.
                newTriangles[contador] = System.Array.IndexOf(newVertices, vector0);
                newTriangles[contador+1] = System.Array.IndexOf(newVertices, vector2);
                newTriangles[contador+2] = System.Array.IndexOf(newVertices, vector1);

                //  Upper right triangle.   
                newTriangles[contador+3] = System.Array.IndexOf(newVertices, vector2);
                newTriangles[contador+4] = System.Array.IndexOf(newVertices, vector3);
                newTriangles[contador+5] = System.Array.IndexOf(newVertices, vector1);
                
                contador = contador+6;
                
            }   
        }

        // Añadir puntos
        myMesh.vertices = newVertices;
        myMesh.uv = newUV;
        myMesh.triangles = newTriangles;

        // Obtener el objeto que tiene el script y asignarle la malla
        go = gameObject;
        go.GetComponent<MeshFilter>().mesh = myMesh;

        if (isPoligono) {
            DibujarPoligono();
        }

    }
    
    private void DibujarPoligono (){

        // Modificar las posiciones de los vértices
        indice = 0;
        ang = 2.0f * Mathf.PI / (alto);

        // Recorriendo el alto y ancho voy rellenando los vértices con la posición de cada punto
        for(int i = 0; i < ancho; i++){
            // Y añado los puntos que tenga por encima
            for(int j = 0; j <= alto; j++){
                // Calculo los vectores necesarios de cada subpunto
                if(i+1==ancho){ w = (lineRenderer.GetPosition(i)-lineRenderer.GetPosition(i-1)).normalized;} // caso del últiimo punto que coge el vector anterior
                else          { w = (lineRenderer.GetPosition(i+1)-lineRenderer.GetPosition(i)).normalized;}
                u = -Vector3.forward;
                v = Vector3.Cross(w, u);//.normalized;
                an = ang*j;
                // newVertices[indice] = new Vector3(lineRenderer.GetPosition(i).x,lineRenderer.GetPosition(i).y+j,lineRenderer.GetPosition(i).z);
                newVertices[indice] = lineRenderer.GetPosition(i) + (Mathf.Cos(an)*u + Mathf.Sin(an)*v) * radio; //* Mathf.Cos(3*an);//////////////////
                indice++;
            }   
        }

        // Actualizar -> Añadir puntos
        myMesh.vertices = newVertices;
        myMesh.uv = newUV;
        myMesh.triangles = newTriangles;

        myMesh.RecalculateNormals();

        // Obtener el objeto que tiene el script y asignarle la malla
        go = gameObject;
        go.GetComponent<MeshFilter>().mesh = myMesh;

    }
}