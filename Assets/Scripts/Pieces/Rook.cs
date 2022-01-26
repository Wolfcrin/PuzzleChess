using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : BasePiece
{
    [HideInInspector]
    public Cell  castleTriggerCell = null;
    public Cell castleCell = null;

    public override void Init(Cell newCell, Factions newFaction, Sprite newImage)
    {
        base.Init(newCell, newFaction, newImage);

        //Solo mueve en horizontal
        steps = new Vector3Int(7,7,0);
    }

    //TODO: Enroque
    // Despues de posicionar la torre guardarmos la posicion 
    // para los enroques 
    public override void Place(Cell newCell)
    {        
        base.Place(newCell);
                
        //celda donde tiene que entrar el rey para el enroque
        int triggerOffset = currentCellParent.BoardPosition.x < 4 ? 2 : -1;
        castleTriggerCell = SaveCellWithOffset(triggerOffset);

        // Celda luego del enroque
        int castleOffset = currentCellParent.BoardPosition.x < 4 ? 3 : -2;
        castleCell = SaveCellWithOffset(castleOffset);
    }

    //Enroque
    public void Castle()
    {
        targetCell = castleCell;

        Move();
    }

    private Cell SaveCellWithOffset(int offset)
    {
        Vector2Int newPosition = currentCellParent.BoardPosition;
        newPosition.x += offset;

        return _gameManager.board._cells[newPosition.x, newPosition.y];
    }

}
