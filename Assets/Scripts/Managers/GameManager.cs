using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    TurnManager turnManager;
    UIManager uiManager;
    int entityIdCounter = 0;
    int cardOrder = 0;
    bool waitForCompletion = false;

    private void Awake() {
        turnManager = FindObjectOfType<TurnManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start() {
        SetupGame();
    }

    public bool GetWaitForCompletion() {
        return waitForCompletion;
    }

    public void SetWaitForCompletion(bool value) {
        waitForCompletion = value;
    }

    public int GetNextEntityId() {
        int nextEntityId = entityIdCounter;
        entityIdCounter++;
        return nextEntityId;
    }

    public int GetNextCardOrder() {
        int nextCardOrder = cardOrder;
        cardOrder++;
        return nextCardOrder;
    }

    void SetupGame() {
        uiManager.SetLocationSelectionPrompt(false);
        StartCoroutine(turnManager.Initialize());
    }
}
