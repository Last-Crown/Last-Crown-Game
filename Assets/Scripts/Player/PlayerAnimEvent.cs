using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    private GameObject FrontObject;

    public void HitByAxe()
    {
        FrontObject?.GetComponent<HarvestableResource>().Harvest(1, eEquipment.Axe);
    }
    public void HitByPickAxe()
    {
        FrontObject?.GetComponent<HarvestableResource>().Harvest(1, eEquipment.PickAxe);
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
