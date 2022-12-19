using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    // Manager of the game 

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

    // Counts the number of animals shown per scene (animal environment)
    private int _counter = 0;

    // Loader to switch between scenes (animal environments)
    public SceneLoader _loader;

    // Text in the UI.
    public TMP_Text scoreText, countdownText;

    // Reaction time in milliseconds. CountdownTime in seconds. 
    public float reactionTime, countdownTime;

    // penalty 
    public int _score, _penalty, _correctAnimals = 0;

    public string _mode;

    public GameObject retryLevelPopup, nextLevelPopup, gameFinishedPopup;

    private int _level = 1;

    private Player currentPlayer;


    void Start()
    {
        retryLevelPopup.SetActive(false);
        nextLevelPopup.SetActive(false);
        gameFinishedPopup.SetActive(false);

        _mode = "game";
        countdownTime = 20;
        DisplayTime(countdownTime);
        Spawn();
    }

    void Update()
    {
        if (_mode == "training")
        {
            if (_counter < 5)
            {
                CheckMatchOrMismatch();
            }
            else
            {
                _mode = "game";
            }
        }
        else if (_mode == "game")
        {
            // Checks if the timer is still going.
            if (countdownTime > 0)
            {
                countdownTime -= Time.deltaTime;  // Subtracts the time from the timer set.  
                reactionTime += Time.deltaTime;
                DisplayTime(countdownTime);
                CheckMatchOrMismatch();
            }
            else
            {
                if (_correctAnimals < 1)
                {
                    RetryOrQuit();
                }
                else
                {
                    _target.StopAudio();
                    if (_level == 2)
                    {
                        gameFinishedPopup.SetActive(true);
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

                    var solution = Instantiate(_targetPrefabs[_randomNumSound], _targetParent.position, Quaternion.identity); // adds correct animal 
                    solution.GetComponent<SpriteRenderer>().sortingOrder = 2;

                    solution.ShowSoulutionAnimal();

                    Destroy(solution.gameObject, 1f);
                    Destroy(background.gameObject, 1f);

                    CalculateScore();
                    Spawn();

                }
                else if (_inputs[i].IsMismatch(_target))
                {
                    _inputs[i].ResetInput();

                    // add penalty if user tries to drag and drop wrong animal 
                    _penalty += 1;
                    CalculateScore();
                    Debug.Log("Current Penalty is:" + _penalty);

                }
            }
        }
    }

    /*
        public void StartNextLevel()
        {
            nextLevelPopup.SetActive(false);
            _target.StartAudio();
            countdownTime = 10 / 2;
            _correctAnimals = 0;
            _level++;
            _score = 0;
        }

    */


    // Spawns animals on the screen 
    public void Spawn()
    {
        reactionTime = 0; // Sets the timer to zero again for every new view of animals. 

        //_counter += 1; // increase counter 
        _indices = Enumerable.Range(0, _inputPrefabs.Count).ToList(); // List of integers from 0 to *num of animal prefabs*
        _inputs = new List<DDInput>(); // Init list to store input animals in to access them later 

        // get random order of list indices 
        var randomSet = _indices.OrderBy(s => Random.value).Take(4).ToList();

        var random = new System.Random();
        int _randomNumPicture = randomSet[random.Next(randomSet.Count)];

        int _randomNumSound = randomSet[random.Next(randomSet.Count)];


        // get a random index for choosing the animal picture 
        //_randomNumPicture = Random.Range(0, _inputPrefabs.Count);

        // get a random index for choosing the animal sound  
        //_randomNumSound = Random.Range(0, _inputPrefabs.Count);

        // for training animal picture and sound has to be the same 
        //_target = Instantiate(_targetPrefabs[randomNumPicture], _targetParent.position, Quaternion.identity);
        //_target.SetAudioClip(audioClips[randomNumPicture]);

        // for game mode animal picture and sound can differ 
        _target = Instantiate(_targetPrefabs[_randomNumPicture], _targetParent.position, Quaternion.identity);
        _target.SetAudioClip(audioClips[_randomNumSound]);


        for (int i = 0; i < randomSet.Count; i++)
        {
            var input = Instantiate(_inputPrefabs[randomSet[i]], _inputParent.GetChild(i).position, Quaternion.identity);
            _inputs.Add(input);

            if (randomSet[i] == _randomNumSound) // in case of training change to randomNumPicture
            {
                input.Init(_target); // Here we remember what input and target match 
            }
        }
    }

    private void RetryOrQuit()
    {
        // write to file 

        _target.StopAudio();
        retryLevelPopup.SetActive(true);
    }


    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(countdownTime / 60);
        float seconds = Mathf.FloorToInt(countdownTime % 60);

        if (minutes < 0 || seconds < 0)
        {
            countdownText.text = "TIME:--:--";
        }
        else
        {
            countdownText.text = $"TIME: {string.Format("{0:00}:{1:00}", minutes, seconds)}";
        }
    }

    private void CalculateScore()
    {
        var reactionTimeToInt = Mathf.FloorToInt(reactionTime);
        _score += (_correctAnimals / reactionTimeToInt) * 1000 - _penalty;
        Debug.Log(_score);
        scoreText.text = $"SCORE: {_score}";
    }



}
