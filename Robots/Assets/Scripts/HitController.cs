using UnityEngine;
using System.Collections.Generic;

class HitController : MonoBehaviour
{
    uint lastAttackId = 0;

    public void Smash(uint attackId)
    {
        if (lastAttackId == attackId) return;

        lastAttackId = attackId;

        transform.gameObject.SendMessage("onSmash");
    }
}
