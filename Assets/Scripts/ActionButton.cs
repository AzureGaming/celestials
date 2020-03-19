
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerClickHandler {
    TurnManager turnManager;
    Player player;

    public void OnPointerClick(PointerEventData eventData) {
        if (turnManager.state == GameState.PLAYERTURN) {
            player.SetIsTurnDone(true);
        } else if (turnManager.state == GameState.MULLIGAN) {
            turnManager.SetMulliganConfirmed(true);
        }
    }

    private void Awake() {
        turnManager = FindObjectOfType<TurnManager>();
        player = FindObjectOfType<Player>();
    }
}
