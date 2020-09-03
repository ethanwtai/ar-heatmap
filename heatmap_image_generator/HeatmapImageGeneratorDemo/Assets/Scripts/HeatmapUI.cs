using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class HeatmapUI : MonoBehaviour
{
    public InputField widthInput, heightInput, TileRatioXInput, TileRatioYInput, ColumnsInput, RowsInput, ColorschemeInput, ImageInput;
    public Button FilenameInput, Generate;
    public Text file, filePlaceholder;
    private string numbers = "0123456789";
    private Process heatmapGenerator;

    // Start is called before the first frame update
    void Start()
    {
        //Show only the placeholder for the button
        file.gameObject.SetActive(false);
        //Initialize the buttons
        FilenameInput.onClick.AddListener(() => ButtonCall(FilenameInput));
        Generate.onClick.AddListener(() => ButtonCall(Generate));
    }

    public void Update()
    {

        // Making sure that there can only be numbers for the width, height, etc. inputs
        widthInput.onValidateInput = (string text, int charIndex, char newChar) =>
        {
            return ValidateInput(numbers, newChar);
        };
        heightInput.onValidateInput = (string text, int charIndex, char newChar) =>
        {
            return ValidateInput(numbers, newChar);
        };
        TileRatioXInput.onValidateInput = (string text, int charIndex, char newChar) =>
        {
            return ValidateInput(numbers, newChar);
        };
        TileRatioYInput.onValidateInput = (string text, int charIndex, char newChar) =>
        {
            return ValidateInput(numbers, newChar);
        };
        ColumnsInput.onValidateInput = (string text, int charIndex, char newChar) =>
        {
            return ValidateInput(numbers, newChar);
        };
        RowsInput.onValidateInput = (string text, int charIndex, char newChar) =>
        {
            return ValidateInput(numbers, newChar);
        };

    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    private char ValidateInput(string validChars, char inputChar)
    {
        if (validChars.IndexOf(inputChar) != -1)
        {
            return inputChar;
        } 
        else 
        {
            return '\0';
        }
    }

    private void ButtonCall(Button b)
    {
        //Letting the user select the location of the data file
        if (b == FilenameInput)
        {
            file.text = EditorUtility.OpenFilePanel("Choose Data File", "", "csv");
            filePlaceholder.gameObject.SetActive(false);
            file.gameObject.SetActive(true);
        }

        if (b == Generate)
        {
            Heatmap();
        }
    }
    private void Heatmap()
    {
        heatmapGenerator = new Process();
        heatmapGenerator.StartInfo.FileName = Application.dataPath + "../../../generators/heatmap_gen";
        heatmapGenerator.StartInfo.Arguments = widthInput.text + " " + heightInput.text + " " +
            TileRatioXInput.text + " " + TileRatioYInput.text + " " + ColumnsInput.text + " " +
            RowsInput.text + " " + file.text + " " + ColorschemeInput.text + " > example_output/" + ImageInput.text + ".png";
        heatmapGenerator.Start();
    }
}
