using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    GameObject locationSelection;
    ManaDisplay manaDisplay;

    private void Awake() {
        locationSelection = GameObject.Find("Location Selection");
        manaDisplay = GetComponentInChildren<ManaDisplay>();
    }

    public void SetLocationSelectionPrompt(bool value) {
        locationSelection.gameObject.SetActive(value);
    }

    public void SetMana(int current, int max) {
        manaDisplay.UpdateText(current, max);
    }
}
