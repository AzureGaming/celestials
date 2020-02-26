using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour {
    TurnManager turnManager;
    Player player;

    private void Awake() {
        turnManager = FindObjectOfType<TurnManager>();
        player = FindObjectOfType<Player>();
    }

    private void OnMouseUpAsButton() {
        Debug.Log("Mouse up");
        if (turnManager.state == GameState.PLAYERTURN) {
            player.SetIsTurnDone(true);
        }
    }
}
