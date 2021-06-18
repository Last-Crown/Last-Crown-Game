using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodItem : ResourceItem
{
    private void Awake()
    {        
        kinds = eResource.Tree;
    }
}
