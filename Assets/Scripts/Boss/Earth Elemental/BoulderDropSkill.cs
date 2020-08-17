using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDropSkill : MonoBehaviour {
    public AudioSource impactAudio;
    public EarthElementalSkillIndicators skillIndicators;
    public AttackQueueManager queueManager;
    public Animator animator;
    CardManager cardManager;
    GameManager gameManager;
    public GameObject boulder;
    public GameObject boulderSpawner;
    Summoner summoner;

    bool isCasting;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
        gameManager = FindObjectOfType<GameManager>();
        summoner = FindObjectOfType<Summoner>();
    }

    public void QueueSkill() {
        skillIndicators.SetBoulderDrop();
        queueManager.Queue(new AttackQueueManager.AttackCommand(EarthElemental.Moves.BOULDERDROP));
    }

    public void OnCastAnimationEnd() {
        isCasting = false;
    }

    public IEnumerator CastSkill() {
        isCasting = true;
        animator.SetTrigger("Attack1");
        yield return new WaitUntil(() => !isCasting);
        yield return StartCoroutine(BoulderDrop());
        skillIndicators.ClearIndicator();
    }

    IEnumerator BoulderDrop() {
        gameManager.SetWaitForCompletion(true);
        Instantiate(boulder, boulderSpawner.transform);
        yield return new WaitUntil(() => DoneBoulderDrop());
        impactAudio.Play();
        yield return StartCoroutine(summoner.TakeDamage(3));
    }

    bool DoneBoulderDrop() {
        return !gameManager.GetWaitForCompletion();
    }
}
