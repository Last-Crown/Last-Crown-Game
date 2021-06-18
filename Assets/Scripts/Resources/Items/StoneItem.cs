using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneItem : ResourceItem
{
    private void Awake()
    {
        kinds = eResource.Rock;
    }
}
