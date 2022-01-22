using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BasePiece
{
    public override void Init(Cell newCell, Factions newFaction, Sprite newImage)
    {
        base.Init(newCell, newFaction, newImage);

        steps = newFaction == Factions.White ? new Vector3Int(0, 1, 1) : new Vector3Int(0, -1, -1);

        //GetComponent<SpriteRenderer>().sprite = newImage;
    }

    protected override void Move()
    {
        base.Move();
        
        CheckForPromotion();
    }

    private bool MatchesState(int targetX, int targetY, CellState targetState)
    {
        CellState cellState = CellState.None;
        cellState = _gameManager.cellManager.ValidateCell(targetX, targetY, faction);

        if (cellState == targetState)
        {
            _gameManager.cellManager.lightedCells.Add(_gameManager.board._cells[targetX, targetY]);
            return true;
        }

        return false;
    }

    private void CheckForPromotion()
    {
        // POsicion a la que mover
        int currentX = currentCellParent.BoardPosition.x;
        int currentY = currentCellParent.BoardPosition.y;

        // VErificamos si el peon llego al final del tablero
        CellState cellState = _gameManager.cellManager.ValidateCell(currentX, currentY + steps.y, faction);

        if (cellState == CellState.OutOfBounds)
        {
            _gameManager.pieceManager.PromotePiece(this, this.currentCellParent, this.faction);
        }
    }

    protected override void CheckPath()
    {
        
        int currentX = currentCellParent.BoardPosition.x;
        int currentY = currentCellParent.BoardPosition.y;

        // Verificamos Arriba a la izquierda si hay un enemigo
        MatchesState(currentX - steps.z, currentY + steps.z, CellState.Enemy);

        // Movemos
        if (MatchesState(currentX, currentY + steps.y, CellState.Empty))
        {
            // Si la celda de enfrente esta libre y es nuestro primer movimiento movemos 
            //dos cuadros
            if (isMyFirstMove)
            {
                MatchesState(currentX, currentY + (steps.y * 2), CellState.Empty);
            }
        }

        // Verificamos arriba a la Derecha si hay un enemigo 
        MatchesState(currentX + steps.z, currentY + steps.z, CellState.Enemy);

        //mostramos el path
        _gameManager.cellManager.ShowCells();
    }
}
