using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    Summon[] summons;

    public Summon[] GetSummons() {
        return GetComponentsInChildren<Summon>();
    }
}
