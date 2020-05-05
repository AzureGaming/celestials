using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    Tile[][] grid;
    GameObject tilePrefab;
    Board board;
    Tile[] tiles;
    UIManager uiManager;
    GameManager gameManager;
    Player player;
    int cardOrder = 0;
    int stageLimit = 3;
    int rowLimit = 3;
    int attackCounter = 0;
    int attacksToWaitFor = 0;
    List<Tile> queue;

    private void Awake() {
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<Player>();
        board = GetComponent<Board>();
        tiles = GetComponentsInChildren<Tile>();
        gameManager = FindObjectOfType<GameManager>();
        grid = new Tile[4][];
        grid[0] = new Tile[3];
        grid[1] = new Tile[3];
        grid[2] = new Tile[3];
        // boss cells
        grid[3] = new Tile[3];

        queue = new List<Tile>();
    }

    void Start() {
        int tileCounter = 0;
        for (int row = 0; row < rowLimit; row++) {
            for (int stage = 0; stage < stageLimit; stage++) {
                Tile tile = tiles[tileCounter];
                tile.column = stage;
                tile.row = row;
                tile.type = TileType.Summon;
                grid[stage][row] = tile;
                tileCounter++;
            }
        }

        for (int row = 0; row < rowLimit; row++) {
            Tile tile = tiles[tileCounter];
            tile.column = 3;
            tile.row = row;
            tile.type = TileType.Boss;
            grid[3][row] = tile;
            tileCounter++;
        }
    }

    public void DetectSummonableSpace() {
        int stage = 0;
        for (int row = 0; row < rowLimit; row++) {
            Tile tile = grid[stage][row];
            if (tile.CheckOccupied()) {
                tile.SetInvalidState();
            } else {
                tile.SetValidState();
            }
        }
    }

    public void SetNeutral() {
        foreach (Tile tile in tiles) {
            tile.SetNeutralState();
        }
    }

    public void DetectSummons() {
        foreach (Tile tile in tiles) {
            if (tile.CheckOccupied()) {
                tile.SetValidState();
            } else {
                tile.SetInvalidState();
            }
        }
    }

    public void HandleSummoned() {
        foreach (Tile tile in tiles) {
            tile.SetNeutralState();
        }
        uiManager.SetLocationSelectionPrompt(false);
    }

    public IEnumerator ResolveMovePhase() {
        for (int stage = stageLimit - 1; stage >= 0; stage--) {
            yield return StartCoroutine(ResolveStageMovement(stage));
        }
    }

    public IEnumerator ResolvePowerAbilityPhase() {
        yield break;
    }

    public IEnumerator ResolveAttackPhase() {
        for (int stage = stageLimit - 1; stage >= 0; stage--) {
            Tile[] tilesInStage = new Tile[stageLimit];
            Array.Copy(grid[stage], tilesInStage, stageLimit);
            System.Array.Sort(tilesInStage, (tileX, tileY) => {
                Summon summonX = tileX.GetComponentInChildren<Summon>();
                Summon summonY = tileY.GetComponentInChildren<Summon>();
                int x, y;
                x = summonX ? summonX.GetOrder() : -1;
                y = summonY ? summonY.GetOrder() : -1;
                return x - y;
            });

            foreach (Tile tile in tilesInStage) {
                Summon summon = tile.GetComponentInChildren<Summon>();
                if (summon != null) {
                    summon.Attack();
                    yield return new WaitUntil(() => summon.DoneAttacking());
                }
            }
        }
        yield break;
    }

    public void IncrementAttackCounter() {
        attackCounter += 1;
    }

    public void IncrementAttacksToWaitFor() {
        attacksToWaitFor += 1;
    }

    public void AddToQueue(Tile tile) {
        queue.Add(tile);
    }

    public List<Tile> GetQueue() {
        return queue;
    }

    public void ClearQueue() {
        queue.Clear();
    }

    public void PlayCard(Card card) {
        CardType type = card.GetType();
        if (type == CardType.Summon) {
            StartCoroutine(PlaySummon(card));
        } else if (type == CardType.Spell) {
            PlaySpell(card);
        } else {
            Debug.Log("Unknown cardtype encountered: " + type);
        }
    }

    public Tile GetDestination(int id, int offset) {
        Tile currentTile = GetCurrentTile(id);
        return Array.Find(tiles, (Tile tile) => {
            if (currentTile && tile.row == currentTile.row && tile.column == currentTile.column + offset) {
                return true;
            }
            return false;
        });
    }

    Tile GetCurrentTile(int id) {
        return Array.Find(tiles, (Tile tile) => {
            Summon summon = tile.GetComponentInChildren<Summon>();
            if (summon) {
                if (summon.GetId() == id) {
                    return true;
                }
            }
            return false;
        });
    }

    IEnumerator PlaySummon(Card card) {
        Debug.Log("Summon: " + card.name);
        uiManager.SetLocationSelectionPrompt(true);
        DetectSummonableSpace();
        yield return new WaitUntil(() => GetQueue().Count == 1);
        SetNeutral();
        card.SummonAt(GetQueue()[0]);
        player.LoseMana(card.GetManaCost());
        ClearQueue();
        Destroy(card.gameObject);
    }

    void PlaySpell(Card card) {
        card.ActivateEffect();
    }

    IEnumerator ResolveStageMovement(int stageIndex) {
        for (int rowIndex = 0; rowIndex < rowLimit; rowIndex++) {
            Tile tile = grid[stageIndex][rowIndex];
            Summon summon = tile.GetComponentInChildren<Summon>();
            summon?.Walk();
        }

        yield return new WaitWhile(() => Array.Find(tiles, (Tile tile2) => {
            Summon summon = tile2.GetComponentInChildren<Summon>();
            return summon && !summon.DoneMoving() ? true : false;
        }));
    }
}
