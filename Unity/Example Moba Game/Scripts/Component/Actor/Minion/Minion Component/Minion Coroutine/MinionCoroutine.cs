using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCoroutine
{
    public MinionSetupCoroutine minionAwaitSetupCoroutine;

    public MinionCoroutine(Minion minion)
    {
        minionAwaitSetupCoroutine = new MinionSetupCoroutine(minion);
    }
}
