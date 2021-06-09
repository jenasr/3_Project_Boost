using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 3f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State {Alive, Dead, Transcending};
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); //Allows to access rockets rigibody
        audioSource = GetComponent<AudioSource>();   //Allows to access rockets Audio
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotationInput();
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly": 
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dead;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", 1f);
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", 1f);//Call functions with name 
        //after certain time period, in this case 1 second
    }

    private void LoadFirstLevel()
    {
        print("dead");
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        print("Hit Finish");
        SceneManager.LoadScene(1);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);//move object in natural direction
                                                            //Vector 3 refers to a group(position/rotation/scale) of 3 floating point
                                                            //numbers bundled together to make an item
        if (!audioSource.isPlaying)//if the audio is not already playing
        {
            audioSource.PlayOneShot(mainEngine);
            mainEngineParticles.Play();
        }
       

    }

    private void RespondToRotationInput()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * rotationThisFrame);//Rotates Counter clockwise
        }
        else if (Input.GetKey(KeyCode.D) && state == State.Alive)
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);//Rotates Clockwise
        }
        rigidBody.freezeRotation = false; //resume physics control

    }
}
