using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainthrust = 100f;
    [SerializeField] float rotationthrust = 1f;
    [SerializeField] AudioClip mainengine;

    [SerializeField] ParticleSystem mainengineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    
    Rigidbody rb;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
        
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Rotateleft();
        }
        
        else if(Input.GetKey(KeyCode.D))
        {
            Rotateright();
        }
        else
        {
            StopRotate();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainthrust * Time.deltaTime);
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainengine);    
        }
        if(!mainengineParticles.isPlaying)
        {
            mainengineParticles.Play();
        }
    }

    void StopThrusting()
    {
        mainengineParticles.Stop();
        audioSource.Stop();
    }

    void Rotateleft()
    {
        ApplyRotation(rotationthrust);
        if(!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    void Rotateright()
    {
        ApplyRotation(-rotationthrust);
        if(!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    void StopRotate()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    void ApplyRotation(float rotationthisframe)
    {
        rb.freezeRotation = true; 
        transform.Rotate(Vector3.forward * rotationthisframe * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
