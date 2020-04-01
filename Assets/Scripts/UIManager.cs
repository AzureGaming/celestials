using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    GameObject locationSelection;

    private void Awake() {
        locationSelection = GameObject.Find("Location Selection");
    }

    public void SetLocationSelectionPrompt(bool value) {
        locationSelection.gameObject.SetActive(value);
    }
}
