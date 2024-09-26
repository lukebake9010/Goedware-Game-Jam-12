using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAI : EnemyAI
{

    new void Awake() 
    {
        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(GoToPlayer());
    }


    private IEnumerator GoToPlayer()
    {
        PlayerManager playerManager = PlayerManager.Instance;
        while (true)
        {
            if (playerManager != null)
            {
                SetDestination(PlayerManager.Instance.GetPosition());
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
