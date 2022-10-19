using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private GroundPiece[] allGroundPieces;

    void Start()
    {
        SetupNewLevel();
    }

    private void SetupNewLevel()
    {
        allGroundPieces = FindObjectsOfType<GroundPiece>();
    }

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if(singleton!=this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishLoading;
    }

    private void OnLevelFinishLoading(Scene scene, LoadSceneMode mode)
    {
        SetupNewLevel();
    }

    public void CheckComplete()
    {
        bool finished = true;
        for(int i = 0; i < allGroundPieces.Length && finished; i++)
            if (!allGroundPieces[i].isColored) finished = false;
        if(finished) NextLevel();
    }

    private void NextLevel()
    {
        int num = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(num);
    }
}
