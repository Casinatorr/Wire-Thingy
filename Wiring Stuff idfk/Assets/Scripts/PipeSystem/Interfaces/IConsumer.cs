using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumer:IPlaceable
{
    int lastSignalID {
        get;
        set;
    }
}
