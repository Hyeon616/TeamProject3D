using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    public string terrainTag = "ItemSpawner"; // ���� �±�
    public float spawnInterval = 1.0f; // ������ ���� ����
    public int maxPoolSize = 15; // �ִ� ������Ʈ Ǯ ũ��
    public float returnDelay = 30f; // ��ȯ�Ǳ������ ���� �ð�

    public GameObject[] itemPrefabs; // ������ ������ �迭

    private GameObject[] terrainObjects; // ���� ������Ʈ �迭
    private List<GameObject> objectPool = new List<GameObject>(); // ������Ʈ Ǯ
    private List<GameObject> activeObjects = new List<GameObject>(); // Ȱ��ȭ�� ������Ʈ ����Ʈ

    void Start()
    {
        // �ش� �±׸� ���� ������Ʈ�� ��� GameObject �迭�� ����
        terrainObjects = GameObject.FindGameObjectsWithTag(terrainTag);

        // ������Ʈ Ǯ �ʱ�ȭ
        InitializeObjectPool();

        // ���� �������� SpawnItem �Լ��� �ݺ� ����
        InvokeRepeating("SpawnItem", 0f, spawnInterval);
    }

    // ������ ���� �Լ�
    void SpawnItem()
    {
        // �������� ������ ������ ������ �� �ֵ��� ������ �������� ����
        GameObject randomTerrain = terrainObjects[Random.Range(0, terrainObjects.Length)];

        // �������� ������ ��ġ�� �������� ����
        Vector3 spawnPoint = GetRandomPointOnTerrain(randomTerrain);

        // ������ ������ �� �����ϰ� ����
        GameObject itemPrefab = GetRandomItemPrefab();

        if (itemPrefab != null)
        {
            // ������Ʈ Ǯ���� ������Ʈ�� ������
            GameObject newItem = GetObjectFromPool();
            if (newItem == null)
            {
                // ������Ʈ Ǯ�� ����ִ� ��� ���ο� ������Ʈ�� �����Ͽ� �߰�
                Debug.LogWarning("������Ʈ Ǯ ����ִ�");
                newItem = Instantiate(itemPrefab);
                objectPool.Add(newItem);
            }

            // �������� ��ġ�� �����ϰ� Ȱ��ȭ
            newItem.transform.position = spawnPoint;
            newItem.SetActive(true);

            // Ȱ��ȭ�� ������Ʈ ����Ʈ�� �߰�
            activeObjects.Add(newItem);

            // Rigidbody�� Kinematic �Ӽ��� ��Ȱ��ȭ�Ͽ� �߷��� �ۿ��ϵ��� ��
            Rigidbody rb = newItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            // ���� �ð� �Ŀ� ������Ʈ�� Ǯ�� ��ȯ
            StartCoroutine(ReturnObjectToPool(newItem));
        }
        else
        {
            Debug.LogError("������ ����");
        }
    }

    // ������Ʈ�� Ǯ�� ��ȯ�ϴ� �ڷ�ƾ �Լ�
    IEnumerator ReturnObjectToPool(GameObject obj)
    {
        yield return new WaitForSeconds(returnDelay); // ��ȯ ���� �ð� �Ŀ� ����

        // ������Ʈ ��Ȱ��ȭ
        obj.SetActive(false);

        // ������Ʈ�� ������ ��ġ�� �ʱ�ȭ
        Vector3 randomSpawnPoint = GetRandomPointOnTerrain(terrainObjects[Random.Range(0, terrainObjects.Length)]);
        obj.transform.position = randomSpawnPoint;

        // ������Ʈ�� Ǯ�� �߰��ϰ� Ȱ��ȭ�� ����Ʈ���� ����
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
        // ������Ʈ Ǯ�� �̸� ������ ������Ʈ�� �����Ͽ� �߰�
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject newItem = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
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
