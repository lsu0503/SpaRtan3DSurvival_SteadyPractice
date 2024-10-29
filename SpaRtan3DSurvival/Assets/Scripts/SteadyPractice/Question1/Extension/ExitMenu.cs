using UnityEngine;
using UnityEngine.InputSystem;

public class ExitMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    private PlayerController controller;

    private void Start()
    {
        menuCanvas.SetActive(false);
        controller = CharacterManager.Instance.player.controller;

        controller.exitMenu += SetMenuOnOff;
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            SetMenuOnOff();
        }
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
