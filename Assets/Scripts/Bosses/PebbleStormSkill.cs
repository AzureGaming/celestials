using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleStormSkill : MonoBehaviour {
    public AudioSource attackAudio;
    public EarthElementalSkillIndicators skillIndicators;
    public AttackQueueManager queueManager;
    public Animator animator;
    public GameObject pebbleCard;
    CardManager cardManager;

    bool isCasting;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
    }

    public void QueueSkill() {
        skillIndicators.SetPebbleStorm();
        queueManager.Queue(new AttackQueueManager.AttackCommand(EarthElemental.Moves.PEBBLESTORM));
    }

    public void OnCastAnimationEnd() {
        isCasting = false;
    }

    public IEnumerator CastSkill() {
        isCasting = true;
        animator.SetTrigger("Attack1");
        yield return new WaitUntil(() => !isCasting);
        attackAudio.Play();
        PebbleStorm();
        skillIndicators.ClearIndicator();
    }

    void PebbleStorm() {
        cardManager.AddToDeck(Instantiate(pebbleCard).GetComponent<Card>());
        cardManager.AddToDeck(Instantiate(pebbleCard).GetComponent<Card>());
    }
}
