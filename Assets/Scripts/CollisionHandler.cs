using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashaudio;
    [SerializeField] AudioClip successaudio;
    [SerializeField] float levelloadDelay = 2f;
    
    
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool IsTransitioning = false;
    bool collisionDisabled = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;   //togle collision
        }
        
    }

    void OnCollisionEnter(Collision other) 
    {   
        if(IsTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("hi");
                break;

            case "Finish":
                SuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;    
        }
    }

    void SuccessSequence()
    {   
        IsTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successaudio);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelloadDelay);
    }
    void StartCrashSequence()
    {   
        IsTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashaudio);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelloadDelay);
    }

    void ReloadLevel()
    {
        int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneIndex);
    }

    void LoadNextLevel()
    {
        int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextsceneIndex = currentsceneIndex + 1;
        if (nextsceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextsceneIndex = 0;
        }
        SceneManager.LoadScene(nextsceneIndex);
    }
}
