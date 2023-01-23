using System.Drawing;
using System.Collections;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CustomHandlesSinAsas))]
public class CustomHandlesSinAsasEditor : Editor
{

    int nearestHandle = -1;
    Vector2 previusMousePosition;
    
    // Para mover los objetos en la pantalla
    private void OnSceneGUI()
    {
        int hoverIndex = -1; // 11: centro, 12: arriba, 13: abajo
        CustomHandlesSinAsas CustomHandlesSinAsas = target as CustomHandlesSinAsas;
        
        // Para que aparezcan en la pantalla -> no quiero que aparezca -> AHORA SÍ
        if(Event.current.type == EventType.Repaint)
        {
            hoverIndex = HandleUtility.nearestControl;
            //Centro
            Handles.color = hoverIndex == 11 ? Color.magenta : Color.white;
            CreateHandleCap(11, 
                            CustomHandlesSinAsas.transform.position, // siempre en el centro
                            CustomHandlesSinAsas.transform.rotation * Quaternion.LookRotation(Vector3.forward),
                            CustomHandlesSinAsas.size, 
                            EventType.Repaint);
        }

        // Para que se pinte cuando pase por encima
        if(Event.current.type == EventType.Layout)
        {
            // Centro
            CreateHandleCap(11, 
                            CustomHandlesSinAsas.transform.position, //Siempre en el centro
                            CustomHandlesSinAsas.transform.rotation * Quaternion.LookRotation(Vector3.forward),
                            CustomHandlesSinAsas.size, 
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
                                CustomHandlesSinAsas.transform.position, CustomHandlesSinAsas.transform.forward);
                float moveR = HandleUtility.CalcLineTranslation(previusMousePosition, Event.current.mousePosition,
                                CustomHandlesSinAsas.transform.position, CustomHandlesSinAsas.transform.right);
                float moveU = HandleUtility.CalcLineTranslation(previusMousePosition, Event.current.mousePosition,
                                CustomHandlesSinAsas.transform.position, CustomHandlesSinAsas.transform.up);

                // Actualizo la posición del punto
                CustomHandlesSinAsas.transform.position += moveF * CustomHandlesSinAsas.transform.forward;
                CustomHandlesSinAsas.transform.position += moveR * CustomHandlesSinAsas.transform.right;
                CustomHandlesSinAsas.transform.position += moveU * CustomHandlesSinAsas.transform.up;
            }
            
            previusMousePosition = Event.current.mousePosition;
        }
    }

    void CreateHandleCap(int id, Vector3 position, Quaternion rotation, float size, EventType eventType)
    {
        Handles.SphereHandleCap(id, position, rotation, size, eventType);
    }
   
}
