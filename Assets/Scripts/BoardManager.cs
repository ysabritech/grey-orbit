using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    private int rows = DifficultySettings.rows;            //grid size (will change dynamically)
    private int columns = DifficultySettings.columns;

    [SerializeField] private RectTransform cardArea;                        //Card area panel
    [SerializeField] private float spacing = 10f;                           //spaces between the cards
    [SerializeField] private GameObject cardPrefab;                         //button prefab created
    [SerializeField] private List<Sprite> cardFrontSprites;                 //assign a list for all images included in the game
    [SerializeField] private TMP_Text matchesText;                              //Assign the text for matches
    [SerializeField] private TMP_Text turnsText;                                //Assign the text for turns

    private int matches = 0;                                                //initialize matches numbr
    private int turns = 0;                                                  //initialize turns numbr

    private List<int> cardPairIds = new List<int>();                        //create a new card pair list
    private List<Card> allCards = new List<Card>();

    private GridLayoutGroup grid;

    private Card firstRevealedCard = null;
    private bool isCheckingMatch = false;

    // Start is called before the first frame update
    void Start()
    {
        grid = cardArea.GetComponent<GridLayoutGroup>(); //set the grid area

        //change the constraint count for a cleaner look based on column count 
        grid.constraintCount = columns;

        AdjustCellSize(); //adjust the cells size at the start of the game
        SpawnCards(); //Changes dynamically based on number of rows and columns

        StartCoroutine(InitialReveal());//Reveal all cards for 3 seconds

        UpdateScoreUI();//start the score UI
    }


    private void UpdateScoreUI()
    {
        matchesText.text = "Matches: " + matches.ToString();
        turnsText.text = "Turns: " + turns.ToString();
    }


    void AdjustCellSize()
    {
        var grid = cardArea.GetComponent<GridLayoutGroup>(); //save the card area into variable grid
        Vector2 area = cardArea.rect.size; //save card area value into a vector2d

        float cellWidth = (area.x - (spacing * (columns - 1))) / columns; //calculate max width by subtracting the x value from the gaps taken
        float cellHeight = (area.y - (spacing * (rows - 1))) / rows; //calculate max height by subtracting the y value from the gaps taken

        float size = Mathf.Min(cellWidth, cellHeight); //calculate the largest size possible to fit

        grid.cellSize = new Vector2(size, size); //control how big each grid cell
    }

    private void SpawnCards()
    {
        int totalCards = rows * columns; //calculate total cards, example: 2x2 = 4
        cardPairIds.Clear();//Clear all IDs

        int pairCount = totalCards / 2;  //Since we have them in pairs

        //1. Create the pair IDs
        for (int i = 0; i < pairCount; i++)
        {
            cardPairIds.Add(i);
            cardPairIds.Add(i);
        }

        //2. Shuffle cards
        for (int i = 0; i < cardPairIds.Count; i++)
        {
            int randomIndex = Random.Range(i, cardPairIds.Count);//get random index to assign
            int temp = cardPairIds[i];//make a temporary holder for storing current value
            cardPairIds[i] = cardPairIds[randomIndex];//replace the value at i with a random index
            cardPairIds[randomIndex] = temp;//store the random index with the current value
        }

        //3. connect each card with its components and instatiate them
        for (int i = 0; i < totalCards; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, cardArea); //instantiate the card prefab
            newCard.transform.localScale = Vector3.one; //locally scale it not based on parent or other variables

            //Connect card with its components
            Card card = newCard.GetComponent<Card>();
            int id = cardPairIds[i];//connect the card index with its id
            Sprite frontSprite = cardFrontSprites[id];//pick the fron sprite from the list

            card.Init(id, frontSprite, this);//give back all components to the card
            allCards.Add(card); //Track all cards
        }

    }

    public void OnCardRevealed(Card card)
    {
        //in case two reveals happen in the same frame to avoid any errors with no timer
        if (isCheckingMatch)
            return;

        //FIRST CARD
        if (firstRevealedCard == null)
        {
            firstRevealedCard = card;
            return;
        }

        //SECOND CARD
        isCheckingMatch = true;
        turns++;                 //count this turn in pairs that was flipped
        UpdateScoreUI();
        StartCoroutine(CheckMatch(card));

    }

    private IEnumerator CheckMatch(Card secondCard)
    {
        // small delay so the player sees the second card
        yield return new WaitForSeconds(0.25f);

        //MATCH :)
        if (firstRevealedCard.PairId == secondCard.PairId)//do they have the same ID?
        {
            firstRevealedCard.LockAsMatched();
            secondCard.LockAsMatched();

            matches++;          //increase matches only when it happens
            UpdateScoreUI();
        }
        else // MISMATCH :( meaning flip back
        {
            firstRevealedCard.ShowBack();
            secondCard.ShowBack();
        }

        //Reset
        firstRevealedCard = null;
        isCheckingMatch = false;
    }

    private IEnumerator InitialReveal()
    {
        //Show all fronts of the cards
        foreach (var card in allCards)
        {
            card.ShowFront();
        }

        //Wait 3 seconds
        yield return new WaitForSeconds(3f);

        //Flip to back
        foreach (var card in allCards)
        {
            card.ShowBack();
        }
    }

}
