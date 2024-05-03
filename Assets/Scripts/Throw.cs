using System.Runtime.InteropServices;
using UnityEngine;

public class Throw : MonoBehaviour
{
    private Rigidbody body;

    public ParticleSystem particle;
    
    public float throwForce = 10.0f;
    public float throwSpeed = 10.0f;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        particle = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        Invoke("Explosion", 3);
        Debug.Log("ÆøÅº ÅÍÁü");
    }

    private void FixedUpdate()
    {
        Vector3 throwDirection = transform.forward;
        body.velocity = throwDirection * throwSpeed;
    }

    void Explosion()
    {
        //particle.Play();
        body.velocity = Vector3.zero;
        Destroy(gameObject, particle.main.duration);
    }
}
