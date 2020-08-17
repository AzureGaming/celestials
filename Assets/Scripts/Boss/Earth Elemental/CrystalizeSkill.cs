using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalizeSkill : MonoBehaviour {
    public AudioSource attackAudio;
    public EarthElementalSkillIndicators skillIndicators;
    public AttackQueueManager queueManager;
    public Animator animator;
    GameManager gameManager;
    Summoner summoner;

    bool isCasting;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        summoner = FindObjectOfType<Summoner>();
    }

    public void QueueSkill() {
        skillIndicators.SetCrystalize();
        queueManager.Queue(new AttackQueueManager.AttackCommand(EarthElemental.Moves.CRYSTALIZE));
    }

    public void OnCastAnimationEnd() {
        isCasting = false;
    }

    public IEnumerator CastSkill() {
        isCasting = true;
        animator.SetTrigger("Attack3");
        attackAudio.Play();
        yield return new WaitUntil(() => !isCasting);
        Crystalize();
        skillIndicators.ClearIndicator();
    }

    void Crystalize() {
        animator.SetBool("IsProtected", true);
    }
}
