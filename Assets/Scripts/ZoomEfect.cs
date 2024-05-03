using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ZoomEfect : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vir;
    public PlayerInput playerInput;

    void Start()
    {
        vir = GetComponent<CinemachineVirtualCamera>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        
        
    }
}
