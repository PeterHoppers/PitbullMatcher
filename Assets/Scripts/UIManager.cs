using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Color gradiantColor1;
    public Color gradiantColor2;

    public GameObject basicSelectable;
    public TextMeshProUGUI resultsText;

    public Color SetColorGradiant(int index, int maxNum)
    {
        float ratio = 1.0f / maxNum;

        return Color.Lerp(gradiantColor1, gradiantColor2, ratio * index);
    }

    public void SetColorsOfButton(Button button)
    {
        ColorBlock buttonColors = button.colors;

        Color.RGBToHSV(buttonColors.normalColor, out float h, out float satuation, out float v);

        buttonColors.highlightedColor = Color.HSVToRGB(h * .2f, satuation * 1.9f, v * .4f);
        buttonColors.pressedColor = Color.HSVToRGB(h, satuation * .4f, v);
        buttonColors.selectedColor = Color.HSVToRGB(h * .7f, satuation * 1.2f, v);

        button.colors = buttonColors;
    }

    public void SetColorsOfButton(Button button, Color baseColor)
    {
        ColorBlock buttonColors = button.colors;

        Color.RGBToHSV(baseColor, out float h, out float satuation, out float v);

        buttonColors.normalColor = baseColor;
        buttonColors.highlightedColor = Color.HSVToRGB(h, satuation * 1.2f, v / 1.5f);
        buttonColors.pressedColor = Color.HSVToRGB(h, satuation * .6f, v);
        buttonColors.selectedColor = Color.HSVToRGB(h * .9f, satuation, v);

        button.colors = buttonColors;
    }

    public void SetDefaultColorToButton(Button button)
    {
        SetColorsOfButton(button, basicSelectable.GetComponent<Button>().colors.normalColor);
    }

    public void SetButtonToResult(Button button, Color answerColor)
    {
        button.interactable = false;
        ColorBlock buttonColors = button.colors;

        buttonColors.disabledColor = answerColor;

        button.colors = buttonColors;
    }

    public void SetResultsText(int numCorrect)
    {
        resultsText.text = $"You got {numCorrect} of the songs correct!";
    }

}
