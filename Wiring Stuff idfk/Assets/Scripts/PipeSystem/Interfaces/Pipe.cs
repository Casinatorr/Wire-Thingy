using System;
using UnityEngine;
using System.Linq;

public abstract class Pipe:Connectable, IConnectable
{
    public void Pass(Signal s)
    {
        foreach(Connectable c in connections.Values.ToList())
        {
            if (c.lastSignalID != s.id)
            {
                c.ReceiveSignal(s);
            }
        }
    }

    public void Receive(Signal s)
    {
        Pass(s);
    }
}
