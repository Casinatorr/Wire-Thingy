using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PipeComponent:MonoBehaviour, IPlaceable, IPipeSystemComponent
{
    public Pipe pipe;
    private bool created = false;
    public List<Vector3Int> occupiedSpaces => new List<Vector3Int>()
    {
        Vector3Int.zero
    };

    public Connectable connectable {
        get => pipe;
    }

    public void Create()
    {
        pipe.Connect();
        created = true;
    }

    public void Spawn(Type t)
    {
        object[] args = new object[]
        {
            transform
        };
        pipe = (Pipe) Activator.CreateInstance(t, args);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            pipe.TranslateConnectPositions();
        }
    }

    private void OnDrawGizmos()
    {
        if (pipe == null)
            return;
        if (created)
            return;
        foreach(Vector3Int v in pipe.TranslateConnectPositions())
        {
            Gizmos.DrawWireCube(v, Vector3.one);
            Gizmos.DrawWireCube(pipe.transform.position, Vector3.one);
        }
    }
}
