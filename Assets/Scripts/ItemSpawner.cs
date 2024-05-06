using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
    // �������� ������ ������ �±�
    public string terrainTag = "ItemSpawner";

    // ������ ���� ����
    public float spawnInterval = 5.0f;

    // ������Ʈ Ǯ�� �ִ� ũ��
    public int maxPoolSize = 15;

    // �������� ������ �� ��ȯ�Ǳ������ ��� �ð�
    public float returnDelay = 30f;

    // ������ ������ �����յ�
    public GameObject[] itemPrefabs;

    // ���� ������Ʈ �迭
    private GameObject[] terrainObjects;

    // ������Ʈ Ǯ
    private List<GameObject> objectPool = new List<GameObject>();

    // Ȱ��ȭ�� ������Ʈ ����Ʈ
    private List<GameObject> activeObjects = new List<GameObject>();

    void Start()
    {
        // 1. �ش� �±׸� ���� ������Ʈ�� ��� GameObject �迭�� ����
        terrainObjects = GameObject.FindGameObjectsWithTag(terrainTag);

        // 2. ������Ʈ Ǯ �ʱ�ȭ
        InitializeObjectPool();

        // 3. ���� �������� SpawnItem �Լ��� �ݺ� ����
        InvokeRepeating("SpawnItem", 0f, spawnInterval);
    }

    // ������ ���� �Լ�
    void SpawnItem()
    {
        // 1. �������� ������ ������ ������ �� �ֵ��� ������ �������� ����
        GameObject randomTerrain = terrainObjects[Random.Range(0, terrainObjects.Length)];

        // 2. �������� ������ ��ġ�� �������� ����
        Vector3 spawnPoint = GetRandomPointOnTerrain(randomTerrain);

        // 3. ������ ������ �� �����ϰ� ����
        GameObject itemPrefab = GetRandomItemPrefab();

        if (itemPrefab != null)
        {
            // 4. ������Ʈ Ǯ���� ������Ʈ�� ������
            GameObject newItem = GetObjectFromPool();
            if (newItem == null)
            {
                // 5. ������Ʈ Ǯ�� ����ִ� ��� ���ο� ������Ʈ�� �����Ͽ� �߰�
                newItem = Instantiate(itemPrefab);
                objectPool.Add(newItem);
            }

            // 6. �������� ��ġ�� �����ϰ� Ȱ��ȭ
            newItem.transform.rotation = Quaternion.Euler(-90, 0, 0);
            newItem.transform.position = spawnPoint;
            newItem.SetActive(true);
            activeObjects.Add(newItem);

            // 7. Rigidbody�� Kinematic �Ӽ��� ��Ȱ��ȭ�Ͽ� �߷��� �ۿ��ϵ��� ��
            Rigidbody rb = newItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            // 8. ���� �ð� �Ŀ� ������Ʈ�� Ǯ�� ��ȯ
            StartCoroutine(ReturnObjectToPool(newItem));
        }
        else
        {
            Debug.LogError("No prefab available.");
        }
    }

    // ������Ʈ�� Ǯ�� ��ȯ�ϴ� �ڷ�ƾ �Լ�
    IEnumerator ReturnObjectToPool(GameObject obj)
    {
        yield return new WaitForSeconds(returnDelay); // ��ȯ ���� �ð� �Ŀ� ����

        // 1. ������Ʈ ��Ȱ��ȭ
        obj.SetActive(false);

        // 2. ������Ʈ�� ������ ��ġ�� �ʱ�ȭ
        Vector3 randomSpawnPoint = GetRandomPointOnTerrain(terrainObjects[Random.Range(0, terrainObjects.Length)]);
        obj.transform.position = randomSpawnPoint;

        // 3. ������Ʈ�� Ǯ�� �߰��ϰ� Ȱ��ȭ�� ����Ʈ���� ����
        objectPool.Add(obj);
        activeObjects.Remove(obj);
    }

    // ���� ���� ������ �� ��ȯ �Լ�
    Vector3 GetRandomPointOnTerrain(GameObject terrainObject)
    {
        Bounds bounds = terrainObject.GetComponent<Renderer>().bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        float y = bounds.max.y + 15f;
        return new Vector3(x, y, z);
    }

    // ������Ʈ Ǯ �ʱ�ȭ �Լ�
    private void InitializeObjectPool()
    {
        // Ǯ�� �θ� ������Ʈ ����
        GameObject poolParent = new GameObject("ObjectPool");

        // ������Ʈ Ǯ�� �̸� ������ ������Ʈ�� �����Ͽ� �߰�
        for (int i = 0; i < maxPoolSize; i++)
        {
            // �θ� ������Ʈ �Ʒ��� ���ο� ������ ����
            GameObject newItem = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], poolParent.transform);
            objectPool.Add(newItem);
            newItem.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ ���·� ����
        }
    }

    // ������ ������ ������ ��ȯ �Լ�
    public GameObject GetRandomItemPrefab()
    {
        return itemPrefabs[Random.Range(0, itemPrefabs.Length)];
    }

    // ������Ʈ Ǯ���� ������Ʈ�� �������� �Լ�
    public GameObject GetObjectFromPool()
    {
        // Ǯ���� ��Ȱ��ȭ�� ������Ʈ�� ã�� ��ȯ
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }

        // Ǯ�� �� �ڸ��� ���� ��� null ��ȯ
        return null;
    }
}
