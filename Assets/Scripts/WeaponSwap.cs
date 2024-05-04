using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwap : MonoBehaviour
{
    public Animator anim;
    public PlayerInput playerInput;
    public Transform parentTransform;
    public GameObject[] prefabsToSpawn; 
    
    private int weaponIndex = 0;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();

        foreach (GameObject prefab in prefabsToSpawn)
        {
            GameObject newPrefab = Instantiate(prefab, parentTransform);
            newPrefab.SetActive(false);
        }
        Transform CloneWeapon = parentTransform.GetChild(weaponIndex); 
        CloneWeapon.gameObject.SetActive(true);
        gameObject.layer = 8;
    }

   void Update()
   {
       playerInput.actions["Swap"].performed += ctx =>
       {
           if (Keyboard.current.qKey.isPressed)
           {
               Transform CloneWeapon = parentTransform.GetChild(weaponIndex);
               CloneWeapon.gameObject.SetActive(false);
               anim.SetTrigger("Swap");

               weaponIndex++;
               if (weaponIndex == 1) { gameObject.layer = 9; }
               else if (weaponIndex == 2) { gameObject.layer = 10; }

               if (weaponIndex >= prefabsToSpawn.Length)
                   {
                       weaponIndex = 0;
                       gameObject.layer = 8;
                   }
               
               Transform nextCloneWeapon = parentTransform.GetChild(weaponIndex);
               nextCloneWeapon.gameObject.SetActive(true);
           }
       };
   }

}



