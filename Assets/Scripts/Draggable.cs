using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    public Transform parentToReturnTo;
    Vector3 startingScale;

    public void OnBeginDrag(PointerEventData eventData) {
        parentToReturnTo = transform.parent;
        startingScale = eventData.pointerDrag.transform.localScale;
        transform.SetParent(transform.parent.parent);
        eventData.pointerDrag.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parentToReturnTo);
        transform.localScale = startingScale;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
