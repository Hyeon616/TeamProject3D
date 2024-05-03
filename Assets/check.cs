using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour
{
    public enum ItemType
    {
       BulleUpdate,
        // 다른 아이템 종류 추가 가능
    }
    public ItemType itemType; // 아이템 종류
    private void OnCollisionEnter(Collision collision)
    {

        GetComponent<Rigidbody>().isKinematic = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌한 경우

            // 아이템 종류에 따라 다른 작업 수행
            switch (itemType)
            {
                case ItemType.BulleUpdate:
                    // 플레이어의 공격력 회복
                    collision.gameObject.GetComponent<PlayerController>().bulletUp(20); // 예시: 플레이어 체력을 20 회복하는 함수 호출
                    break;
                /*case ItemType.SpeedBoost:
                    // 플레이어의 이동 속도를 증가
                    collision.gameObject.GetComponent<PlayerController>().ApplySpeedBoost(2.0f); // 예시: 플레이어 이동 속도를 2배 증가시키는 함수 호출
                    break;
                    // 다른 아이템 종류에 따른 작업 추가 가능*/
            }

            // 아이템을 회수하거나 비활성화
            gameObject.SetActive(false); // 혹은 아이템 회수 등의 작업 수행
        }
    }

}
