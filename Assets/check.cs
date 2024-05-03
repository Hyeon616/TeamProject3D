using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour
{
    public enum ItemType
    {
       BulleUpdate,
        // �ٸ� ������ ���� �߰� ����
    }
    public ItemType itemType; // ������ ����
    private void OnCollisionEnter(Collision collision)
    {

        GetComponent<Rigidbody>().isKinematic = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� �浹�� ���

            // ������ ������ ���� �ٸ� �۾� ����
            switch (itemType)
            {
                case ItemType.BulleUpdate:
                    // �÷��̾��� ���ݷ� ȸ��
                    collision.gameObject.GetComponent<PlayerController>().bulletUp(20); // ����: �÷��̾� ü���� 20 ȸ���ϴ� �Լ� ȣ��
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
