using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float health;
    [SerializeField] float damage;
    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject deadBoar;
    [SerializeField] private Vector2 offset;
    private Vector3 dir = Vector3.left;
    private bool isDead = false;

    private Rigidbody2D myBody;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        leftLimit += transform.position.x;
        rightLimit += transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            transform.Translate(dir * moveSpeed * Time.deltaTime);

            if (transform.position.x <= leftLimit)
            {
                dir = Vector3.right;
                spriteRenderer.flipX = true;
            }
            else if (transform.position.x >= rightLimit)
            {
                dir = Vector3.left;
                spriteRenderer.flipX = false;
            }
        }
    }

    public void onDeath()
    {
        isDead = true;
        Instantiate(deadBoar, transform.position + (Vector3)offset, Quaternion.identity, null);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (!isDead && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Hunger>().removeEnergy(this.damage);
        }
    }
}
