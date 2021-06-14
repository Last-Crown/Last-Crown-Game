using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOnGround : HarvestableResource
{
    private void Awake()
    {
        HitLimit = 4;
        MatchedTool = eEquipment.PickAxe;
        Kinds = eHarResource.Rock;
    }
}
