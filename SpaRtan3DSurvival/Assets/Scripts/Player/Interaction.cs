using System;
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

    //public TextMeshProUGUI promptText; // Q3 개선 문제 수정 사항
    private Camera camera;

    public event Action onInteractionEvent;

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
                    // Q3 개선 문제 추가 사항
                    if (curInteracable != null)
                        curInteracable.OutAim();

                    curInteractGameObject = hit.collider.gameObject;
                    curInteracable = hit.collider.GetComponent<IInteractable>();
                    curInteracable.OnAimed();

                    // Q3 개선 문제 수정 사항
                    //SetPrompText();
                }
            }

            else
            {
                if(curInteracable != null)
                    curInteracable.OutAim();

                GameManager.Instance.OffPrompt();
                curInteractGameObject = null;
                curInteracable = null;


                // Q3 개선 문제 수정 사항
                //promptText.gameObject.SetActive(false);
            }
        }
    }

    // Q3 개선 문제 수정 사항
    //private void SetPrompText()
    //{
    //    promptText.gameObject.SetActive(true);
    //    promptText.text = curInteracable.GetInteractPrompt();
    //}

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteracable != null)
        {
            onInteractionEvent?.Invoke();
            curInteractGameObject = null;
            curInteracable = null;

            // Q3 개선 문제 수정 사항
            //curInteracable.OnInteract();
            //promptText.gameObject.SetActive(false);
        }
    }
}