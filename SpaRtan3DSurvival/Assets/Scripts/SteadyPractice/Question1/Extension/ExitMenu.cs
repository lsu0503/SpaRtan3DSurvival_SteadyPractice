using UnityEngine;
using UnityEngine.InputSystem;

public class ExitMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    private PlayerController controller;

    private void Start()
    {
        controller = CharacterManager.Instance.player.controller;

        controller.exitMenu += SetMenuOnOff;
        menuCanvas.SetActive(false);
    }

    public void SetMenuOnOff()
    {
        controller.ToggleCursor();
        menuCanvas.SetActive(!menuCanvas.activeInHierarchy);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
