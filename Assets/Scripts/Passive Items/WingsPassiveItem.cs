using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        playerStats.CurrentMovementSpeed *= 1 + PassiveItemData.Multipler / 100f;
    }
}
