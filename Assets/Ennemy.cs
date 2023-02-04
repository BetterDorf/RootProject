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
    private Vector3 dir = Vector3.left;

    private Rigidbody2D myBody;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);

        if (transform.position.x <= leftLimit)
        {
            dir = Vector3.right;
        }
        else if (transform.position.x >= rightLimit)
        {
            dir = Vector3.left;
        }
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
