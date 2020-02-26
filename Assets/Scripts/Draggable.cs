using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
    GameManager gameManager;
    TurnManager turnManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        turnManager = FindObjectOfType<TurnManager>();
    }

    private void OnMouseDown() {
        gameManager.SetIsHoldingCard(true);
    }

    private void OnMouseUpAsButton() {
        gameManager.SetIsHoldingCard(false);
    }

    private void OnMouseDrag() {
        if (gameManager.GetIsHoldingCard()) {
            Vector3 pos = Input.mousePosition;
            pos.z = transform.position.z - Camera.main.transform.position.z;
            transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
    }
}
