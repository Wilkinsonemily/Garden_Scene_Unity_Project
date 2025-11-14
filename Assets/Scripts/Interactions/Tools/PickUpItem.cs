using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController playerCtrl = other.GetComponent<playerController>();
            if (playerCtrl != null)
            {
                playerCtrl.CollectCrop();
            }

            Destroy(gameObject);
        }
    }
}
