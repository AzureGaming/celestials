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

    }
}
