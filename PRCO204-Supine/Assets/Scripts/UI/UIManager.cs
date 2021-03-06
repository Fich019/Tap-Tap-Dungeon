using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roomCodeUI;
    
    public void SetText(string text) => roomCodeUI.text = $"Code: {text}";

    void Awake() {
        if (ServerManager.Instance) {
            SetText(ServerManager.Instance.RoomCode);
        }
        else {
            SetText("----");
        }
    }
}
