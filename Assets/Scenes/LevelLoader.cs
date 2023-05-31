using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private InputField inputTextField;
    public Text puzzleText;
    private int puzzleNum = 1;
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
        inputTextField = GetComponent<InputField>();

    }

    public void CheckCodeAndLoadScene()
    {
        if (inputTextField.text == answers[puzzleNum])
        {
            SceneManager.LoadScene("Level " + puzzleNum);
            puzzleNum += 1;
        }
        inputTextField.text = "";
    }
}
