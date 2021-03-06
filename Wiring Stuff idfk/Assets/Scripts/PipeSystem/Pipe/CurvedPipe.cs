﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedPipe : Pipe
{
    public override List<Vector3Int> connectSpaces => new List<Vector3Int>()
    {
        new Vector3Int(0, 0, -1),
        new Vector3Int(-1, 0, 0)
    };

    public CurvedPipe(Transform t)
    {
        transform = t;
        connection = this;
    }
}
