using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItem : MonoBehaviour
{
    public int count;

    protected eResource kinds { get; set; }

    public virtual void Collecte(Transform player)
    {
        switch(kinds)
        {
            case eResource.Tree:
                player.GetComponent<PlayerInfo>().WoodCount += count;
                Destroy(gameObject);
                break;
            case eResource.Rock:
                player.GetComponent<PlayerInfo>().StoneCount += count;
                Destroy(gameObject);
                break;
        }
    }
}
