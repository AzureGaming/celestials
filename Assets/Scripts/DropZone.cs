using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("On drop to " + gameObject.name);
        Draggable2 d = eventData.pointerDrag.GetComponent<Draggable2>();
        if (d != null) {
            d.parentToReturnTo = transform;
        }
    }

}
