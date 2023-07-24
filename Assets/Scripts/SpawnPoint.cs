using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool collides;
    [SerializeField]
    private int count;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("SpawnPoint") && !collision.gameObject.CompareTag("Finish"))
        {
            if (count <= 0)
                collides = true;
            count++;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("SpawnPoint") && !collision.gameObject.CompareTag("Finish"))
        {
            count--;
        }
        if (!collision.gameObject.CompareTag("SpawnPoint") && !collision.gameObject.CompareTag("Finish") && count <= 0)
        {
            collides = false;
        }
    }

}
