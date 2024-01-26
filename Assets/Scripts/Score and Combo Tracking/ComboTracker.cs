using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTracker : Singleton<ComboTracker>
{
    [SerializeField]
    private int _combo;
    [SerializeField]
    private int _maxCombo;
    [SerializeField]
    private bool comboReached;

    public static int GetCombo(){return Instance._combo;}
    public static int GetMaxComboVal(){return Instance._maxCombo; }
    public static bool IsMaxCombo(){return Instance.comboReached; }

    void Start()
    {
        
    }

    public static void IncreaseCombo()
    {
        if (!Instance.ChechMaxReached())
        {
            Instance._combo++;
            EventCoordinator.TriggerEvent(EventName.Score.ComboIncreased(), GameMessage.Write().WithIntMessage(Instance._combo));
        }
        if (Instance.ChechMaxReached())
        {
            EventCoordinator.TriggerEvent(EventName.Score.MaxComboReached(), GameMessage.Write().WithIntMessage(Instance._combo));
        }
    }

    bool ChechMaxReached()
    {
        if(_combo >= _maxCombo)
        {
            _combo = _maxCombo;
            comboReached = true;
        } else
        {
            comboReached = false;
        }
        return comboReached;
    }

    public static void ResetCombo()
    {
        Instance._combo = 0;
        Instance.comboReached = false;
    }
}
