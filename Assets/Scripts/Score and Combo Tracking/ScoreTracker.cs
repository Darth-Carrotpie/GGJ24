using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : Singleton<ScoreTracker>
{
    public int PefectDefaultValue;
    public int AlmostDefaultValue;
    public int DadJokeDefaultValue;
    public int BotchDefaultValue;

    public float PefectAllowedDeviation;
    public float AlmostAllowedDeviation;
    public float DadJokeAllowedDeviation;
    //public float BotchAllowedDeviation;
    [SerializeField]
    List<ScoreItem> scoreItems = new List<ScoreItem>();
    public int totalScore = 0;

    public static void AddScore(float deviation)
    {
        ScoreItem newScoreItem = new ScoreItem();
        ScoreItemType type = Instance.GetTypeFromDeviation(deviation);
        newScoreItem.scoreItemType = type;
        newScoreItem.deviation = deviation;
        AddScore(newScoreItem);
    }

    public static void AddScore(ScoreItem newScoreItem)
    {
        newScoreItem.score = Instance.GetDefaultValue(newScoreItem.scoreItemType);
        newScoreItem.currentCombo = ComboTracker.GetCombo();
        Instance.totalScore += newScoreItem.score * newScoreItem.currentCombo;
        Instance.scoreItems.Add(newScoreItem);
        EventCoordinator.TriggerEvent(EventName.Score.ScoreIncreased(), GameMessage.Write().WithScoreItem(newScoreItem));
        Instance.IncreaseCombo(newScoreItem.scoreItemType);
    }
    public static ScoreItem GetLastScore()
    {
        return Instance.scoreItems[Instance.scoreItems.Count-1];
    }
    public static ScoreItem GetScore(int indexAt)
    {
        return Instance.scoreItems[indexAt];
    }

    public static void ClearScores()
    {
        Instance.scoreItems.Clear();
    }
    public static float GetTotalScore()
    {
        float output = 0f;
        for (int i = 0; i < Instance.scoreItems.Count; i++)
        {
            output += Instance.scoreItems[i].score * Instance.scoreItems[i].currentCombo;
        }
        return output;
    }
    int GetDefaultValue(ScoreItemType type)
    {
        switch (type)
        {
            case ScoreItemType.Botch: return BotchDefaultValue;
                case ScoreItemType.DadJoke: return DadJokeDefaultValue;
            case ScoreItemType.Perfect: return PefectDefaultValue;
                case ScoreItemType.Almost: return AlmostDefaultValue;
                default: return -1;
        }
    }

    float GetAllowedDeviation(ScoreItemType type)
    {
        switch (type)
        {
            case ScoreItemType.DadJoke: return DadJokeAllowedDeviation;
            case ScoreItemType.Perfect: return PefectAllowedDeviation;
            case ScoreItemType.Almost: return AlmostAllowedDeviation;
            default: return 9999;
        }
    }
    ScoreItemType GetTypeFromDeviation(float deviation)
    {
        if(deviation <= PefectAllowedDeviation) {
            return ScoreItemType.Perfect;
        }

        if(PefectAllowedDeviation < deviation && deviation <= AlmostAllowedDeviation)
        {
            return ScoreItemType.Almost;

        }

        if (AlmostAllowedDeviation < deviation && deviation <= DadJokeAllowedDeviation)
        {
            return ScoreItemType.DadJoke;

        }

        if (DadJokeAllowedDeviation < deviation)
        {
            return ScoreItemType.Botch;

        }
        return ScoreItemType.Botch;
    }
    void IncreaseCombo(ScoreItemType type)
    {
        switch (type)
        {
            case ScoreItemType.Botch: ComboTracker.ResetCombo(); break;
            //case ScoreItemType.DadJoke: ComboTracker.ResetCombo(); break;
            case ScoreItemType.Perfect: ComboTracker.IncreaseCombo(); break;
            case ScoreItemType.Almost: ComboTracker.IncreaseCombo();break;
        }
    }
}
