using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SummonerController))]
public class Summoner : MonoBehaviour {
    SummonerController controller;

    private void Awake() {
        controller = GetComponent<SummonerController>();    
    }

    public void Summon() {
        controller.Summon();
    }

    public void OnCastAnimationEventEnd() {
        
    }
}
