using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeHandlerImage : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = this.gameObject.GetComponent<Image>();
    }
    
    // Update the image based on the given value
    public void UpdateImage(float newVal)
    {
        Sprite newSprite = LoadSpriteBasedOnValue(newVal);

        // Safely update the image sprite, handling potential issues
        if (newSprite != null)
        {
            image.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning("Failed to load sprite for value: " + newVal);
        }
    }

    // Load the appropriate sprite based on the value
    private Sprite LoadSpriteBasedOnValue(float value)
    {
        if (value < 10)
            return Resources.Load<Sprite>("Sprites/1");
        else if (value < 30)
            return Resources.Load<Sprite>("Sprites/2");
        else if (value < 50)
            return Resources.Load<Sprite>("Sprites/3");
        else if (value < 70)
            return Resources.Load<Sprite>("Sprites/4");
        else if (value < 90)
            return Resources.Load<Sprite>("Sprites/5");
        else
            return Resources.Load<Sprite>("Sprites/6");
    }
    
}
