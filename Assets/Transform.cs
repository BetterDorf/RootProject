using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform : MonoBehaviour
{
    [SerializeField] int timerValue;
    [SerializeField] GameObject food;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitForFood());
    }

    IEnumerator waitForFood()
    {
        yield return new WaitForSeconds(timerValue);
        Instantiate(food, transform.position, Quaternion.identity, null);
        Destroy(this.gameObject);
    }
}
