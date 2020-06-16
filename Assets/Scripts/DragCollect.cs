using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCollect : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData) {
        eventData.pointerDrag.gameObject.transform.SetParent(transform);
        eventData.pointerDrag.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
