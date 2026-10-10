using UnityEngine;
using UnityEngine.UI;

public class SlideManager:MonoBehaviour
{
    public Image screenImage;
    public Sprite[] slides;
    private int currentSlide = 0;

    void Start()
    {
        ShowSlide(0);
    }
    public void NextSlide()
    {
        currentSlide++;
        if(currentSlide >= slides.Length)
        {
            currentSlide = slides.Length - 1;
        }

        ShowSlide(currentSlide);
        Debug.Log("10");
    }

    public void ShowSlide(int index)
    {
        if(index>=0&&index<slides.Length)
        {
            screenImage.sprite = slides[index];
        }
    }
}