using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); //Allosws to access rockets rigibody
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);//move object in natural direction
            //Vector 3 refers to a group(position/rotation/scale) of 3 floating point
            //numbers bundled together to make an item
        }
        if (Input.GetKey(KeyCode.A))
        {
            print("A pressed");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("D pressed");
        }
    }
}
