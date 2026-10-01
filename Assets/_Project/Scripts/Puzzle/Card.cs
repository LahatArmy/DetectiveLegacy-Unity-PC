using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class Card : MonoBehaviour
{
   [SerializeField] private Image iconImage;

   public Sprite hiddenIconSprite;
   public Sprite iconSprite;

   public bool isSelected;

   public CardController cardController;


    public void OnCardClicked()
    {
        cardController.SetSelected(this);
    }
   public void SetIconSprite(Sprite sprite)
   {
      iconSprite = sprite;
   }

   public void Show()
    {
        Tween.Rotation(transform,
        new Vector3(0f, 180f, 0f), 0.5f);

        Tween.Delay(0.1f, () =>
        {
            iconImage.sprite = iconSprite;
        });

        isSelected = true;
    }

    public void Hide()
    {
        Tween.Rotation(transform,
        new Vector3(0f, 0f, 0f), 0.5f);

        Tween.Delay(0.1f, () =>
        {
            iconImage.sprite = hiddenIconSprite;
        });
        isSelected = false;
    }
}
