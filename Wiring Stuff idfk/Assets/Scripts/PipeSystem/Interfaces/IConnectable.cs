using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnectable
{
    void Receive(Signal s);
}
