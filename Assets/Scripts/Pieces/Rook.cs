using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : BasePiece
{
    public override void Init(Cell newCell, Factions newFaction, Sprite newImage)
    {
        base.Init(newCell, newFaction, newImage);

        //Solo mueve en horizontal
        steps = new Vector3Int(7,7,0);
    }

    //TODO: Enroque
}
