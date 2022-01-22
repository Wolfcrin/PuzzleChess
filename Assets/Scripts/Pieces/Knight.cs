using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BasePiece
{
    //El caballo mueve en L
    public override void Init(Cell newCell, Factions newFaction, Sprite newImage)
    {
        base.Init(newCell, newFaction, newImage);
    }

    private void CreateCellPath(int flipper)
    {
        // Celda de origen
        int currentX = currentCellParent.BoardPosition.x;
        int currentY = currentCellParent.BoardPosition.y;

        // Izquierda
        MatchesState(currentX - 2, currentY + (1 * flipper));

        // Arriba Izquierda
        MatchesState(currentX - 1, currentY + (2 * flipper));

        // Arriba derecha
        MatchesState(currentX + 1, currentY + (2 * flipper));

        // Derecha
        MatchesState(currentX + 2, currentY + (1 * flipper));
    }

    protected override void CheckPath()
    {
        // Dibujamos la mitad Arriba
        CreateCellPath(1);

        // Dibujamos la mitad Abajo
        CreateCellPath(-1);

        _gameManager.cellManager.ShowCells();
    }

    private void MatchesState(int targetX, int targetY)
    {
        CellState cellState = CellState.None;
        cellState = _gameManager.cellManager.ValidateCell(targetX, targetY, faction);

        // si la celda no tiene un amigo y no es el final del tablero
        if (cellState != CellState.Friendly && cellState != CellState.OutOfBounds)
            _gameManager.cellManager.lightedCells.Add(_gameManager.cellManager.board._cells[targetX, targetY]);
    }
}
