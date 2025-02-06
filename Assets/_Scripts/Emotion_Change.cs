using UnityEngine;
using UnityEngine.UI;

public class Emotion_Change : MonoBehaviour
{
    public Sprite[] emotions;
    private Image icon;
    int currentIcon = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon = GetComponent<Image>();
        icon.sprite = emotions[0];
    }

    public void NextIcon()
    {
        currentIcon++;
        if (currentIcon < emotions.Length) {
            icon.sprite = emotions[currentIcon];
        }

    }
}
