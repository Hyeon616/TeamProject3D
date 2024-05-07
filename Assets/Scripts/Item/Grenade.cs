using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    private Rigidbody body;
    public GameObject effectPrefap;
    private SphereCollider sphereCollider;


    public float throwForce = 2.0f;
    public float liftForce = 2.0f;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();

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
        sphereCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
        Destroy(effect, particleEffect.main.duration);

        Debug.Log("폭탄 터짐");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            //Destroy(other.gameObject);
            body.velocity = Vector3.zero;
            other.gameObject.SetActive(false);
            Debug.Log("몬스터와 충돌됨");
        }
    }
}
