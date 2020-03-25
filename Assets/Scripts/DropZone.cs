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
        Debug.Log("On drop to " + gameObject.name);
        Draggable droppedObject = eventData.pointerDrag.GetComponent<Draggable>();

        if (droppedObject != null) {
            droppedObject.parentToReturnTo = transform;
            StartCoroutine(boardManager.InsertSummon(0, 0, droppedObject.GetComponent<Card>()));
            return;
        }
    }
}
