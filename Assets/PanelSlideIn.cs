using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSlideIn : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float timeToReach;
    private Vector2 startPos;
    private Vector2 goalPos;

    private float progress = 0.0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        goalPos = transform.position;
        transform.position += Vector3.up * distance;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPos, goalPos, progress / timeToReach);
        progress += Time.deltaTime;
    }
}
