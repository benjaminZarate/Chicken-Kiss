using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    [SerializeField] BoxCollider coll;

    public void ActivateCollider() {
        coll.enabled = true;
    }

    public void DeactvateCollider() {
        coll.enabled = false;
    }
}
