using UnityEngine;
using TMPro;

public class GrenadeCountText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public GameObject playerInfo;
    private PlayerController playerController;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        playerController = playerInfo.GetComponent<PlayerController>();
    }

    private void Update()
    {
        text.text = $"Grenade : {playerController.currentgrenade}";
    }
}
