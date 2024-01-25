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
        newScoreItem.scoreItemType = Instance.GetTypeFromDeviation(deviation);
        newScoreItem.score = Instance.GetDefaultValue(newScoreItem.scoreItemType);
        Instance.scoreItems.Add(newScoreItem);
        Instance.totalScore += newScoreItem.score;
        EventCoordinator.TriggerEvent(EventName.World.ScoreIncreased(), GameMessage.Write().WithIntMessage(newScoreItem.score));
    }

    public static void AddScore(ScoreItemType type)
    {
        ScoreItem newScoreItem = new ScoreItem();
        newScoreItem.scoreItemType = type;
        newScoreItem.score = Instance.GetDefaultValue(type);
        Instance.scoreItems.Add(newScoreItem);
        Instance.totalScore += newScoreItem.score;
        EventCoordinator.TriggerEvent(EventName.World.ScoreIncreased(), GameMessage.Write().WithIntMessage(newScoreItem.score));
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
            output += Instance.scoreItems[i].score;
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
}
