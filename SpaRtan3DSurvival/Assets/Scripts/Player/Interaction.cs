using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteracable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteracable = hit.collider.GetComponent<IInteractable>();

                    SetPrompText();
                }
            }

            else
            {
                curInteractGameObject = null;
                curInteracable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPrompText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteracable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteracable != null)
        {
            curInteracable.OnInteract();
            curInteractGameObject = null;
            curInteracable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}