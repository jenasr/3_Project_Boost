using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 3f;
    Rigidbody rigidBody;
    AudioSource thrusterAudio;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); //Allows to access rockets rigibody
        thrusterAudio = GetComponent<AudioSource>();   //Allows to access rockets Audio
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly": 
                print("OK");
                break;
            default: 
                print("dead");
                break;
        }
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);//move object in natural direction
            //Vector 3 refers to a group(position/rotation/scale) of 3 floating point
            //numbers bundled together to make an item
            if (!thrusterAudio.isPlaying)//if the audio is not already playing
            {
                thrusterAudio.Play();
            }
        }
        else
        {
            thrusterAudio.Stop();
        }
    }
    private void Rotate()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * rotationThisFrame);//Rotates Counter clockwise
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);//Rotates Clockwise
        }
        rigidBody.freezeRotation = false; //resume physics control

    }
}
