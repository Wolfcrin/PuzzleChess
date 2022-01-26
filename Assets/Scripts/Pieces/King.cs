using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : BasePiece
{
    private Rook _leftRook  = null;
    private Rook _rightRook = null;

    public override void Init(Cell newCell, Factions newFaction, Sprite newImage)
    {
        base.Init(newCell, newFaction, newImage);

        //Solo mueve en diagonal
        steps = new Vector3Int(1, 1, 1);

    }

    //TODO enroque

    //para el enroque necesito la posicion de las dos torres
    // y la nueva posicion del rey al hacer enroque
    // hay dos tipos de enroque el largo y el corto
    // Segun la wiki
    //El rey nunca se movió.
    //La torre a usar en el enroque nunca fue movida.
    //El rey no está en jaque.
    //Ninguno de las celdas por los que el rey pasará o quedará, está bajo ataque.4​
    //Las celdas entre el rey y la torre estén desocupados.
    //El rey no termina en jaque (válido para cualquier movimiento legal).

    protected override void CheckPath()
    {      
        //Obtenemos la posicion de las dos Torres (Musica epica)
        _leftRook = GetRook(-1, 4);

        _rightRook = GetRook(1, 3);

        base.CheckPath();
    }

    protected override void Move()
    {
        //movimiento Base
        base.Move();

        // Castling left
        if (CanCastle(_leftRook))
            _leftRook.Castle();

        // Castling Right
        if (CanCastle(_rightRook))
            _rightRook.Castle();


    }

    private bool CanCastle(Rook rook)
    {
        //si hay una torre
        if (rook == null)
            return false;

        //si la celda donde esta el rey es distinta la que se necesita para 
        // el enroque
        if (rook.castleTriggerCell != currentCellParent)
            return false;

        //Controlamos que las torres sean de la misma faction y nunca movieron
        if (rook.faction != faction || !rook.isMyFirstMove)
            return false;

        return true;
    }

    private Rook GetRook(int direction, int count)
    {
        // EL rey ya movio?
        if (!isMyFirstMove)
            return null;

        // current cell
        int currentX = currentCellParent.BoardPosition.x;
        int currentY = currentCellParent.BoardPosition.y;

        // Recorremos las celdas entre la nuestra y la torre
        for (int i = 1; i < count; i++)
        {
            int offsetX = currentX + (i * direction);
            CellState cellState = _gameManager.cellManager.ValidateCell(offsetX, currentY, faction);

            // validamos que las celdas entre medios esten vacias
            if (cellState != CellState.Empty)
                return null;
        }

        // Obtenemos la celda de la torre
        Cell rookCell = _gameManager.board._cells[currentX + (count * direction), currentY];
        Rook rook = null;

        // Comprobamos que la pieza en la celda sea realmente una torre
        if (rookCell.currentPiece is Rook)
            rook = (Rook)rookCell.currentPiece;

        // Agregamos las celdas para mostrar
        if (rook != null)
            _gameManager.cellManager.lightedCells.Add(rook.castleTriggerCell);

        return rook;
    }

    //TODO: cuando el rey muere perdemos
    public override void Kill()
    {
        base.Kill();

        //game over
    }
}
