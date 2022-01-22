using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : BasePiece
{
    public override void Init(Cell newCell, Factions newFaction, Sprite newImage)
    {
        base.Init(newCell, newFaction, newImage);

        //Solo mueve en diagonal
        steps = new Vector3Int(0,0,7);

    }
}
