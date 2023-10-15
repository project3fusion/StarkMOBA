using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCallback
{
    public virtual void InitializeCallback() => throw new NotImplementedException();

    public virtual void Callback(int id) => throw new NotImplementedException();
}
