using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject futureSightDisplay;
    public GameObject futureSightQueue;
    GameObject locationSelection;
    ManaDisplay manaDisplay;

    private void Awake() {
        locationSelection = GameObject.Find("Location Selection");
        manaDisplay = GetComponentInChildren<ManaDisplay>();
    }

    private void Start() {
        futureSightDisplay.SetActive(false);
    }

    public void SetLocationSelectionPrompt(bool value) {
        locationSelection.gameObject.SetActive(value);
    }

    public void SetMana(int current, int max) {
        manaDisplay.UpdateText(current, max);
    }
}
