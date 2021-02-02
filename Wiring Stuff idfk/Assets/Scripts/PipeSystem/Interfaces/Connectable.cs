using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Connectable
{

    public Dictionary<Vector3Int, Connectable> connections = new Dictionary<Vector3Int, Connectable>();
    public IConnectable connection;
    public int lastSignalID;

    public abstract List<Vector3Int> connectSpaces {
        get;
    }

    public Transform transform;

    public void Connect()
    {
        Vector3Int gridPosition = toVector3Int(transform.position);
        foreach (Vector3Int v in TranslateConnectPositions())
        {
            if (!Placing.grid.ContainsKey(v))
                continue;
            Connectable c = null;
            IPlaceable current = Placing.grid[v];
            if (current is IPipeSystemComponent)
                c = ((IPipeSystemComponent) current).connectable;

            if (c == null)
                continue;

            if (c.TranslateConnectPositions().Contains(gridPosition))
            {
                ConnectTo(v, c);
                c.ConnectTo(gridPosition, this);
            }
        }
    }

    public void ConnectTo(Vector3Int pos, Connectable c)
    {
        if (c != this)
            connections.Add(pos, c);
    }


    public List<Vector3Int> TranslateConnectPositions()
    {
        List<Vector3Int> l = new List<Vector3Int>();
        Vector3Int gridPosition = toVector3Int(transform.position);
        foreach (Vector3Int v in connectSpaces)
        {
            Vector3 vec = gridPosition + transform.TransformVector(v);
            l.Add(toVector3Int(vec));
        }
        return l;
    }

    private Vector3Int toVector3Int(Vector3 v)
    {
        try
        {
            Vector3Int vec = new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
            return vec;
        }
        catch
        {
            throw new System.Exception("Tried to convert Vector3 to Vector3Int, but Vector3 succ so error");
        }
    }

    public void ReceiveSignal(Signal s)
    {
        lastSignalID = s.id;
        connection.Receive(s);
    }
}
