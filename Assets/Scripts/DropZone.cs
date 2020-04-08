using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {
    GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("On drop to " + gameObject.name);
        Draggable droppedObject = eventData.pointerDrag.GetComponent<Draggable>();

        if (droppedObject != null) {
            //droppedObject.parentToReturnTo = transform;
            StartCoroutine(gameManager.StartCardSummon(droppedObject.GetComponent<Card>()));
            Destroy(droppedObject.gameObject);
        }
    }
}
