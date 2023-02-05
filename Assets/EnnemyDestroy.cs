using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDestroy : MonoBehaviour
{
    [SerializeField] private float bounceVel;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //Destroy(obj: transform.parent.gameObject);
            transform.GetComponentInParent<Ennemy>().onDeath();

            collider.GetComponent<Rigidbody2D>().velocity = new Vector2(collider.GetComponent<Rigidbody2D>().velocity.x,
                bounceVel);
        }
    }
}
