using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Managers
    [SerializeField]
    private TimeManager _timeManager;
    [SerializeField]
    private EventHandler _eventHandler;
    private ScoreHandler _scoreHandler;
    private ScoreManager _scoreManager;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private PlayerController _playerController;

    private bool _isNextScene;
    public bool IsNextScene => _isNextScene;
    // Manage Game Stops.
    private bool _isPauseGame;
    public bool IsPauseGame => _isPauseGame;
    // Manage scene transitions.
    private bool _isFinishGame;
    public bool IsFinishGame => _isFinishGame;
    private bool _isGameOver;
    public bool IsGameOver => _isGameOver;

    [SerializeField]
    private float _insInterVal;
    [SerializeField]
    private float _eventInterVal;
    [SerializeField]
    private int _eventChance;

    // Start is called before the first frame update
    void Start()
    {
        _isPauseGame = false;
        _isFinishGame = false;
        _isGameOver = false;
        _isNextScene = false;
        _scoreHandler = GameObject.FindWithTag("ScoreHandler").GetComponent<ScoreHandler>();
        _scoreManager = ScoreManager.InstanceScoreManager;
        StartCoroutine("SammonEnemies");
        StartCoroutine("GenerateEvents");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFinishGame || _isGameOver) return;

        ChangePuseFlag();
        if (_isPauseGame) return;

        CountDown();

        if (_playerController.health <= 0)
        {
            _isGameOver = true;
            _scoreManager.SortRanking(_scoreHandler.CurrentScore);
        }

        if (_isFinishGame || _isGameOver) StartCoroutine("WaitFinGame");
    }

    // Change pause flag.
    void ChangePuseFlag()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) _isPauseGame = !_isPauseGame;
    }

    // Count down time.
    void CountDown()
    {
        _timeManager.CountDown(Time.deltaTime);
        if (_timeManager.RemainingTime > 0) return;
        if (!_isFinishGame) _scoreManager.SortRanking(_scoreHandler.CurrentScore);
        _isFinishGame = true;
    }

    // Enemy generation process.
    IEnumerator SammonEnemies()
    {
        int count = 0;
        const int STETE_SPLIT = 5;
        float oneTerm = _timeManager.SetTime / STETE_SPLIT;
        float remainingTime = _timeManager.RemainingTime;

        if (remainingTime <= oneTerm * 1)
        {
            count = Random.Range(4, 6);
        }
        else if (remainingTime > oneTerm * 1 && remainingTime <= oneTerm * 2)
        {
            count = Random.Range(3, 5);
        }
        else if (remainingTime > oneTerm * 2 && remainingTime <= oneTerm * 3)
        {
            count = Random.Range(1, 3);
        }
        else if (remainingTime > oneTerm * 4 && remainingTime <= oneTerm * 5)
        {
            count = 1;
        }

        int kind = 0;
        for (int i = 0; i < count; i++)
        {
            if (remainingTime <= oneTerm * 1)
            {
                kind = CalcKind(45, 35, 10, 10);
            }
            else if (remainingTime > oneTerm * 1 && remainingTime <= oneTerm * 2)
            {
                kind = CalcKind(35, 25, 20, 20);
            }
            else if (remainingTime > oneTerm * 2 && remainingTime <= oneTerm * 3)
            {
                kind = CalcKind(25, 15, 30, 30);
            }
            else if (remainingTime > oneTerm * 4 && remainingTime <= oneTerm * 5)
            {
                kind = CalcKind(15, 5, 40, 40);
            }
            _spawnManager.SpawnEnemy(kind);
        }

        yield return new WaitForSeconds(_insInterVal);
        StartCoroutine("SammonEnemies");
    }

    int CalcKind(int enemy0, int enemy1, int enemy2, int enemy3)
    {
        int rnd = Random.Range(0, 101);
        if (rnd > enemy1 && rnd <= enemy0)
        {
            return 0;
        }
        else if (rnd > enemy2 && rnd <= enemy1)
        {
            return 1;
        }
        else if (rnd > enemy3 && rnd <= enemy2)
        {
            return 2;
        }
        else if (rnd <= enemy3)
        {
            return 3;
        }

        return 0;
        ////// It is not versatile and I would like to fix it someday.
    }

    // Control Events.
    IEnumerator GenerateEvents()
    {
        float waitTime = _eventInterVal;
        int eventRaffle = Random.Range(0, 101);
        float launchTime = _timeManager.SetTime;
        float remainingTime = _timeManager.RemainingTime;
        int gameProgress = (int)(remainingTime / launchTime * 100);
        // Publish Events.
        if (gameProgress <= eventRaffle)
        {
            _eventHandler.MonsterHouse(_spawnManager);
            // Waiting time doubles if event issued.
            waitTime *= 3;
        }
        yield return new WaitForSeconds(waitTime);
        StartCoroutine("GenerateEvents");
    }

    IEnumerator WaitFinGame()
    {
        yield return new WaitForSeconds(3.0f);
        _isNextScene = true;
    }

}
