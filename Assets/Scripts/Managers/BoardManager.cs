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
                tile.tag = "[Tile] Summon";
                grid[stage][row] = tile;
                tileCounter++;
            }
        }

        for (int row = 0; row < rowLimit; row++) {
            grid[3][row] = null;
        }
    }

    public void IncrementCardOrder(int value) {
        cardOrder += value;
    }

    public int GetCardOrder() {
        return cardOrder;
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
        Debug.Log("Resolve Attack Phase");
        for (int stage = stageLimit - 1; stage >= 0; stage--) {
            Tile[] tilesInStage = new Tile[stageLimit];
            Array.Copy(grid[stage], tilesInStage, stageLimit);
            System.Array.Sort(tilesInStage, (tileX, tileY) => {
                Summon summonX = tileX.GetComponentInChildren<Summon>();
                Summon summonY = tileY.GetComponentInChildren<Summon>();
                int x, y;
                x = summonX ? summonX.getOrder() : -1;
                y = summonY ? summonY.getOrder() : -1;
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
        if (card.type == CardType.Summon) {
            StartCoroutine(PlaySummon(card));
        } else if (card.type == CardType.Spell) {
            PlaySpell(card);
        } else {
            Debug.Log("Unknown cardtype encountered: " + card.type);
        }
    }

    public Tile GetDestination(Summon summon, int offset) {
        Tile currentTile = GetCurrentTile(summon);
        Debug.Log("Find next for " + summon.name + currentTile.row + " " + currentTile.column + " " + offset);
        return Array.Find(tiles, (Tile tile) => {
            if (tile.row == currentTile.row && tile.column == currentTile.column + offset) {
                Debug.Log("next" + tile.name + tile.column + tile.row);
                return true;
            }
            return false;
        });
    }

    Tile GetCurrentTile(Summon summon) {
        return Array.Find(tiles, (Tile tile) => {
            if (tile.GetComponentInChildren<Summon>() == summon) {
                return true;
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
        GetQueue()[0].Summon(card);
        player.LoseMana(card.manaCost);
        ClearQueue();
    }

    void PlaySpell(Card card) {
        Debug.Log("Cast spell: " + card.name);
        card.ActivateEffect();
    }

    IEnumerator ResolveStageMovement(int stageIndex) {
        Debug.Log("Resolve stage movement" + stageIndex);
        for (int rowIndex = 0; rowIndex < rowLimit; rowIndex++) {
            Tile tile = grid[stageIndex][rowIndex];
            Summon summon = tile.GetComponentInChildren<Summon>();
            if (summon) {
                if (GetDestination(summon, summon.GetRange())) {
                    summon.Walk();
                } else {
                    summon.Die();
                }
            }
        }

        yield return new WaitWhile(() => Array.Find(tiles, (Tile tile2) => {
            Summon summon = tile2.GetComponentInChildren<Summon>();
            return summon && !summon.DoneMoving() ? true : false;
        }));
    }


    public bool CheckAttackRange(int range, int colIndex, int rowIndex) {
        int targetColIndex = colIndex + range;
        if (targetColIndex < grid.Length && rowIndex < grid.Length) {
            Debug.Log("Check atack range" + " " + range + " " + colIndex + " " + rowIndex + " " + grid[targetColIndex][rowIndex]);
            if (grid[targetColIndex][rowIndex] == null) {
                return true;
            }
        } else {
            Debug.LogWarning("Range check is invalid: Range - " + range + " colIndex: " + colIndex + " rowIndex: " + rowIndex);
        }
        return false;
    }

    //IEnumerator MoveSummonToTile(Summon summon) {
    //    Debug.Log("Move");

    //    // Implement: different types of movement
    //    Tile currentPos = summon.GetComponentInParent<Tile>();
    //    //int stagesToMove = summon.card.movementSpeed;
    //    int stagesToMove = 0;
    //    int endCol = currentPos.column + stagesToMove;
    //    Debug.Log("Move summon " + currentPos.column + " " + currentPos.row + " " + stagesToMove);

    //    if (grid.ElementAtOrDefault(currentPos.row) == null) {
    //        Debug.LogWarning("Row " + currentPos.row + " does not exist in grid.");
    //        yield break;
    //    }

    //    if (grid[currentPos.row].ElementAtOrDefault(endCol) == null) {
    //        //StartCoroutine(summon.Die());
    //        yield break;
    //    }

    //    if (!grid[currentPos.row][endCol].CompareTag("[Tile] Summon")) {
    //        Debug.Log("Cell exists but is not a valid tile");
    //        yield break;
    //    }

    //    //yield return StartCoroutine(summon.WalkToTile(grid[currentPos.row][endCol]));
    //    summon.Walk();
    //}

}
