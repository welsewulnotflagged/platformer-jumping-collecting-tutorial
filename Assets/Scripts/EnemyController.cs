using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public Transform startMarker;
    public Transform endMarker;
    private Animator anim; 

    private bool facingRight = true;

    public float speed = 3.0F;

    private float startTime;

    private float journeyLength;

    void Start()
    {
        startTime = Time.time; 
        anim = GetComponent<Animator>();
        anim.Play("Base Layer.EnemyWalk", 0, 0);
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
    }

    // Move to the target end position.
    void FixedUpdate()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, fractionOfJourney);

        if (distCovered >= journeyLength)
        {
            Transform tempMarker = startMarker;
            startMarker = endMarker;
            endMarker = tempMarker; 
            startTime = Time.time;
            Flip();
            journeyLength = Vector2.Distance(startMarker.position, endMarker.position);            
        }
    }
    void Flip()
    {
            facingRight = !facingRight;
            Vector2 Scaler = transform.localScale;
            Scaler.x = Scaler.x * -1;
            transform.localScale = Scaler;
    }
}
