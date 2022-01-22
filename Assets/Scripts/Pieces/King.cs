using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : BasePiece
{
    public override void Init(Cell newCell, Factions newFaction, Sprite newImage)
    {
        base.Init(newCell, newFaction, newImage);

        //Solo mueve en diagonal
        steps = new Vector3Int(1, 1, 1);

    }

    //TODO enroque

    //TODO: cuando el rey muere perdemos
    public override void Kill()
    {
        base.Kill();

        //game over
    }
}
