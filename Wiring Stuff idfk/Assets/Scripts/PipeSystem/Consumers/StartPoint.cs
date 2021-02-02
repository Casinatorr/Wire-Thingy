using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint:Consumer
{
    public override List<Vector3Int> connectSpaces => new List<Vector3Int>()
    {
        new Vector3Int(0, 0, 1),
        new Vector3Int(0, 0, -1),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
    };

    public override List<Vector3Int> occupiedSpaces => new List<Vector3Int>()
    {
        Vector3Int.zero
    };

    public override void Activate()
    {
        Send(new Signal("FUCKING SICC", 0));
    }

    public override void Receive(Signal s)
    {
        //Pass
    }

    public StartPoint(Transform t)
    {
        transform = t;
        connection = this;
    }
}
