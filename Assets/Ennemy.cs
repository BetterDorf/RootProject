using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float health;
    [SerializeField] float damage;

    private Rigidbody2D myBody;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision with player");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Hunger>().removeEnergy(this.damage);
        }
    }
}
