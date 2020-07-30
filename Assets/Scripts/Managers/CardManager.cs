﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
    public GameObject cardPrefab;
    Card[] cards;
    Deck deck;
    Hand hand;
    List<Card> discardPile = new List<Card>();
    Mulligan mulligan;
    GameObject cardQueue;

    private void Awake() {
        hand = FindObjectOfType<Hand>();
        cards = Resources.LoadAll<Card>("Loadable Cards");
        Debug.Log("Loaded cards" + cards.Length);
        deck = FindObjectOfType<Deck>();
        mulligan = FindObjectOfType<Mulligan>();
        cardQueue = FindObjectOfType<UIManager>().futureSightQueue;
    }

    public IEnumerator DrawToHand() {
        Card card = DrawCard();
        GameObject instance = Instantiate(card.gameObject);
        instance.SetActive(true);
        instance.transform.SetParent(hand.transform);
        instance.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        yield break;
    }

    public void AddToDeck(Card card) {
        deck.AddCard(card);
    }

    public Card[] GetCardsInHand() {
        return hand.GetCards();
    }

    public void AddToDiscard(Card card) {
        discardPile.Add(card);
    }

    public void TrashCard(Card card) {
        Destroy(card.gameObject);
    }

    public Card DrawCard() {
        if (cardQueue.GetComponentsInChildren<Transform>().Length > 1) {
            Card card = cardQueue.transform.GetChild(0).GetComponent<Card>();
            Card newCard = Instantiate(card.gameObject).GetComponent<Card>();
            Destroy(card.gameObject);
            return newCard;
        }
        return deck.RemoveCard();
    }

    public List<Card> GetDiscardPile() {
        return discardPile;
    }

    public void ClearDiscardPile() {
        discardPile.Clear();
    }

    public void QueueFutureSightCard(Card card) {
        GameObject cardObj = Instantiate(card.gameObject);
        cardObj.transform.SetParent(cardQueue.transform);
    }

    GameObject CompileCard(Entity data) {
        return cardPrefab;
    }

    GameObject GetCardPrefab() {
        // Will break if no valid cards
        int randomIndex = Random.Range(0, cards.Length);
        return cards[randomIndex].gameObject;
        //if (!hand.handCardIds.Contains(cards[randomIndex].id)) {
        //    return cards[randomIndex];
        //}
        //return GetValidCardData();
        //return cards[1];
    }
}
