using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnforcerController : SummonController {
    public override void UseHowl() {
        base.UseHowl();
        StartCoroutine(Howl());
    }

    IEnumerator Howl() {
        if (!CheckValidHowl()) {
            yield break;
        }
        boardManager.DetectSummonsExcluding(GetId());
        FindObjectOfType<Hand>().SetCardsInteractivity(false);
        boardManager.ClearQueue();
        yield return new WaitUntil(() => boardManager.GetQueue().Count == 1);
        Summon summon = boardManager.GetQueue()[0].GetSummon();
        boardManager.SetNeutral();
        FindObjectOfType<Hand>().SetCardsInteractivity(true);
        yield return StartCoroutine(boardManager.ResolveTurnForSummon(summon));
        boardManager.ClearQueue();
        howlRoutineRunning = false;
    }

    bool CheckValidHowl() {
        Summon summonCheck = null;
        for (int i = 0; i < 3; i++) {
            Summon randomSummon = boardManager.GetRandomSummonInStage(i);
            if (randomSummon != null && randomSummon != GetComponent<Summon>()) {
                summonCheck = randomSummon;
            }
        }
        if (summonCheck == null) {
            return false;
        }
        return true;
    }
}
