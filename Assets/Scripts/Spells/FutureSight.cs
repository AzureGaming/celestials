using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureSight : CardEffect {
    public GameObject displayArea;
    PlayZone playZone;
    int numberOfCards = 3;
    CardManager cardManager;
    CardSnapToFutureSightDisplay cardSnap;
    Hand hand;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
        displayArea = FindObjectOfType<UIManager>().futureSightDisplay;
        playZone = FindObjectOfType<PlayZone>();
        hand = FindObjectOfType<Hand>();
    }

    private void Start() {
        cardSnap = playZone.futureSightCardSnap;
    }

    public override IEnumerator Apply() {
        yield return StartCoroutine(FutureSightRoutine());
    }

    public void AcceptCards() {
        List<Transform> cards = new List<Transform>();
        foreach (Transform transform in displayArea.transform.GetChild(0)) {
            cards.Add(transform);
        }
        foreach (Transform transform in cards) {
            cardManager.QueueFutureSightCard(transform.GetComponent<Card>());
        }
        displayArea.SetActive(false);
        cardSnap.enabled = false;
        playZone.enabled = true;
        hand.enabled = true;
        foreach (Transform transform in displayArea.transform.GetChild(0)) {
            Destroy(transform.gameObject);
        }
    }

    IEnumerator FutureSightRoutine() {
        displayArea.SetActive(true);
        playZone.enabled = false;
        cardSnap.enabled = true;
        hand.enabled = false;
        // view the top 3 cards on the deck
        for (int i = 0; i < numberOfCards; i++) {
            Card card = cardManager.DrawCard();
            GameObject instance = Instantiate(card.gameObject);
            instance.transform.SetParent(displayArea.transform.GetChild(0).transform);
            instance.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }

        yield break;
    }
}
