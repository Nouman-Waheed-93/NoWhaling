[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
    public int smallSize;
    public int normalSize;
    public int bigSize;
    public string font;
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}