using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPipe:Pipe
{
    public override List<Vector3Int> connectSpaces => new List<Vector3Int>()
    {
        Vector3Int.right,
        Vector3Int.left,
        new Vector3Int(0, 0, 1),
        new Vector3Int(0, 0, -1)
    };

    public CrossPipe(Transform t)
    {
        transform = t;
        connection = this;
    }
}
