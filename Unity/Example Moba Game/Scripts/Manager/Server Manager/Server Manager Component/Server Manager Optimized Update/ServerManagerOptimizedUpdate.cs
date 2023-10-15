using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManagerOptimizedUpdate
{
    public static Queue<Minion> optimizedMinionQueue = new Queue<Minion>();
    public static Queue<Tower> optimizedTowerQueue = new Queue<Tower>();

    private static int count = 5;

    public static void OptimizedUpdate()
    {
        OnMinionOptimizedUpdate();
        OnTowerOptimizedUpdate();
    }

    public static void OnMinionOptimizedUpdate()
    {
        for (int i = 0; i < count; i++)
        {
            Minion currentMinion;
            if (!optimizedMinionQueue.TryDequeue(out currentMinion)) break;
            if (currentMinion == null) continue;
            currentMinion.OnOptimizedUpdate();
            optimizedMinionQueue.Enqueue(currentMinion);
        }
    }

    public static void OnTowerOptimizedUpdate()
    {
        for (int i = 0; i < count; i++)
        {
            Tower currentTower;
            if (!optimizedTowerQueue.TryDequeue(out currentTower)) break;
            currentTower.OnOptimizedUpdate();
        }
    }
}
