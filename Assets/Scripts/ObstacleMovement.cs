using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ObstacleMovement : MonoBehaviour
{
    
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    // todo remove from inspector later
    [Range(0, 1)] [SerializeField] float movementFactor; // 0 for not moved, 1 for fully moved.

    Vector3 startingPos;

    // Use this for initialization
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        //Mathf.Epsilon is the smallest floating point number
        float cycles = Time.time / period;
        
        const float tau = Mathf.PI * 2; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);
        
        movementFactor = rawSinWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor; //Grows continually from 0
        transform.position = startingPos + offset;
    }
}
