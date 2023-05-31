using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePuzzle : MonoBehaviour
{
    public Text chosenPuzzle;
    private Dictionary<int, string> puzzles = new Dictionary<int, string>(){
        {1, "Puzzle 1"},
        {2, "Puzzle 2"},
        {3, "Puzzle 3"},
        {4, "Puzzle 4"},
        {5, "Puzzle 5"}
    };

    private Dictionary<int, string> answers = new Dictionary<int, string>(){
        {1, "69025477"},
        {2, "80555062"},
        {3, "94096616"},
        {4, "18585765"},
        {5, "56337500"}
    };
    // Start is called before the first frame update
    void Start()
    {
        chosenPuzzle.text = puzzles[Random.Range(1, 4)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
