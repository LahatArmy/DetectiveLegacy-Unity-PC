using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using PrimeTween;

public class CardController : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Sprite[] sprites;
    [SerializeField] private GameObject clueObject;

    private List<Sprite> spritePairs;

    Card firstSelectedCard;
    Card secondSelectedCard;

    int matchCounts;

    private void Start()
    {
        clueObject.transform.localScale = Vector3.zero;

        PrepareSprites();
        CreateCards();
    }

    private void PrepareSprites()
    {
        spritePairs = new List<Sprite>();

        for (int i = 0; i < sprites.Length; i++)
        {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);
        }

        ShuffleSprites(spritePairs);
    }

    private void CreateCards()
    {
        for (int i = 0; i < spritePairs.Count; i++)
        {
            Card newCard = Instantiate(cardPrefab, gridTransform);
            newCard.SetIconSprite(spritePairs[i]);
            newCard.cardController = this;
        }
    }

    public void SetSelected(Card card)
    {
        if(card.isSelected == false)
        {
            card.Show();

            if(firstSelectedCard == null)
            {
                firstSelectedCard = card;
                return;
            }

            if(secondSelectedCard == null)
            {
                secondSelectedCard = card;
                StartCoroutine(CheckMatching(firstSelectedCard, secondSelectedCard));

                firstSelectedCard = null;
                secondSelectedCard = null;
            }
        }
    }

    IEnumerator CheckMatching(Card a, Card b)
    {
        yield return new WaitForSeconds(0.3f);
        if(a.iconSprite == b.iconSprite)
        {
            Debug.Log("Cards Match!");
            matchCounts++;
            if (matchCounts >= spritePairs.Count / 2)
            {
                Debug.Log("All Cards Matched!");

                PrimeTween.Sequence.Create()
                    .Chain(
                        PrimeTween.Tween.Scale(
                        gridTransform,
                        Vector3.one * 1.2f,
                        0.2f,
                        ease: PrimeTween.Ease.OutBack
                        )
                    )
                    .Chain(
                        PrimeTween.Tween.Scale(
                        gridTransform,
                        Vector3.one,
                        0.1f
                        )
                    )
                    .ChainCallback(() =>
                        {
                            gridTransform.gameObject.SetActive(false);
                            clueObject.SetActive(true);
                        })
                    .Chain(
                        PrimeTween.Tween.Scale(
                        clueObject.transform,
                        Vector3.one,
                        0.4f,
                        ease: PrimeTween.Ease.OutBack
                        )
                );
            }
        }
        else
        {
            Debug.Log("Cards Do Not Match!");
            yield return new WaitForSeconds(1f);
            a.Hide();
            b.Hide();
        }

        
    }

    private void ShuffleSprites(List<Sprite> spritesToShuffle)
    {
        for (int i = spritesToShuffle.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            Sprite temp = spritesToShuffle[i];
            spritesToShuffle[i] = spritesToShuffle[randomIndex];
            spritesToShuffle[randomIndex] = temp;
        }

    }
}
