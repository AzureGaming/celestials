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
    public ThrowBoulderSkill rockThrow;

    public void Queue(AttackCommand command) {
        if (attackCommands.Count >= 2) {
            Debug.LogWarning("Queue is full");
            return;
        }
        boardManager.ResetAllIndicators();
        attackCommands.Add(command);
        foreach (int[] coord in command.coords) {
            boardManager.GetTile(coord[0], coord[1]).SetAttackIndicator(true);
        }
    }

    public IEnumerator ProcessNextAttack() {
        AttackCommand command = DeQueue();
        if (command.moveName == EarthElemental.Moves.PEBBLESTORM) {
        } else if (command.moveName == EarthElemental.Moves.BOULDERDROP) {
        } else if (command.moveName == EarthElemental.Moves.ROCKTHROW) {
            yield return StartCoroutine(rockThrow.CastSkill(command));
        } else if (command.moveName == EarthElemental.Moves.CRYSTALBLOCK) {
        } else if (command.moveName == EarthElemental.Moves.CRYSTALIZE) {
        } else {
            Debug.LogWarning("Unknown Attack processed");
        }
    }

    public AttackCommand DeQueue() {
        if (attackCommands.Count <= 0) {
            Debug.LogWarning("No attack command dequeued");
        }
        AttackCommand attackCommand = attackCommands.Last();
        attackCommands.Remove(attackCommand);
        boardManager.ResetAllIndicators();
        foreach (int[] coord in attackCommand.coords) {
            boardManager.GetTile(coord[0], coord[1]).SetAttackIndicator(true);
        }
        //UpdateIndicators(attackCommand);
        return attackCommand;
    }

    public void RefreshIndicators() {
        if (attackCommands.Count > 0) {
            UpdateIndicators(attackCommands.Last());
        }
    }

    void UpdateIndicators(AttackCommand command) {
        boardManager.ResetAllIndicators();
        if (command.moveName == EarthElemental.Moves.PEBBLESTORM) {
        } else if (command.moveName == EarthElemental.Moves.BOULDERDROP) {
        } else if (command.moveName == EarthElemental.Moves.ROCKTHROW) {
            command.coords = rockThrow.CalculateTargets();
        } else if (command.moveName == EarthElemental.Moves.CRYSTALBLOCK) {
        } else if (command.moveName == EarthElemental.Moves.CRYSTALIZE) {
        } else {
            Debug.LogWarning("Unknown Attack processed");
        }

        foreach (int[] coord in command.coords) {
            boardManager.GetTile(coord[0], coord[1]).SetAttackIndicator(true);
        }
    }
}
