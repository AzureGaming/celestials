using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverSizeIncrease : MonoBehaviour {
    TurnManager turnManager;
    Vector3 startingScale;

    private void Awake() {
        turnManager = FindObjectOfType<TurnManager>();
    }

    private void Start() {
        startingScale = transform.localScale;
    }

    private void OnMouseOver() {
        if (turnManager.state == GameState.PLAYERTURN) {
            transform.localScale = startingScale * 2;
        }
    }

    private void OnMouseExit() {
        transform.localScale = startingScale;
    }
}
