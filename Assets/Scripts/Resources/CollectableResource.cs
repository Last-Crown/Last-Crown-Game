using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eColResource    // collectable resource
{
    None, Wood, Stone
}

public class CollectableResource : MonoBehaviour
{
    protected eColResource Kinds { get; set; }
    protected eEquipment MatchedTool { get; set; }

    public virtual void Collecte()
    {

    }
}
