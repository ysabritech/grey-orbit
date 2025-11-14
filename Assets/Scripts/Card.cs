using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image backImage;
    [SerializeField] private Image frontImage;

    private int pairId;                //which pair this card belongs to
    private bool isRevealed = false;   //check if our card is revealed
    private BoardManager boardManager; //board manager script

    public int PairId => pairId;        //get pairID
    public bool IsRevealed => isRevealed;//get if isRevealed

    // Called by BoardManager when creating cards
    public void Init(int id, Sprite frontSprite, BoardManager manager)//initialize the card
    {
        pairId = id;
        frontImage.sprite = frontSprite;
        boardManager = manager;
        ShowBack();//Start with showing the back image
    }

    public void OnCardClicked()
    {
        //ignore if card is revealed
        if (isRevealed) return;

        ShowFront();

        //Tell board manager that this card has been revealed using OnCardRevealed
        if (boardManager != null)
        {
            boardManager.OnCardRevealed(this);//To know which card and its current state
        }
    }

    public void ShowFront()//Flip card
    {
        frontImage.enabled = true;
        backImage.enabled = false;
        isRevealed = true;
    }

    public void ShowBack()//Show the back of the cart
    {
        frontImage.enabled = false;
        backImage.enabled = true;
        isRevealed = false;
    }

    //CARDS MATCHED!!
    public void LockAsMatched()
    {
        //Stop any interaction with the buttons
        GetComponent<Button>().interactable = false;

        //Fade functionality when cards are matched and locked
        Color c1 = frontImage.color;
        Color c2 = backImage.color;

        c1.a = 0.5f;
        c2.a = 0.5f;

        frontImage.color = c1;
        backImage.color = c2;

    }
}



/*now it all works well YAYYYY
Include a scoring mechanism.
I need a scoring system please I will have a scoring for the number of matches and number of turns
Now the turns are taken for the pairs*/