using UnityEngine;

public static class GlobalFunctions {

    public static float DistanceOnHorizontalPlane(Vector3 pointA, Vector3 pointB)
    {
        Vector3 difference = pointA - pointB;

        var distanceInX = Mathf.Abs(difference.x);
        var distanceInZ = Mathf.Abs(difference.z);
        return distanceInX + distanceInZ;
    }

    public static float Wrap180(float angle)
    {
        angle %= 360;
        if (angle < -180)
        {
            angle += 360;
        }
        else if (angle > 180)
        {
            angle -= 360;
        }
        return angle;
    }

    public static void ChangeWhalePopulation(int regionInd, int newVal)
    {
        PlayerPrefs.SetInt("Whales" + regionInd, newVal);
    }

    public static int GetWhalePopulation(int regionInd)
    {
        return PlayerPrefs.GetInt("Whales" + regionInd, -5);
    }

    public static void EstablishJWH(int regionInd)
    {
        PlayerPrefs.SetInt("JWHPresence" + regionInd, 1);
    }
    
    public static void DemolishJWH(int regionInd)
    {
        PlayerPrefs.SetInt("JWHBanned" + regionInd, 1);
    }

    public static bool IsJWHPresent(int regionInd)
    {
        return PlayerPrefs.GetInt("JWHPresence" + regionInd, 0) == 1;
    }

    public static bool IsJWHDemolished(int regionInd)
    {
        return PlayerPrefs.GetInt("JWHBanned" + regionInd, 0) == 1;
    }

    public static void EstablishTaso(int regionInd)
    {
        PlayerPrefs.SetInt("TassoPresence" + regionInd, 1);
    }

    public static bool IsTassoPresent(int regionInd)
    {
        return PlayerPrefs.GetInt("TassoPresence" + regionInd, 0) == 1;
    }

    /// <summary>
    /// last fishing time by jWH
    /// </summary>
    public static void SetLastFishingTime()
    {
        SetLastTime("LastFishingTime");
    }
    /// <summary>
    /// JWH remaining fishing time
    /// </summary>
    /// <returns></returns>
    public static float GetRemainingFishingTime()
    {
        return GetRemainingTime("LastFishingTime");
    }

    public static void SetLastBrnchCreationTime()
    {
        SetLastTime("LastBranchTime");
    }

    public static float GetRemainingBranchTime()
    {
        return GetRemainingTime("LastBranchTime");
    }

    public static void SetLastMarketingTime()
    {
        SetLastTime("LastMarketingTime");
    }

    public static float GetRemainingMarketingTime()
    {
        return GetRemainingTime("LastMarketingTime");
    }

    public static void SetLastNewSpotTime()
    {
        SetLastTime("LastSpotTime");
    }

    public static float GetRemainingNewSpotTime()
    {
        return GetRemainingTime("LastSpotTime");
    }

    public static void SetLastMnthTime() {
        SetLastTime("LastMonthTime");
    }

    public static float GetRemainingMnthTime()
    {
        return GetRemainingTime("LastMonthTime");
    }

    public static void SetLastTime(string opName)
    {
        PlayerPrefs.SetString(opName, System.DateTime.Now.ToBinary().ToString());
    }

    public static float GetRemainingTime(string opName)
    {
        System.DateTime lastTime = System.DateTime.FromBinary(long.Parse(
        PlayerPrefs.GetString(opName, System.DateTime.MinValue.ToBinary().ToString())));
        Debug.Log("Remaing oyoy " + lastTime);
        Debug.Log("Reamingin " + opName + " " + (System.DateTime.Now - lastTime).TotalSeconds);
        return (float)(System.DateTime.Now - lastTime).TotalSeconds;
    }
    
    public static void SetLastOperation(string name, int regionInd)
    {
        PlayerPrefs.SetString("Operation" + regionInd, name);
        SetLastTime("OperationTime" + regionInd);
    }

    public static string GetLastOperation(int regionInd)
    {
        return PlayerPrefs.GetString("Operation" + regionInd, "");
    }

    public static float GetTassoOpTime(int regionInd)
    {
        return GetRemainingTime("OperationTime" + regionInd);
    }

    public static void SetWhaleReserves(int amount)
    {
        PlayerPrefs.SetInt("WhaleReserves", amount);
    }

    public static int GetWhaleReserves()
    {
        return PlayerPrefs.GetInt("WhaleReserves", 100);
    }

    public static int GetProgress()
    {
        return PlayerPrefs.GetInt("Progress", 1);
    }

    public static void IncreaseProgressLevel()
    {
        PlayerPrefs.SetInt("Progress", GetProgress() + 1);
    }
    
    public static int GetWhalesSaved()
    {
        return PlayerPrefs.GetInt("TotalWhalesSaved", 0);
    }

    public static void AddWhalesSaved(int number) {
        PlayerPrefs.SetInt("TotalWhalesSaved", GetWhalesSaved() + number);
    }

    public static int GetWhalesLost()
    {
        return PlayerPrefs.GetInt("TotalWhalesLost", 0);
    }

    public static void AddWhalesLost(int num)
    {
        PlayerPrefs.SetInt("TotalWhalesLost", GetWhalesLost() + num);
    }

    public static int GetDJINDamage() {
        return PlayerPrefs.GetInt("TotalDJINDamage", 0);
    }

    public static void AddDJINDamage(int num)
    {
        PlayerPrefs.SetInt("TotalDJINDamage", GetDJINDamage() + num);
    }

    public static int GetTotalDJINBanScore() {
        return PlayerPrefs.GetInt("TotalDJINBanScore", 0);
    }

    public static void AddTotalDJINBanScore(int num)
    {
        PlayerPrefs.SetInt("TotalDJINBanScore", GetTotalDJINBanScore() + num);
    }

    public static void SetDJINBanTimeScore()
    {
        System.DateTime gameStartTime = System.DateTime.FromBinary(long.Parse(
        PlayerPrefs.GetString("GameStartTime", System.DateTime.MinValue.ToBinary().ToString())));
        PlayerPrefs.SetInt("DJINBanTimeScore", (int)(GetTotalDJINBanScore() * 2 / Mathf.Clamp( (float)(System.DateTime.Now - gameStartTime).TotalHours, 0, 200) ));
    }

    public static int GetDJINBanTimeScore()
    {
        return PlayerPrefs.GetInt("DJINBanTimeScore", 0);
    }

    public static void SetGameStartTime()
    {
        if (!PlayerPrefs.HasKey("GameStartTime"))
            SetLastTime("GameStartTime");
    }

    public static bool RangerRegistered()
    {
        return PlayerPrefs.GetInt("RangerRegistered", 0) == 1;
    }

    public static void RegisterRanger()
    {
        PlayerPrefs.SetInt("RangerRegistered", 1);
    }

}
