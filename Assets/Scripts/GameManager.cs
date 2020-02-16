using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public List<int> mulligans;
    TurnManager turnManager;
    Text mulliganIds;

    private void Awake() {
        turnManager = FindObjectOfType<TurnManager>();
        mulligans = new List<int>(2);
        mulliganIds = GameObject.Find("MulliganIds").GetComponent<Text>();
    }

    private void Start() {
        SetupGame();
    }

    public void SetMulligan(int cardId) {
        if (mulligans.Contains(cardId)) {
            mulligans.Remove(cardId);
            UpdateIdsDisplay(cardId.ToString(), "remove");
        } else if (mulligans.Count < 2) {
            mulligans.Add(cardId);
            UpdateIdsDisplay(cardId.ToString(), "insert");
        }
    }

    void SetupGame() {
        StartCoroutine(turnManager.Initialize());
    }

    void UpdateIdsDisplay(string id, string operation) {
        if (operation == "insert") {
            mulliganIds.text += "\n" + id;
        } else if (operation == "remove") {
            mulliganIds.text.Replace(id, "");
        }
    }
}
