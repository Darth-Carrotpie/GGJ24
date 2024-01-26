[System.Serializable]
public class ScoreItem
{
    public int score;
    public float deviation;
    public int currentCombo;
    public ScoreItemType scoreItemType;

    public string ToString()
    {
        return "<ScoreItem>.Score=" + score + "; ScoreItemType=" + scoreItemType;
    }
}
public enum ScoreItemType
{
    Perfect,
    Almost,
    DadJoke,
    Botch
}