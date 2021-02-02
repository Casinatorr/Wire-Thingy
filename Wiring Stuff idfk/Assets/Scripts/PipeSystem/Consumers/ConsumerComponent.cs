using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerComponent : MonoBehaviour, IPlaceable, IPipeSystemComponent
{
    public Consumer consumer;

    public List<Vector3Int> occupiedSpaces => consumer.occupiedSpaces;

    public Connectable connectable => consumer;

    public void Create()
    {
        consumer.Connect();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            consumer.Activate();
    }

    public void Spawn(Type t)
    {
        object[] args = new object[]
        {
            transform
        };
        consumer = (Consumer) Activator.CreateInstance(t, args);
    }

}
