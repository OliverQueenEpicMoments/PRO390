using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthBuff", menuName = "ScriptableObjects/HealthBuff")]
public class HealthBuff : ItemStatEffect {
    [SerializeField] private float Amount;

    public override void Apply(GameObject target) {
        if (target.CompareTag("Player")) target.GetComponent<Health>().AddMaxHealth(Amount);
    }
}