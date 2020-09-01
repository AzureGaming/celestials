using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHoverSize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Transform transformToModify;
    Vector3 lastPos;
    Vector3 startPos;
    Hand hand;

    private void Awake() {
        hand = FindObjectOfType<Hand>();
    }

    private void Start() {
        startPos = transformToModify.position;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Vector3 newPos = transformToModify.position;
        newPos.y += 80;
        transformToModify.position = newPos;
        lastPos = transformToModify.position;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (lastPos != null) {
            Vector3 newPos = transformToModify.position;
            newPos.y = startPos.y;
            transformToModify.position = newPos;
        }
        lastPos = default;
    }
}
