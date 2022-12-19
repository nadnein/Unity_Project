using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


// Manager of the game 

public class GameManager : MonoBehaviour
{

    // Prefabs for the animal inputs and targets passed through the Unity Inspector
    [SerializeField] private List<DDTarget> _targetPrefabs;
    [SerializeField] private List<DDInput> _inputPrefabs;
    [SerializeField] private GameObject _circlePrefab;

    // Transforms (positions) for the animal prefabs passed through Insprector 
    [SerializeField] private Transform _targetParent, _inputParent;

    // List of animal sounds for current environment 
    public AudioClip[] audioClips;

    // Variables assigned here in script 
    private List<DDInput> _inputs;
    private DDTarget _target;
    private List<int> _indices;
    private int _randomNumPicture, _randomNumSound;
    private int index;

    // Variables for calculating the score
    private int _counter, _score, _penalty, _correctAnimals = 0;

    // Loader to switch between scenes (animal environments)
    public SceneLoader _loader;

    // Text in the UI.
    public TMP_Text correctText, countdownText, scoreTextFinished, ScoreTextRetry;

    // Reaction time in milliseconds. CountdownTime in seconds. 
    public float countdown;
    private float countdownTime = 90;


    // mode of the game, either "training", "game" or "break"
    private string _mode;

    // Popup windows
    public GameObject retryLevelPopup, nextLevelPopup, gameFinishedPopup, taskPopup;

    // current level
    private int _level = 1; 


    void Start()
    {
        taskPopup.SetActive(false);
        retryLevelPopup.SetActive(false);
        nextLevelPopup.SetActive(false);
        gameFinishedPopup.SetActive(false); 
        _mode = "training";
        countdown = countdownTime;
        Spawn();
    }

    void Update()
    {
        if (_mode == "training")
        {
            if (_counter < 2)
            {
                DisplayAnimalNumber();
                CheckMatchOrMismatch();
            }
            else
            {
                _target.StopAudio();
                taskPopup.SetActive(true);
            }
        }
        else if (_mode == "game")
        {
            // Checks if the timer is still going.
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;  // Subtracts the time from the timer set.  
                DisplayTime(countdown);
                DisplayNumCorrect();
                CheckMatchOrMismatch();   
            }
            else
            {
                if (_correctAnimals < 5)
                {
                    RetryOrQuit();
                }
                else
                {
                    _target.StopAudio();
                    if (_level == 3)
                    {
                        gameFinishedPopup.SetActive(true);
                        scoreTextFinished.text = $"SCORE: {CalculateScore()}";
                    }
                    else
                    {
                        var text = nextLevelPopup.transform.GetChild(1).GetComponent<TMP_Text>();
                        text.text = $"You made it to level {_level + 1}, good job!";
                        nextLevelPopup.SetActive(true);
                    }
                    

                }
                
                Debug.Log("Countdown is over.");
            }
        }
        
    }

    void CheckMatchOrMismatch()
    {
        //CalculateScore();
        if (_inputs != null)
        {
            for (int i = 0; i < _inputs.Count; i++)
            {
                if (_inputs[i].IsMatched()) // Constantly checks if a "match" happened
                {
                    _correctAnimals++;
                    _inputs[i].SetMatched(false);
                    for (int j = 0; j < _inputs.Count; j++)
                    {
                        Destroy(_inputs[j].gameObject); // deletes the input dragged to the animal in middle
                    }
                    Destroy(_target.gameObject); // deletes animal in middle 

                    var background = Instantiate(_circlePrefab, _targetParent.position, Quaternion.identity); // adds circle background to animal 
                    background.GetComponent<SpriteRenderer>().sortingOrder = 1;

                    var solution = Instantiate(_targetPrefabs[index], _targetParent.position, Quaternion.identity); // adds correct animal 
                    solution.GetComponent<SpriteRenderer>().sortingOrder = 2;

                    solution.ShowSoulutionAnimal();

                    Destroy(solution.gameObject, 1f);
                    Destroy(background.gameObject, 1f);

                    Spawn();

                }
                else if (_inputs[i].IsMismatch(_target))
                {
                    _inputs[i].ResetInput();

                    // add penalty if user tries to drag and drop wrong animal 
                    _penalty += 1;
                    Debug.Log("Current Penalty is:" + _penalty);

                }
            }
        }
    }

    public void StartNextLevel()
    {
        nextLevelPopup.SetActive(false);
        _target.StartAudio();
        countdown = countdownTime / 2;
        _correctAnimals = 0;
        _level++;
        _score = 0;
    }


    // Spawns animals on the screen 
    public void Spawn()
    {

        _counter += 1; // increase counter 
        _indices = Enumerable.Range(0, _inputPrefabs.Count).ToList(); // List of integers from 0 to *num of animal prefabs*
        _inputs = new List<DDInput>(); // Init list to store input animals in to access them later 

        // get random order of list indices 
        var randomSet = _indices.OrderBy(s => Random.value).Take(4).ToList();
        foreach (var num in randomSet)
        {
            Debug.Log(num);

        }
        var random = new System.Random();
        _randomNumPicture = randomSet[random.Next(randomSet.Count)];
        Debug.Log(_randomNumPicture);
        _randomNumSound = randomSet[random.Next(randomSet.Count)];
        Debug.Log(_randomNumSound);
        if (_mode == "training") index = _randomNumPicture;
        else if (_mode == "game") index = _randomNumSound;


        // for game mode animal picture and sound can differ 
        _target = Instantiate(_targetPrefabs[_randomNumPicture], _targetParent.position, Quaternion.identity);
        _target.SetAudioClip(audioClips[index]);


        for (int i = 0; i < randomSet.Count; i++) 
        {
            var input = Instantiate(_inputPrefabs[randomSet[i]], _inputParent.GetChild(i).position, Quaternion.identity);
            _inputs.Add(input);

            if (randomSet[i] == index) 
            {
                input.Init(_target); // Here we remember what input and target match 
            }
        }
    }

    private void RetryOrQuit()
    {
        _mode = "break";
        _target.StopAudio();
        ScoreTextRetry.text = $"SCORE: {CalculateScore()}";
        retryLevelPopup.SetActive(true);
    }

    public void StartGame()
    {
        _mode = "game";
        _correctAnimals = 0;
        taskPopup.SetActive(false);
        _target.StartAudio();
    }


    private void DisplayTime(float timeToDisplay)
    {

        float minutes = Mathf.FloorToInt(countdown / 60);
        float seconds = Mathf.FloorToInt(countdown % 60);

        if (minutes < 0 || seconds < 0)
        {
            countdownText.text = "TIME:--:--";
        }
        else
        {
            countdownText.text = $"TIME: {string.Format("{0:00}:{1:00}", minutes, seconds)}";
        }
    }

    private void DisplayNumCorrect()
    {
        correctText.text = $"CORRECT: {_correctAnimals.ToString()}";
    }

    private int CalculateScore()
    {
        _mode = "break";

        // equation that calculates score 
        _score = Mathf.RoundToInt(((_correctAnimals - _penalty)/countdownTime) * 1000);

        if (_score < 0) _score = 0; // Set score to 0 if negative 
        var players = DataSaver.loadData<Players>("players");
        var player = players.GetPlayerByName(ExchangeBetweenScenes.playerName);
        player.addScore(_score);


        if (_score > player.highScore) player.highScore = _score; // set new highscore 

        // write to players file 
        DataSaver.saveData(players, "players");
        return _score;
    }

    private void DisplayAnimalNumber()
    {
        correctText.text = $"ANIMAL {_counter.ToString()}/30";
    }



}
