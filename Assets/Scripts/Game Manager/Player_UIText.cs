using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_UIText : MonoBehaviour
{
    public static Player_UIText instance;

    public GameObject chat_panel;
    public CanvasGroup chat_canvasGroup;
    [SerializeField] private TextMeshProUGUI interaction_text;
    public static bool displayDone = false;

    private void Awake()
    {
        instance = this;
    }

    public void DisplayUI(string text) //emfanise ta minimata  
    {
        StartCoroutine(Text_Display(text));
    }

    IEnumerator Text_Display(string text) //emfanise ta minimata 
    {
        if (!displayDone)
        {
            displayDone = true;
            chat_panel.SetActive(true);
            LeanTween.alphaCanvas(chat_canvasGroup, 1, 0.5f);
            interaction_text.text = text;
            yield return new WaitForSeconds(2.5f);
            LeanTween.alphaCanvas(chat_canvasGroup, 0, 0.5f);
            yield return new WaitForSeconds(0.5f);
            chat_panel.SetActive(false);
            displayDone = false;
        }
    }


}
