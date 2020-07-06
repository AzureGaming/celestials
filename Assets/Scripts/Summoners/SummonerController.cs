using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SummonerController : MonoBehaviour {
    public GameObject flyingCard;
    Animator animator;
    bool routineRunning = false;
    bool doneAnimation;

    private void Awake() {
        animator = GetComponent<Animator>();    
    }

    public void Summon(Tile tile) {
        animator.SetTrigger("isCasting");
        GameObject flyingCardObj = Instantiate(flyingCard, transform);
        flyingCardObj.GetComponent<FlyingCard>().sunset = tile.transform;
        SetRoutineRunning(true);
    }

    public void CastSpell() {
        animator.SetTrigger("isCasting");
        doneAnimation = false;
    }

    public void SetRoutineRunning(bool value) {
        routineRunning = value;
    }

    public bool GetRoutineRunning() {
        return routineRunning;
    }

    public void OnCastSpellAnimationEnd() {
        doneAnimation = true;
    }

    public bool GetDoneAnimation() {
        return doneAnimation;
    }
}
