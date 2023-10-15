using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegenerationCoroutine
{
    private Player player;

    public PlayerRegenerationCoroutine(Player player) => this.player = player;

    public IEnumerator Coroutine()
    {
        while (true)
        {
            RegenerateHealth();
            RegenerateMana();
            /*
             * Currently netcode doesn't support nested variables to be syncronized only.
             * We will wait for the unity to bring nested variable syncronization support on Unity.
             * If that will not happen, we will implement lots of components to the player.
             */
            if(player.playerData.Value != null) player.playerData.SetDirty(true); //That's why this line is really bad in optimization but we have no option.
            yield return new WaitForSeconds(1f);
        }
    }

    public void RegenerateHealth() => player.playerData.Value.playerHealthData.RegenerateHealth();

    public void RegenerateMana() => player.playerData.Value.playerManaData.RegenerateMana();
}
