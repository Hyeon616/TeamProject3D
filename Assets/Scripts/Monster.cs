using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public float maxHp = 5f;
    public float hp;

    [SerializeField] private float attackDamage = 1.0f;

    BoxCollider boxCollider;

    private void Start()
    {
        hp = maxHp;
        boxCollider = GetComponent<BoxCollider>();

        boxCollider.enabled = false;
    }


    public void AttackStart()
    {
        boxCollider.enabled = true;
        PlayerUIManager.Instance.GetDamaged(1);
    }


    public void AttackEnd()
    {
        boxCollider.enabled = false;
    }

    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerController>().BasePlayerHp -= attackDamage;
        }
    }

}
