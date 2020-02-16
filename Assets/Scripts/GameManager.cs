using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    List<string> mulligans;
    TurnManager turnManager;

    private void Awake() {
        turnManager = FindObjectOfType<TurnManager>();
        mulligans = new List<string>(2);
    }

    private void Start() {
        SetupGame();
    }

    public void SetMulligan(string cardId) {
        if (mulligans.Contains(cardId)) {
            mulligans.Remove(cardId);
        } else if (mulligans.Count < 2) {
            mulligans.Add(cardId);
        }
    }

    void SetupGame() {
        StartCoroutine(turnManager.Initialize());
    }
}
