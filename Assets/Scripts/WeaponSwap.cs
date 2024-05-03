using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwap : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject weaponHolder;
    public GameObject[] weaponPrefabs; 
    private GameObject currentWeapon;
    private int currentWeaponIndex = 0; 

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        //SpawnWeapon(currentWeaponIndex);
    }

    void Update()
    {
        playerInput.actions["Swap"].performed += ctx =>
        {
            Destroy(currentWeapon);
            currentWeaponIndex = (currentWeaponIndex + 1) % weaponPrefabs.Length; 
            SpawnWeapon(currentWeaponIndex);
        };
    }

    void SpawnWeapon(int index)
    {
        currentWeapon = Instantiate(weaponPrefabs[index], weaponHolder.transform.position, Quaternion.identity);
    }
}
