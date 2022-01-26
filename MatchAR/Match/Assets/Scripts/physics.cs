using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physics : MonoBehaviour
{

    public Rigidbody body;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = body.velocity;
        float maxAcceleration = 0;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        float desiredVelocity = 0;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity, maxSpeedChange);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity, maxSpeedChange);
        body.velocity = velocity;
    }
}
