using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
    public GameObject cardPrefab;
    CardObject[] cards;

    private void Awake() {
        cards = Resources.LoadAll<CardObject>("Cards");
        Debug.Log("Loaded cards" + cards.Length);
    }

    public GameObject CreateCard() {
        GameObject card = Instantiate(cardPrefab, transform);
        card.GetComponent<Card>().LoadCard(cards[Random.Range(0, cards.Length - 1)]);
        return card;
    }
}
