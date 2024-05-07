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
        HealthUpdate,
    }
    public ItemType itemType; // ������ ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ������ ������ ���� �ٸ� �۾� ����
            switch (itemType)
            {
                case ItemType.BulleUpdate:
                    other.gameObject.GetComponent<PlayerController>().bulletUp(20); // �Ѿ�/ź�� up
                    break;
                case ItemType.HealthUpdate:
                    PlayerUIManager.Instance.hpUp();
                    break;

            }
            gameObject.SetActive(false); // ������ ȸ�� ���� �۾� ����
        }
    }

}
