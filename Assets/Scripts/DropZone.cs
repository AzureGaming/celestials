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
            droppedObject.GetComponent<CanvasGroup>().alpha = 0;
            boardManager.PlayCard(droppedObject.GetComponent<Card>());
        }
    }
}
