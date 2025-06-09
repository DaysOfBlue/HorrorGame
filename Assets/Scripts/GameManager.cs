using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum Progress
    {
        Tutorial, // Tutorial
        First, // First puzzle, until clear
        Second, // Second puzzle
        Third, // Third puzzle
        Last // End
    }
    public Transform startPoint;
    public GameObject playerPrefab;
    private Progress _progress = Progress.Tutorial;

    // Change progress by calling this method.
    public void SetProgress(Progress newProgress)
    {
        _progress = newProgress;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = Instantiate(playerPrefab,startPoint.position, Quaternion.identity);
        player.name = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(125, 40, 200, 20), "Progress : " + _progress);
        if(GUILayout.Button("Tutorial"))
        {
            SetProgress(Progress.Tutorial);
        }
        if(GUILayout.Button("First"))
        {
            SetProgress(Progress.First);
        }
        if(GUILayout.Button("Second"))
        {
            SetProgress(Progress.Second);
        }
        if(GUILayout.Button("Third"))
        {
            SetProgress(Progress.Third);
        }
        if(GUILayout.Button("Last"))
        {
            SetProgress(Progress.Last);
        }
    }
}
