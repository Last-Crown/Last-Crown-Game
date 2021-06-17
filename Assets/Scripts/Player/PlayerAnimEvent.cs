using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    private GameObject FrontObject;

    public void HitByHand()
    {
        FrontObject?.GetComponent<HarvestableResource>().Harvest(0.3f, eEquipment.None);
    }
    public void HitByAxe(eEquipment tool)
    {
        FrontObject?.GetComponent<HarvestableResource>().Harvest(1, tool);
    }
    public void HitByPickAxe(eEquipment tool)
    {
        FrontObject?.GetComponent<HarvestableResource>().Harvest(1, tool);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree") || collision.CompareTag("Rock"))
        {
            FrontObject ??= collision.gameObject;   // FrontObject가 null이면 대입
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FrontObject = null;
    }
}
