using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Prompt : int

{
    Up,
    Left,
    Right,
    Jump
}

[CreateAssetMenu(fileName = "ButtonPrompt", menuName = "ButtonsPrompt", order = 0)]
public class AdaptablePrompts : ScriptableObject
{
    public List<PromptImage> Images;

    public Sprite GetImage(Prompt prompt, bool keyboard)
    {
        PromptImage image = Images[(int)prompt];
        return keyboard ? image.keySprite : image.gamepadSprite;
    }
}
