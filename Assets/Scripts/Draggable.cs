using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    Transform parentToReturnTo;
    Vector3 startingScale;
    Hand hand;

    private void Awake() {
        parentToReturnTo = FindObjectOfType<Hand>().transform;
        hand = FindObjectOfType<Hand>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        startingScale = eventData.pointerDrag.transform.localScale;
        transform.SetParent(transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        eventData.pointerDrag.GetComponent<UIHoverSize>().enabled = false;
        foreach (Card card in hand.GetCards()) {
            card.GetComponent<UIHoverSize>().enabled = false;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
