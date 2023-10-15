using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCoroutine
{
    public TowerSetupCoroutine towerAwaitSetupCoroutine;

    public TowerCoroutine(Tower tower)
    {
        towerAwaitSetupCoroutine = new TowerSetupCoroutine(tower);
    }
}
