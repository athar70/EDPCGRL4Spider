using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetJoystick : MonoBehaviour
{
    public Slider sliderInstance; // Reference to the UI Slider
    public Image sliderFill;       // Reference to the fill image of the slider
    public Image sliderKnob;       // Reference to the knob image of the slider

    private string pathLogFile = "Assets/Resources/LogSUDs.txt"; // Path to the log file
    public float delayedLog = 1f;  // Delay in seconds for logging
    private Logger logger;          // Instance of the Logger class

    void Start()
    {
        // Initialize slider properties
        sliderInstance.minValue = 0;
        sliderInstance.maxValue = 100;
        sliderInstance.wholeNumbers = true;
        sliderInstance.value = 50;

        // Create logger and log file
        logger = new Logger();
        logger.CreateText(pathLogFile);
        logger.WriteTitle(pathLogFile);

        // Start logging the slider value at specified intervals
        InvokeRepeating(nameof(WriteValue), delayedLog, delayedLog);
    }

    // Invoked when the value of the slider changes
    public void ValueChangeCheck()
    {
        Debug.Log(sliderInstance.value);
    }

    void Update()
    {
        // Get input from the OVR controller thumbstick
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        // Update the slider value based on the input
        sliderInstance.value += input.y;

        // Update the fill color based on the slider value
        sliderFill.color = Color.Lerp(Color.green, Color.red, sliderInstance.value / 100);

        // Update the knob image based on the slider value
        sliderKnob.sprite = ChooseKnobImage(sliderInstance.value);
    }

    // Log the current slider value to the log file
    void WriteValue()
    {
        logger.WriteValue(pathLogFile, sliderInstance.value.ToString());
    }

    // Choose the appropriate knob image based on the slider value
    public Sprite ChooseKnobImage(float newVal)
    {
        // Load default sprite
        Sprite newSprite = Resources.Load<Sprite>("Sprites/1");

        // Determine which sprite to load based on the value of the slider
        if (newVal < 10)
            newSprite = Resources.Load<Sprite>("Sprites/1");
        else if (newVal < 30)
            newSprite = Resources.Load<Sprite>("Sprites/2");
        else if (newVal < 50)
            newSprite = Resources.Load<Sprite>("Sprites/3");
        else if (newVal < 70)
            newSprite = Resources.Load<Sprite>("Sprites/4");
        else if (newVal < 90)
            newSprite = Resources.Load<Sprite>("Sprites/5");
        else
            newSprite = Resources.Load<Sprite>("Sprites/6");

        return newSprite;
    }
}
