using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enmy : MonoBehaviour
{
    bool isGrounded;
    [SerializeField]
    public Transform groundCheck;          //ground check gameobject
    [SerializeField]
    public LayerMask groundLayer;          // layer mask to use as ground
    public float speed = 0.8f;
    public float range = 3;

    float startingY;
    int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime * dir);
        if (transform.position.y < startingY || transform.position.y > startingY + range)
            dir *= -1;
    }
}
