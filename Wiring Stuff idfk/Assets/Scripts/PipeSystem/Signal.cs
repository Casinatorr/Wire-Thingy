using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Signal
{
    public int id;
    public string msg;
    public int signal;

    public Signal(string msg, int signal)
    {
        this.id = Generator.GenerateRandomSignalID();
        this.msg = msg;
        this.signal = signal;
    }
}
public static class Generator
{
    public static int GenerateRandomSignalID()
    {
        return Random.Range(int.MinValue, int.MaxValue);
    }
}
