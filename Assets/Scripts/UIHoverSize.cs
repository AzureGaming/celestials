using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHoverSize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Transform transformToModify;
    Vector3 startingPos;
    Vector3 startingScale;
    Hand hand;

    private void Awake() {
        hand = FindObjectOfType<Hand>();
    }

    private void Start() {
        startingPos = transformToModify.position;
        startingScale = transformToModify.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("Enter");
        transformToModify.Translate(new Vector3(0, 60, transform.position.z));
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log("Exit");
        transformToModify.position = startingPos;
    }
}
