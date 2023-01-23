using System.Drawing;
using System.Collections;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CustomHandles))]
public class CustomHandlesEditor : Editor
{

    int nearestHandle = -1;
    Vector2 previusMousePosition;

    
    // Para mover los objetos en la pantalla
    private void OnSceneGUI()
    {
        int hoverIndex = -1; // 11: centro, 12: arriba, 13: abajo
        CustomHandles customHandles = target as CustomHandles;
        
        // Para que aparezcan en la pantalla -> no quiero que aparezca -> AHORA SÍ
        if(Event.current.type == EventType.Repaint)
        {
            hoverIndex = HandleUtility.nearestControl;
            //Centro
            Handles.color = hoverIndex == 11 ? Color.magenta : Color.white;
            CreateHandleCap(11, 
                            customHandles.transform.position, // siempre en el centro
                            customHandles.transform.rotation * Quaternion.LookRotation(Vector3.forward),
                            customHandles.size, 
                            EventType.Repaint);
            //Arriba (derecho)
            Handles.color = hoverIndex == 12 ? Color.magenta : Color.blue;
            CreateHandleCap(12,   
                            customHandles.transform.position + new Vector3 (0,1,0) * customHandles.offsetArriba + customHandles.desplazamientoArriba,
                            customHandles.transform.rotation * Quaternion.LookRotation(new Vector3 (0,1,0)),
                            customHandles.size/2, 
                            EventType.Repaint);
            //Abajo (izquierdo)
            Handles.color = hoverIndex == 13 ? Color.magenta : Color.red;
            CreateHandleCap(13, 
                            customHandles.transform.position + new Vector3 (0,-1,0) * customHandles.offsetAbajo + customHandles.desplazamientoAbajo,
                            customHandles.transform.rotation * Quaternion.LookRotation(new Vector3 (0,-1,0)),
                            customHandles.size/2, 
                            EventType.Repaint);


        }

        // Para que se pinte cuando pase por encima
        if(Event.current.type == EventType.Layout)
        {
            // Centro
            CreateHandleCap(11, 
                            customHandles.transform.position, //Siempre en el centro
                            customHandles.transform.rotation * Quaternion.LookRotation(Vector3.forward),
                            customHandles.size, 
                            EventType.Layout);
            // Arriba (derecho)
            CreateHandleCap(12, 
                            customHandles.transform.position + new Vector3 (0,1,0) * customHandles.offsetArriba + customHandles.desplazamientoArriba,
                            customHandles.transform.rotation * Quaternion.LookRotation(new Vector3 (0,1,0)),
                            customHandles.size/2, 
                            EventType.Layout);
            // Abajo (izquierdo)
            CreateHandleCap(13,   
                            customHandles.transform.position + new Vector3 (0,-1,0) * customHandles.offsetAbajo + customHandles.desplazamientoAbajo,
                            customHandles.transform.rotation * Quaternion.LookRotation(new Vector3 (0,-1,0)),
                            customHandles.size/2, 
                            EventType.Layout);

        }

        // Cuando se pincha
        if(Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            nearestHandle = HandleUtility.nearestControl;
            previusMousePosition = Event.current.mousePosition;

        }
        
        // Cuando deja de pincharse
        if(Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {
            nearestHandle = -1;
            previusMousePosition = Vector3.zero;
        }
        
        // Para moverse
        if(Event.current.type == EventType.MouseDrag && Event.current.button == 0)
        {
            // EL CENTRO MUEVE LA POSICIÓN DEL PUNTO
            if(nearestHandle == 11)
            {
                // Movimiento hacia los 3 lados
                float moveF = HandleUtility.CalcLineTranslation(previusMousePosition, Event.current.mousePosition,
                                customHandles.transform.position, customHandles.transform.forward);
                float moveR = HandleUtility.CalcLineTranslation(previusMousePosition, Event.current.mousePosition,
                                customHandles.transform.position, customHandles.transform.right);
                float moveU = HandleUtility.CalcLineTranslation(previusMousePosition, Event.current.mousePosition,
                                customHandles.transform.position, customHandles.transform.up);

                // Actualizo la posición del punto
                customHandles.transform.position += moveF * customHandles.transform.forward;
                customHandles.transform.position += moveR * customHandles.transform.right;
                customHandles.transform.position += moveU * customHandles.transform.up;
            }


            if(nearestHandle == 12)
            {
                // Movimiento hacia los 3 lados
                float moveF = HandleUtility.CalcLineTranslation(previusMousePosition, Event.current.mousePosition,
                                customHandles.transform.position, customHandles.transform.forward);
                float moveR = HandleUtility.CalcLineTranslation(previusMousePosition, Event.current.mousePosition,
                                customHandles.transform.position, customHandles.transform.right);
                float moveU = HandleUtility.CalcLineTranslation(previusMousePosition, Event.current.mousePosition,
                                customHandles.transform.position, customHandles.transform.up);

                // Actualizo la posición del punto azul
                customHandles.desplazamientoArriba += moveF * customHandles.transform.forward;
                customHandles.desplazamientoArriba += moveR * customHandles.transform.right;
                customHandles.offsetArriba += moveU;

                // Hago el simétrico para el punto rojo
                customHandles.desplazamientoAbajo += moveF * -customHandles.transform.forward;
                customHandles.desplazamientoAbajo += moveR * -customHandles.transform.right;
                customHandles.offsetAbajo += moveU;
                
            }


            if(nearestHandle == 13)
            {
                // Movimiento hacia los 3 lados
                float moveF = HandleUtility.CalcLineTranslation(previusMousePosition, Event.current.mousePosition,
                                customHandles.transform.position, customHandles.transform.forward);
                float moveR = HandleUtility.CalcLineTranslation(previusMousePosition, Event.current.mousePosition,
                                customHandles.transform.position, customHandles.transform.right);
                float moveU = HandleUtility.CalcLineTranslation(previusMousePosition, Event.current.mousePosition,
                                customHandles.transform.position, customHandles.transform.up);

                // Actualizo la posición del punto rojo
                customHandles.desplazamientoAbajo += moveF * customHandles.transform.forward;
                customHandles.desplazamientoAbajo += moveR * customHandles.transform.right;
                customHandles.offsetAbajo -= moveU;

                // Hago el simétrico para el punto azul
                customHandles.desplazamientoArriba += moveF * -customHandles.transform.forward;
                customHandles.desplazamientoArriba += moveR * -customHandles.transform.right;
                customHandles.offsetArriba -= moveU;
                
            }

            previusMousePosition = Event.current.mousePosition;
        }
    }

    void CreateHandleCap(int id, Vector3 position, Quaternion rotation, float size, EventType eventType)
    {
        Handles.SphereHandleCap(id, position, rotation, size, eventType);
    }
   
}
