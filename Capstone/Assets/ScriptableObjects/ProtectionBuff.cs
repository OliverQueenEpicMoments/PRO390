using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProtectionBuff", menuName = "ScriptableObjects/ProtectionBuff")]
public class ProtectionBuff : ItemStatEffect {
    [SerializeField] private float Amount;

    public override void Apply(GameObject target) {
        if (target.CompareTag("Player")) target.GetComponent<Health>().AddProtections(Amount);
    }
}