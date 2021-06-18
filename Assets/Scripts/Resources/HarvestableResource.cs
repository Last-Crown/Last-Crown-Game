using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public enum eResource    // harvestable resource
{
    None, Tree, Rock
}

public class HarvestableResource : MonoBehaviour
{
    public float hitLimit;

    protected eResource Kinds { get; set; }
    protected eEquipment MatchedTool { get; set; }

    public virtual void Harvest(float damage, eEquipment tool)
    {
        if (hitLimit > 0 && (tool == MatchedTool || tool == eEquipment.None))
        {
            JSONNode json = JSONNode.Parse("{ name: " + gameObject.name + " ,type: " + gameObject.tag + " ,health: " + hitLimit + " ,value:" + -damage + " }");

            GameObject.FindWithTag("Server")?.GetComponent<ServerInitializer>().EmitUpdateHealth(json.ToString());
        }
    }

    public virtual void UpdateHitLimit(float _hitLimit)
    {
        hitLimit = _hitLimit;

        if(hitLimit < 0)
        {
            Destroy(gameObject);
        }
    }
}
