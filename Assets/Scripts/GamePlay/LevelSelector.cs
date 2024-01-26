using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : Singleton<LevelSelector>
{
    public List<BeatLevel> levels;
    public List<GameObject> characters;

    public BeatLevel selectedLevel;
    public GameObject selectedCharacter;

    public int currentSelection;
    int maxSelection;

    void Start()
    {
        EventCoordinator.StartListening(EventName.Input.Menus.SelectCharacterNext(), OnNextChar);
        EventCoordinator.StartListening(EventName.Input.Menus.SelectCharacterPrevious(), OnPreviosChar);
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
    }
}
