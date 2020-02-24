using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    int health;

    private void Start() {
        health = 30;
    }

    public IEnumerator SetupBoss() {
        yield break;
    }

    public int getHealth() {
        return health;
    }

    public IEnumerator RunTurnRoutine() {
        Debug.Log("Implement Boss AI turn AI");
        yield break;
    }
}
