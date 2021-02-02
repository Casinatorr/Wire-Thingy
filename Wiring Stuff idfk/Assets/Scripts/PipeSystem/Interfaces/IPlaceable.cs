using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPlaceable
{
    void Create();

    void Spawn(Type t);

    List<Vector3Int> occupiedSpaces {
        get;
    }

}
