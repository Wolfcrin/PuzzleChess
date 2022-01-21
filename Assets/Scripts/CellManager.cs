using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public Board board = null;

    private List<Cell> _lightedCells = new List<Cell>();

    private void Awake()
    {
        board = GameObject.FindObjectOfType<Board>();
    }

    public CellState ValidateCell(int targetX, int targetY, int faction)
    {
        // Para no salir de los bordes del tablero
        if (targetX < 0 || targetX > 7)
            return CellState.OutOfBounds;

        if (targetY < 0 || targetY > 7)
            return CellState.OutOfBounds;

        // Obtenemos la celda de la posicion X,Y
        Cell targetCell = board._cells[targetX, targetY];

        // Si la celda contiene una pieza
        if (targetCell.currentPiece != null)
        {
            // Si es una facción aliada
            if (faction == targetCell.currentPiece.faction)
                return CellState.Friendly;

            // Si es una facción enemiga
            if (faction != targetCell.currentPiece.faction)
                return CellState.Enemy;
        }

        return CellState.Empty;
    }

    public void CreateCellPath(
        Vector2Int cellPosition, // posicion inicial de la celda
        int faction,    // la faccion de la pieza
        int xDirection, // la direccion en X a donde va a buscar
        int yDirection, // la direccion en Y en donde va a buscar
        int movement)    // la cantidad de movimientos
    {
        // La posicion actual de la celda
        int currentX = cellPosition.x;
        int currentY = cellPosition.y;

        // Check each cell
        for (int i = 1; i <= movement; i++)
        {
            currentX += xDirection;
            currentY += yDirection;

            // Obtenemos el estado de la celda
            CellState cellState = CellState.None;
            cellState = ValidateCell(currentX, currentY, faction);

            //  SI es un enemigo en la celda terminamos el for de esta linea
            if (cellState == CellState.Enemy)
            {
                _lightedCells.Add(board._cells[currentX, currentY]);
                break;
            }

            // Si la celda no esta libre es decir hay un aliado terminamos
            if (cellState != CellState.Empty)
                break;

            // Agregamos la celda 
            _lightedCells.Add(board._cells[currentX, currentY]);
        }

        ShowCells();
    }

    protected void ShowCells()
    {
        foreach (Cell cell in _lightedCells)
            cell.ShowOutline();
    }

    protected void ClearCells()
    {
        foreach (Cell cell in _lightedCells)
            cell.HideOutline();

        _lightedCells.Clear();
    }
}
