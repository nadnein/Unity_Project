using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
    public float reactionTime, countdownTime = 30;

    // the score 
    public int _score;

    // penalty 
    public int penalty; 


    // the playerobject. 
    public GameProfile _player;

    public InputField _nameInput;

    public FileDataHandler dataHandler;


    void Start()
    {
        InitialisePlayer();
        DisplayTime(countdownTime);
        penalty = 0;
        Spawn();
    }

    void Update()
    {
        // Checks if the timer is still going.
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;  // Subtracts the time from the timer set.  
            DisplayTime(countdownTime);
            reactionTime += (Time.deltaTime * 1000); // Adds the time between startime and now. 

            if (_inputs != null)
            {
                for (int i = 0; i < _inputs.Count; i++)
                {
                    if (_inputs[i].IsMatched()) // Constantly checks if a "match" happened
                    {
                        _inputs[i].SetMatched(false);
                        for (int j = 0; j < _indices.Count; j++)
                        {
                            Destroy(_inputs[j].gameObject); // deletes the input dragged to the animal in middle
                        }
                        Destroy(_target.gameObject); // deletes animal in middle 

                        var background = Instantiate(_circlePrefab, _targetParent.position, Quaternion.identity); // adds circle background to animal 
                        background.GetComponent<SpriteRenderer>().sortingOrder = 1;

                        var solution = Instantiate(_targetPrefabs[_randomNumSound], _targetParent.position, Quaternion.identity); // adds correct animal 
                        solution.GetComponent<SpriteRenderer>().sortingOrder = 2;

                        solution.Increase();

                        Destroy(solution.gameObject, 1f);
                        Destroy(background.gameObject, 1f);

                        CalculateScore();

                        // Switches scene if 5 animals were shown
                        if (_counter == 5)
                        {
                            // Using "Invoke" the function is executed after 1 second
                            _loader.Invoke("LoadNextScene", 1f);
                        }
                        // Otherwise spawn animals again 
                        else
                        {
                            Spawn();
                        }

                    }
                    else if (_inputs[i].IsMismatch(_target))
                    {
                        _inputs[i].ResetInput();
                        penalty += 1;
                        Debug.Log("Current Penalty is:" + penalty);
                        
                    }
                }
            }
        }
        else
        {
            QuitGame();
            Debug.Log("Countdown is over.");
        }
    }


    // Spawns animals on the screen 
    public void Spawn()
    {
        reactionTime = 0; // Sets the timer to zero again for every new view of animals. 

        _counter += 1; // increase counter 
        _indices = Enumerable.Range(0, _inputPrefabs.Count).ToList(); // List of integers from 0 to *num of animal prefabs*
        _inputs = new List<DDInput>(); // Init list to store input animals in to access them later 

        // get random order of list indices 
        var randomSet = _indices.OrderBy(s => Random.value).Take(4).ToList();

        // get a random index for choosing the animal picture 
        _randomNumPicture = Random.Range(0, _inputPrefabs.Count);

        // get a random index for choosing the animal sound  
        _randomNumSound = Random.Range(0, _inputPrefabs.Count);

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

    private void QuitGame()
    {
        dataHandler.WriteToFile(_player);
        _loader.Invoke("LoadQuitScene", 1f);
    }


    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(countdownTime / 60);
        float seconds = Mathf.FloorToInt(countdownTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void CalculateScore()
    {
        int score = Mathf.FloorToInt(Mathf.Round((reactionTime)));
        _score = score;
        scoreText.text = score.ToString();
    }

    private void InitialisePlayer()
    {
        // check if inputfield is not empty and enter. 
        if (Input.GetKeyUp(KeyCode.Return) && !(_nameInput.Equals("")))
        {
            _loader.Invoke("LoadMenuScene", 1f);
            _player = new GameProfile(name);
        }
        else
        {

        }

        // TODO: if player chooses a saved profile --> LoadGame()
        // TODO: else make a new profile from inputfield name. 

    }


}
