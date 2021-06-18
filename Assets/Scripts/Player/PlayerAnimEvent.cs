using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    private GameObject frontObject;

    public void Hit(eEquipment tool)
    {
        PlayerAction playerAction = transform.GetComponent<PlayerAction>();
        Equipment equipment = playerAction.myEquipmentsDict[playerAction.whatsInHand];
        float damage;

        if (equipment == null)
        {
            damage = 0.3f;
        }
        else
        {
            damage = equipment.damage;
        }

        if (GameObject.FindWithTag("Server").GetComponent<ServerInitializer>().playerName == gameObject.name)
        {
            frontObject?.GetComponent<HarvestableResource>().Harvest(damage, eEquipment.None);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree") || collision.CompareTag("Rock"))
        {
            frontObject ??= collision.gameObject;   // frontObject가 null이면 대입
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        frontObject = null;
    }
}
