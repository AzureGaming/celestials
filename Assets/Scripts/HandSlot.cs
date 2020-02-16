using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour {
    public void InsertCard(GameObject card) {
        DestroyChildren();
        card.transform.parent = transform;
        card.transform.position = transform.position;
    }

    public void DestroyChildren() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }
}
