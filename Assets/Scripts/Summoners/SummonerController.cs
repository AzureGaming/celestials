using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SummonerController : MonoBehaviour {
    public GameObject flyingCard;
    public SpellEffect stasis;
    public SpellEffect stockpile;
    public AccelerateEffect accelerate;
    public FutureSightEffect futureSight;
    public SpellEffect paradox;
    public AudioSource stasisEntryAudio;
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

    public IEnumerator CastStasis() {
        stasisEntryAudio.Play();
        yield break;
    }

    public IEnumerator CastStockPile() {
        yield return StartCoroutine(stockpile.Activate());
        yield return new WaitUntil(() => stockpile.IsDone());
    }

    public IEnumerator CastAccelerate() {
        yield return StartCoroutine(accelerate.Activate());
        yield return new WaitUntil(() => accelerate.IsDone());
    }

    public IEnumerator CastFutureSight() {
        yield return StartCoroutine(futureSight.Activate());
        yield return new WaitUntil(() => futureSight.IsDone());
    }

    public IEnumerator CastParadox() {
        yield return StartCoroutine(paradox.Activate());
        yield return new WaitUntil(() => paradox.IsDone());
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
