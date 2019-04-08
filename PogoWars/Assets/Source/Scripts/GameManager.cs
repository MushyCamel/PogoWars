using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null; 
    private int Level = 0;
    Scene scene;

    [Header("Properties")]

    public int _numRoundsToWin = 3;            
    public float _startDelay = 3f;             
    public float _endDelay = 3f;

    [Header("References")]

    public Text _messageText;
    [SerializeField]
   // public CameraScript _cameraScript;       // Reference to the CameraScript for control during different phases.

    public PlayerManager[] _players;


    private int _roundNumber;
    private WaitForSeconds _startWait; 
    private WaitForSeconds _endWait;
    private PlayerManager _roundWinner;         
    private PlayerManager _gameWinner;
    private Transform[] _spawnPoints;

    // Start is called before the first frame update
    void Awake()
    {
        //Makes sure there is only ever one instance of gamemanger in the scene (think it's called a singleton??)
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
            return;
    }

    void Start()
    {

        // Create the delays so they only have to be made once.
        _startWait = new WaitForSeconds(_startDelay);
        _endWait = new WaitForSeconds(_endDelay);

       // SetCameraTargets();


        StartCoroutine(GameLoop());
    }



    private void SpawnPlayers()
    {
        {
            var spwn = GameObject.FindGameObjectsWithTag("SpawnPoint");
            _spawnPoints = new Transform[spwn.Length];
            for (var i = 0; i < spwn.Length; i++)
                _spawnPoints[i] = spwn[i].transform;
        }

        if (_spawnPoints == null || _players == null)
            return;

        if (_spawnPoints.Length != _players.Length)
            return;

        // For all the tanks...
        for (int i = 0; i < _players.Length; i++)
        {
            // ... create them, set their player number and references needed for control.
            _players[i]._instance = Instantiate(_players[i]._playerPrefab, _spawnPoints[i].transform.position, _spawnPoints[i].transform.rotation) as GameObject;
            _players[i]._playerNumber = i + 1;
            _players[i].Setup();
        }
    }



    private IEnumerator GameLoop()
    {

 
         

        // Start by running 'RoundStarting'.
        yield return StartCoroutine(RoundStarting());

        SpawnPlayers();

        // Once 'RoundStarting' is finished, run the 'RoundPlaying' coroutine return when finished.
        yield return StartCoroutine(RoundPlaying());

        // Once returned, run the 'RoundEnding' coroutine, return when finished.
        yield return StartCoroutine(RoundEnding());

        //   Check if a game winner has been found, load random level
        if (_gameWinner != null)
        {

            // If there is a game winner, load main menu.
            SceneManager.LoadScene(0);
        }
        else
        {
            // If there isn't a winner yet, restart this coroutine so the loop continues.
            // Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
            NextLevel();
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        
        ResetAllPlayers();
        

        _roundNumber++;
        _messageText.text = "ROUND " + _roundNumber;

        yield return _startWait;
    }

    private IEnumerator RoundPlaying()
    {
        _messageText.text = string.Empty;

        while(!OnePlayerLeft())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        _roundWinner = null;

        _roundWinner = GetRoundWinner();

        //Add to the score if there is a winner
        if (_roundWinner != null)
            _roundWinner._wins++;

        _gameWinner = GetGameWinner();

        string message = EndMessage();
        _messageText.text = message;


        //wait for time before giving control to game loop again.
        yield return _endWait;
    }

    private bool OnePlayerLeft()
    {
        int numPlayersLeft = 0;

        //goes through all the players in the array and if they are active adds to the counter.
        for (int i = 0; i< _players.Length; i++)
        {
            if (_players[i] == null)
            {
                numPlayersLeft = 2;
                continue;
            }

            if (_players[i]._instance == null)
            {
                continue;
            }

            if (_players[i]._instance.activeSelf)
                numPlayersLeft++;
        }

        return numPlayersLeft <= 1;
    }

    //Only called if there are 1 or less players active.
    private PlayerManager GetRoundWinner()
    {
        //Goes through all the players and if one of them is active, it's the winner which returns the player number.
        for (int i = 0; i < _players.Length; i++)
        {
            if (_players[i] == null)
                continue;

            if (_players[i]._instance == null)
                continue;

            if (_players[i]._instance.activeSelf)
                return _players[i];
        }

        //only on the occassion that none of them are active (rare but who knows) Will make the game a draw.
        return null;
    }

    //used to find out if there is a game winner
    private PlayerManager GetGameWinner()
    {
        //go through all the players and find out if they have won enough rounds to win the game.
        for (int i = 0; i < _players.Length; i++)
        {
            if (_players[i]._wins == _numRoundsToWin)
                return _players[i];
        }
        //if noone has enough to win return
        return null;
    }

    private string EndMessage()
    {
        //As a default message. Incase somehow the players manage to kill each other at the same time. (highly unlikely unless it's a bug)
        string message = "DRAW!";

        //If there a player dies the player that killed them wins the round, displays a message of the player who is left alive then " WINS THE ROUND!"
        if (_roundWinner != null)
            message = _roundWinner._colouredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < _players.Length; i++)
        {
            message += _players[i]._colouredPlayerText + ": " + _players[i]._wins + " WINS!\n";
        }

        if (_gameWinner != null)
            message = _gameWinner._colouredPlayerText + " WINS THE GAME!";

        return message;
    }

    private void ResetAllPlayers()
    {
        if (_spawnPoints == null || _players == null)
            return;

        if (_players.Length != _spawnPoints.Length)
            return;

        for (int i = 0; i < _players.Length; i++)
        {
            _players[i].Reset(_spawnPoints[i]);
        }
    }

   void NextLevel()
    {
        int index = UnityEngine.Random.Range(1, 5);
        SceneManager.LoadScene(index);
    }

}

