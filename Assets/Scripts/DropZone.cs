
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {
    BoardManager boardManager;

    private void Awake() {
        boardManager = FindObjectOfType<BoardManager>();
    }

    public void OnDrop(PointerEventData eventData) {
        Draggable droppedObject = eventData.pointerDrag.GetComponent<Draggable>();
        if (droppedObject != null) {
            //droppedObject.parentToReturnTo = transform;
            boardManager.PlayCard(droppedObject.GetComponent<Card>());
            droppedObject.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            //Destroy(droppedObject.gameObject);
        }
    }
}
