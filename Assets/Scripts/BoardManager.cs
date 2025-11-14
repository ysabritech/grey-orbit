using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private RectTransform cardArea; //Card area panel
    [SerializeField] private int rows = 4;                //grid size (will change dynamically)
    [SerializeField] private int columns = 4;
    [SerializeField] private float spacing = 10f; //spaces between the cards
    [SerializeField] private GameObject cardPrefab;

    private GridLayoutGroup grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = cardArea.GetComponent<GridLayoutGroup>(); //set the grid area

        //change the constraint count for a cleaner look based on column count 
        grid.constraintCount = columns;

        AdjustCellSize(); //adjust the cells size at the start of the game
        SpawnCards(); //Changes dynamically based on number of rows and columns
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

        for (int i = 0; i < totalCards; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, cardArea); //instantiate the card prefab
            newCard.transform.localScale = Vector3.one; //locally scale it not based on parent or other variables
        }
    }
}
