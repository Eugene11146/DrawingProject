using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragTracking : MonoBehaviour, IDragHandler
{
   
    public static Vector2 dragPosition;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        dragPosition = transform.position;

    }

}
