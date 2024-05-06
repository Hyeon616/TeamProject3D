using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit,1.5f))
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

    }
    public enum ItemType
    {
        BulleUpdate,
    }
    public ItemType itemType; // 아이템 종류
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 아이템 종류에 따라 다른 작업 수행
            switch (itemType)
            {
                case ItemType.BulleUpdate:
                    other.gameObject.GetComponent<PlayerController>().bulletUp(20); // 총알/탄약 up
                    break;
            }
            gameObject.SetActive(false); // 아이템 회수 등의 작업 수행
        }
    }

}
