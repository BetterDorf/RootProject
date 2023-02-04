using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ButtonPrompt : MonoBehaviour
{
    [SerializeField] private AdaptablePrompts prompts;
    [SerializeField] private Canvas promptCanvas;
    [SerializeField] private Prompt promptType;
    [SerializeField] private Vector3 offset = Vector3.up;
    private Canvas instanceCanvas;
    private Image image;

    private bool show = false;
    [SerializeField] private float fadeTime = 0.4f;
    private float fadeProgress = 0.0f;

    private PlayerInput playerInput;

    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        instanceCanvas = Instantiate(promptCanvas, transform.position + offset, Quaternion.identity, transform);
        image = instanceCanvas.GetComponentInChildren<Image>();
        image.color = new Color(255, 255, 255, 0);
    }

    void Update()
    {
        float change = show ? Time.deltaTime : -Time.deltaTime;
        fadeProgress += change;
        fadeProgress = Mathf.Clamp(fadeProgress, 0.0f, 1.0f);
        image.color = Color.Lerp(new Color(255, 255, 255, 0), new Color(255, 255, 255, 1),
            fadeProgress / fadeTime);

        image.sprite = prompts.GetImage(promptType, playerInput.currentControlScheme == "Keyboard&Mouse");
    }

    public void FadeIn()
    {
        show = true;
    }

    public void FadeOut()
    {
        show = false;
    }
}
