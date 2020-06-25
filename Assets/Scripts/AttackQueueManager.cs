using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackQueueManager : MonoBehaviour {
    public class AttackCommand {
        public EarthElemental.Moves moveName;
        public int[][] coords;
        public AttackCommand(EarthElemental.Moves _moveName, int[][] _coords) {
            moveName = _moveName;
            coords = _coords;
        }
    }
    public List<AttackCommand> attackCommands = new List<AttackCommand>();
    public BoardManager boardManager;

    public void Queue(AttackCommand attackCommand) {
        boardManager.ResetAllIndicators();
        attackCommands.Add(attackCommand);
        UpdateIndicators(attackCommand);
    }

    public AttackCommand DeQueue() {
        if (attackCommands.Count < 0) {
            Debug.LogWarning("No attack command dequeued");
        }
        AttackCommand attackCommand = attackCommands.Last();
        attackCommands.Remove(attackCommand);
        UpdateIndicators(attackCommand);
        return attackCommand;
    }

    public void RefreshIndicators() {
        if (attackCommands.Count > 0) {
            UpdateIndicators(attackCommands.Last());
        }
    }

    void UpdateIndicators(AttackCommand attackCommand) {
        foreach (int[] coord in attackCommand.coords) {
            boardManager.GetTile(coord[0], coord[1]).SetAttackIndicator(true);
        }
    }
}
