using UnityEngine;
using TMPro;
using PrimeTween;

public class PasscodeDoor : MonoBehaviour
{
    [Header("Passcode Settings")]
    [SerializeField] private string correctCode = "1234";
    [SerializeField] private int maxDigits = 4;

    [Header("UI")]
    [SerializeField] private Transform panelTransform;
    [SerializeField] private TMP_Text codeText;
    [SerializeField] private TMP_Text feedbackText;

    private string currentCode = "";

    private void OnEnable()
    {
        ResetPasscode();

        // Popup animation
        panelTransform.localScale = Vector3.zero;

        Tween.Scale(
            panelTransform,
            Vector3.one,
            0.35f,
            ease: Ease.OutBack
        );
    }

    public void ResetPasscode()
    {
        currentCode = "";

        codeText.text = CreateCodeDisplay();
        feedbackText.text = "";
    }

    public void AddNumber(string number)
    {
        if (currentCode.Length >= maxDigits)
            return;

        currentCode += number;

        codeText.text = CreateCodeDisplay();

        // Feedback ketika angka ditekan
        Tween.Scale(
            codeText.transform,
            Vector3.one * 1.15f,
            0.08f,
            ease: Ease.OutBack
        ).OnComplete(() =>
        {
            Tween.Scale(
                codeText.transform,
                Vector3.one,
                0.08f
            );
        });
    }

    public void DeleteNumber()
    {
        if (currentCode.Length <= 0)
            return;

        currentCode = currentCode.Substring(
            0,
            currentCode.Length - 1
        );

        codeText.text = CreateCodeDisplay();
    }

    public void Submit()
    {
        if (currentCode.Length < maxDigits)
        {
            WrongFeedback("INCOMPLETE");
            return;
        }

        if (currentCode == correctCode)
        {
            CorrectFeedback();
        }
        else
        {
            WrongFeedback("INCORRECT");
        }
    }

    private string CreateCodeDisplay()
    {
        string display = "";

        for (int i = 0; i < maxDigits; i++)
        {
            if (i < currentCode.Length)
                display += "● ";
            else
                display += "_ ";
        }

        return display;
    }

    private void WrongFeedback(string message)
    {
        feedbackText.text = message;

        // Feedback text muncul
        feedbackText.transform.localScale = Vector3.zero;

        Tween.Scale(
            feedbackText.transform,
            Vector3.one,
            0.2f,
            ease: Ease.OutBack
        );

        // Panel shake
        Tween.ShakeLocalPosition(
            panelTransform,
            strength: new Vector3(15f, 0f, 0f),
            duration: 0.35f
        );

        // Reset setelah sebentar
        Tween.Delay(0.6f).OnComplete(() =>
        {
            currentCode = "";
            codeText.text = CreateCodeDisplay();
            feedbackText.text = "";
        });
    }

    private void CorrectFeedback()
    {
        feedbackText.text = "ACCESS GRANTED";

        feedbackText.transform.localScale = Vector3.zero;

        Tween.Scale(
            feedbackText.transform,
            Vector3.one,
            0.3f,
            ease: Ease.OutBack
        );

        // Success pop
        Tween.Scale(
            panelTransform,
            Vector3.one * 1.05f,
            0.15f,
            ease: Ease.OutBack
        ).OnComplete(() =>
        {
            Tween.Scale(
                panelTransform,
                Vector3.one,
                0.1f
            );
        });

        // Buka pintu setelah feedback
        Tween.Delay(0.8f).OnComplete(() =>
        {
            //GAME SELESAI
        });
    }


}