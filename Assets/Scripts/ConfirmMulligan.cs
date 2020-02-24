using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmMulligan : MonoBehaviour {
    TurnManager turnManager;

    private void Awake() {
        turnManager = FindObjectOfType<TurnManager>();
    }

    void OnMouseUpAsButton() {
        turnManager.ConfirmMulligan();
    }
}
