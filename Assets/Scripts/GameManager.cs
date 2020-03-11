using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public List<int> mulligans;
    TurnManager turnManager;
    Text mulliganIds;
    int heldCardId;
    bool isHoldingCard;

    private void Awake() {
        turnManager = FindObjectOfType<TurnManager>();
        mulliganIds = GameObject.Find("MulliganIds").GetComponent<Text>();
    }

    private void Start() {
        mulligans = new List<int>(2);
        heldCardId = -1;
        //SetupGame();
    }

    public bool GetIsHoldingCard() {
        return isHoldingCard;
    }

    public int GetHeldCardId() {
        return heldCardId;
    }

    public void SetIsHoldingCard(bool value) {
        if (turnManager.state == GameState.PLAYERTURN) {
            isHoldingCard = value;
        }
    }

    public void SetMulligan(int cardId) {
        if (mulligans.Contains(cardId)) {
            mulligans.Remove(cardId);
        } else if (mulligans.Count < 2) {
            mulligans.Add(cardId);
        }
        UpdateIdsDisplay();
    }

    public void SetLastTouchedCard(int cardId) {
        heldCardId = cardId;
    }

    public bool CanHoldCard(int cardId) {
        return heldCardId == cardId;
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
