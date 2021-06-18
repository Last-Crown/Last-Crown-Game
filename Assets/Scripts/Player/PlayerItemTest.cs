using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            collision.GetComponent<ResourceItem>().Collecte(transform);
        }
    }
}
