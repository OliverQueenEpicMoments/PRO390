using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerBuff", menuName = "ScriptableObjects/PowerBuff")]
public class PowerBuff : ItemStatEffect {
    [SerializeField] private float Amount;

    public override void Apply(GameObject target) {
        Debug.Log(target.GetComponent<ComboCharacter>().Power);
        if (target.CompareTag("Player")) target.GetComponent<ComboCharacter>().Power += Amount;
        Debug.Log(target.GetComponent<ComboCharacter>().Power);
    }
}