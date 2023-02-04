using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDestroy : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision with player on vulnerable area");
            Destroy(obj: transform.parent.gameObject);
        }
    }
}
