using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
    public GameObject cardPrefab;
    CardObject[] cards;
    Hand hand;

    private void Awake() {
        hand = FindObjectOfType<Hand>();
        cards = Resources.LoadAll<CardObject>("Cards");
        Debug.Log("Loaded cards" + cards.Length);
    }

    public GameObject CreateCard() {
        GameObject card = Instantiate(cardPrefab, transform);
        card.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        card.GetComponent<Card>().LoadCard(GetValidCardData());
        return card;
    }

    CardObject GetValidCardData() {
        // Will break if no valid cards
        //int randomIndex = Random.Range(0, cards.Length - 1);
        //if (!hand.handCardIds.Contains(cards[randomIndex].id)) {
        //    return cards[randomIndex];
        //}
        //return GetValidCardData();
        return cards[0];
    }
}
