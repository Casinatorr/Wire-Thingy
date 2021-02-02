using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Consumer : Connectable, IConnectable
{
    public abstract void Activate();
    public abstract void Receive(Signal s);

    public abstract List<Vector3Int> occupiedSpaces {
        get;
    }

    public void Send(Signal s)
    {
        Debug.Log("Sicc");
        lastSignalID = s.id;
        foreach (Connectable c in connections.Values.ToList())
        {
            if (c.lastSignalID != s.id)
            {
                c.ReceiveSignal(s);
            }
        }
    }
}
