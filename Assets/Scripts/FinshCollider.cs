using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinshCollider : MonoBehaviour
{
    public bool playerIn;
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Player"))
        {       
            playerIn = true;
        }
    }
    
}
