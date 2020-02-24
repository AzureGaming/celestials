using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour {
    int order;

    public int getOrder() {
        return order;
    }

    public IEnumerator ExecuteAction() {
        Debug.Log("Implement summon action execution");
        yield break;
    }
}
