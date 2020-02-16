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
        } else if (mulligans.Count < 2) {
            mulligans.Add(cardId);
        }
        UpdateIdsDisplay();
    }

    void SetupGame() {
        StartCoroutine(turnManager.Initialize());
    }

    void UpdateIdsDisplay() {
        mulliganIds.text = "";
        foreach (int id in mulligans) {
            mulliganIds.text += id.ToString() + "\n";
        }
    }
}
