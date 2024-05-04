using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    public string terrainTag = "ItemSpawner"; // 지형 태그
    public float spawnInterval = 1.0f; // 아이템 생성 간격
    public int maxPoolSize = 15; // 최대 오브젝트 풀 크기
    public float returnDelay = 30f; // 반환되기까지의 지연 시간

    public GameObject[] itemPrefabs; // 아이템 프리팹 배열

    private GameObject[] terrainObjects; // 지형 오브젝트 배열
    private List<GameObject> objectPool = new List<GameObject>(); // 오브젝트 풀
    private List<GameObject> activeObjects = new List<GameObject>(); // 활성화된 오브젝트 리스트

    void Start()
    {
        // 해당 태그를 가진 오브젝트를 모두 GameObject 배열에 저장
        terrainObjects = GameObject.FindGameObjectsWithTag(terrainTag);

        // 오브젝트 풀 초기화
        InitializeObjectPool();

        // 일정 간격으로 SpawnItem 함수를 반복 실행
        InvokeRepeating("SpawnItem", 0f, spawnInterval);
    }

    // 아이템 생성 함수
    void SpawnItem()
    {
        // 아이템이 랜덤한 지형에 생성될 수 있도록 지형을 랜덤으로 선택
        GameObject randomTerrain = terrainObjects[Random.Range(0, terrainObjects.Length)];

        // 아이템이 생성될 위치를 랜덤으로 지정
        Vector3 spawnPoint = GetRandomPointOnTerrain(randomTerrain);

        // 아이템 프리팹 중 랜덤하게 선택
        GameObject itemPrefab = GetRandomItemPrefab();

        if (itemPrefab != null)
        {
            // 오브젝트 풀에서 오브젝트를 가져옴
            GameObject newItem = GetObjectFromPool();
            if (newItem == null)
            {
                // 오브젝트 풀이 비어있는 경우 새로운 오브젝트를 생성하여 추가
                Debug.LogWarning("오브젝트 풀 비어있다");
                newItem = Instantiate(itemPrefab);
                objectPool.Add(newItem);
            }

            // 아이템의 위치를 설정하고 활성화
            newItem.transform.position = spawnPoint;
            newItem.SetActive(true);

            // 활성화된 오브젝트 리스트에 추가
            activeObjects.Add(newItem);

            // Rigidbody의 Kinematic 속성을 비활성화하여 중력이 작용하도록 함
            Rigidbody rb = newItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            // 일정 시간 후에 오브젝트를 풀로 반환
            StartCoroutine(ReturnObjectToPool(newItem));
        }
        else
        {
            Debug.LogError("프리팹 없다");
        }
    }

    // 오브젝트를 풀로 반환하는 코루틴 함수
    IEnumerator ReturnObjectToPool(GameObject obj)
    {
        yield return new WaitForSeconds(returnDelay); // 반환 지연 시간 후에 실행

        // 오브젝트 비활성화
        obj.SetActive(false);

        // 오브젝트를 랜덤한 위치로 초기화
        Vector3 randomSpawnPoint = GetRandomPointOnTerrain(terrainObjects[Random.Range(0, terrainObjects.Length)]);
        obj.transform.position = randomSpawnPoint;

        // 오브젝트를 풀에 추가하고 활성화된 리스트에서 제거
        objectPool.Add(obj);
        activeObjects.Remove(obj);
    }

    // 지형 위의 랜덤한 점 반환 함수
    Vector3 GetRandomPointOnTerrain(GameObject terrainObject)
    {
        Bounds bounds = terrainObject.GetComponent<Renderer>().bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        float y = bounds.max.y + 15f;
        return new Vector3(x, y, z);
    }

    // 오브젝트 풀 초기화 함수
    private void InitializeObjectPool()
    {
        // 오브젝트 풀에 미리 아이템 오브젝트를 생성하여 추가
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject newItem = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
            objectPool.Add(newItem);
            newItem.SetActive(false); // 초기에는 비활성화 상태로 설정
        }
    }

    // 랜덤한 아이템 프리팹 반환 함수
    public GameObject GetRandomItemPrefab()
    {
        return itemPrefabs[Random.Range(0, itemPrefabs.Length)];
    }

    // 오브젝트 풀에서 오브젝트를 가져오는 함수
    public GameObject GetObjectFromPool()
    {
        // 풀에서 비활성화된 오브젝트를 찾아 반환
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }

        // 풀에 빈 자리가 없는 경우 null 반환
        return null;
    }
}
