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
        // �ٸ� ������ ���� �߰� ����
    }
    public ItemType itemType; // ������ ����
    private void OnTriggerEnter(Collider other)
    {
       // GetComponent<Rigidbody>().isKinematic = true;
        if (other.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� �浹�� ���

            // ������ ������ ���� �ٸ� �۾� ����
            switch (itemType)
            {
                case ItemType.BulleUpdate:
                    // �÷��̾��� ���ݷ� ȸ��
                    other.gameObject.GetComponent<PlayerController>().bulletUp(20); // ����: �÷��̾� ü���� 20 ȸ���ϴ� �Լ� ȣ��
                    break;
                    /*case ItemType.SpeedBoost:
                        // �÷��̾��� �̵� �ӵ��� ����
                        collision.gameObject.GetComponent<PlayerController>().ApplySpeedBoost(2.0f); // ����: �÷��̾� �̵� �ӵ��� 2�� ������Ű�� �Լ� ȣ��
                        break;
                        // �ٸ� ������ ������ ���� �۾� �߰� ����*/
            }

            // �������� ȸ���ϰų� ��Ȱ��ȭ
            gameObject.SetActive(false); // Ȥ�� ������ ȸ�� ���� �۾� ����
        }
    }

}
