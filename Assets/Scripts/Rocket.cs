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

    [SerializeField] float levelLoadDelay = 1f;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State {Alive, Dead, Transcending};
    State state = State.Alive;
    bool collisionsDisabled = false;
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
        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
        
    }
    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled; // toggle
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionsDisabled) { return; }
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
        Invoke("LoadCurrentLevel", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);//Call functions with name 
        //after certain time period, in this case 1 second
    }

    private void LoadCurrentLevel()
    {
        print("dead");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (SceneManager.sceneCountInBuildSettings == nextSceneIndex)
        {
           nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
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
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);//move object in natural direction
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
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            RotateManually(rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D) && state == State.Alive)
        {
            RotateManually(-rotationThisFrame);
        }
         

    }

    private void RotateManually(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true; //take manual control of rotation
        transform.Rotate(Vector3.forward * rotationThisFrame);//Rotates Counter clockwise
        rigidBody.freezeRotation = false;//resume physics control
    }
}
