using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint:Consumer
{
    public override List<Vector3Int> occupiedSpaces => new List<Vector3Int>()
    {
        Vector3Int.zero
    };

    public override List<Vector3Int> connectSpaces => new List<Vector3Int>()
    {
        new Vector3Int(0, 0, 1),
        new Vector3Int(0, 0, -1),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
    };

    public override void Activate()
    {
        //Pass
    }

    public override void Receive(Signal s)
    {
        Debug.Log($"Received: {s.msg}");
    }

    public EndPoint(Transform t)
    {
        transform = t;
        connection = this;
    }
}
