using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class Grenade : MonoBehaviour
{
   
    private Rigidbody body;
    public GameObject effectPrefap;
    
    

    public float throwForce = 2.0f;
    public float liftForce = 2.0f; 

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        
        StartCoroutine(Explosion());
    }

    private void FixedUpdate()
    {
        Vector3 throwDirection = transform.forward * throwForce;
        body.AddForce(throwDirection, ForceMode.Impulse);
        body.AddForce(Vector3.down * liftForce, ForceMode.Acceleration);
    }

    IEnumerator Explosion()
    {
        body.velocity = Vector3.zero;
        yield return new WaitForSeconds(3);

        GameObject effect = Instantiate(effectPrefap, transform.position, Quaternion.identity);
        ParticleSystem particleEffect = effect.GetComponent<ParticleSystem>();
        particleEffect.Play();

        Destroy(gameObject);
        Destroy(effect, particleEffect.main.duration);
        Debug.Log("폭탄 터짐");
    }
}
