using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler {
    Transform parentToReturnTo;
    Vector3 startingScale;

    private void Awake() {
        parentToReturnTo = FindObjectOfType<Hand>().transform;    
    }

    public void OnBeginDrag(PointerEventData eventData) {
        startingScale = eventData.pointerDrag.transform.localScale;
        transform.SetParent(transform.parent.parent);
        //eventData.pointerDrag.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        Debug.Log("Start");
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
    }

    //public void OnEndDrag(PointerEventData eventData) {
    //    Debug.Log(eventData.pointerEnter);
    //    //transform.SetParent(parentToReturnTo, false);
    //    transform.localScale = startingScale;
    //    //GetComponent<CanvasGroup>().blocksRaycasts = true;
    //}
}
