using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : Singleton<LevelSelector>
{
    public List<BeatLevel> levels;
    public List<GameObject> characters;
    public List<GameObject> posters;

    public BeatLevel selectedLevel;
    public GameObject selectedCharacter;
    public GameObject selectedPoster;

    public Transform comedianLocation;
    public Transform posterLocation;

    public int currentSelection;
    int maxSelection;

    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.Menus.SelectCharacterNext(), OnNextChar);
        EventCoordinator.StartListening(EventName.Input.Menus.SelectCharacterPrevious(), OnPreviosChar);
        EventCoordinator.StartListening(EventName.World.GameStateChange(), OnSceneChange);
        maxSelection = levels.Count-1;
        RotateSelectionIndex(0);
    }

    void OnNextChar(GameMessage msg)
    {
        RotateSelectionIndex(1);
    }

    void OnPreviosChar(GameMessage msg)
    {
        RotateSelectionIndex(-1);
    }

    void RotateSelectionIndex(int increment)
    {
        currentSelection += increment;
        if (currentSelection > maxSelection)
        {
            currentSelection = 0;
        }
        if (currentSelection < 0)
        {
            currentSelection = maxSelection;
        }
        Debug.Log(currentSelection.ToString());
        selectedLevel = levels[currentSelection];
        selectedCharacter = characters[currentSelection];
               
        RotatePoster();
    }
    void RotatePoster()
    {
        if (selectedPoster != null)
        {
            selectedPoster.GetComponent<Animator>().SetTrigger("fadeout");
            Destroy(selectedPoster, 0.5f);
        }
        selectedPoster = Instantiate(posters[currentSelection], posterLocation);
        selectedPoster.transform.position = Vector3.zero;
    }
    void OnSceneChange(GameMessage msg)
    {
        if(msg.gameState == GameState.BeatRun)
        {
            GameObject newChar = Instantiate(selectedCharacter, comedianLocation);
            newChar.transform.position = Vector3.zero;
        }
    }
}
