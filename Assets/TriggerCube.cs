using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TriggerCube : MonoBehaviour
{
    private bool playerInsideTrigger = false;

    [Space(10)]
    [Header("Toggle for the gui on off")]
    public bool GuiOn;

    [Space(10)]
    [Header("The text to Display on Trigger")]
    [Tooltip("To edit the look of the text Go to Assets > Create > GUIskin. Add the new Guiskin to the Custom Skin proptery. If you select the GUIskin in your project tab you can now adjust the Label section to change this text")]
    public string Text = "Turn Back";

    [Tooltip("This is the window Box's size. It will be mid screen. Add or reduce the X and Y to move the box in Pixels. ")]
    public Vector2 BoxSize = new Vector2(300, 300); // Change to Vector2

    [Tooltip("Position of the text within the box.")]
    public Vector2 textPosition = new Vector2(0, 0);

    [Space(10)]
    [Tooltip("To edit the look of the text Go to Assets > Create > GUIskin. Add the new Guiskin to the Custom Skin proptery. If you select the GUIskin in your project tab you can now adjust the font, colour, size etc of the text")]
    public GUISkin customSkin;

    [Space(10)]
    [Header("Text Settings")]
    public int textSize = 30; // Size of the text

    // Canvas with sorting order
    private Canvas canvas;

    private void Start()
    {
        // Create a new canvas
        GameObject canvasObject = new GameObject("TextCanvas");
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1; // Set the sorting order to appear behind other UI elements
    }

    // if this script is on an object with a collider display the Gui
    void OnTriggerEnter()
    {
        GuiOn = true;
        playerInsideTrigger = true;
    }

    void OnTriggerExit()
    {
        GuiOn = false;
        playerInsideTrigger = false;
    }

    void OnGUI()
    {
        if (customSkin != null)
        {
            GUI.skin = customSkin;
        }

        if (GuiOn == true)
        {
            // Draw transparent background
            Color backgroundColor = Color.black; // Set the background color
            backgroundColor.a = 0.5f; // Set the alpha channel to make it transparent
            GUI.color = backgroundColor;
            GUI.DrawTexture(new Rect((Screen.width - BoxSize.x) / 2, (Screen.height - BoxSize.y) / 2, BoxSize.x, BoxSize.y), Texture2D.whiteTexture); // Adjusted BoxSize

            // Reset GUI color
            GUI.color = Color.yellow;

            // Set the font size
            GUI.skin.label.fontSize = textSize;

            // Set text with outline
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.normal.textColor = Color.black;
            style.fontSize = textSize;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;
            style.stretchWidth = true;
            style.stretchHeight = true;
            style.wordWrap = true;
            style.richText = true;
            style.contentOffset = new Vector2(0f, 0f);

            Rect rect = new Rect((Screen.width - BoxSize.x) / 2, (Screen.height - BoxSize.y) / 2, BoxSize.x, BoxSize.y); // Adjusted BoxSize
            Vector2 adjustedTextPosition = new Vector2(rect.x + textPosition.x, rect.y + textPosition.y); // Calculate adjusted text position within the box
            GUI.Label(new Rect(adjustedTextPosition.x, adjustedTextPosition.y, BoxSize.x, BoxSize.y), "<b><color=yellow>" + Text + "</color></b>", style);
        }
    }
}
