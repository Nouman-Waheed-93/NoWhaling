using System.Collections;
using System.Collections.Generic;

public static class InFieldProfitLoss{

    public static Dictionary<string, int> companyPLInField = new Dictionary<string, int>();
    
    static bool initialized;

    public static void Start() {
        companyPLInField.Add("TassoLoss", 0);
        companyPLInField.Add("TassoProfit", 0);
        companyPLInField.Add("JWHLoss", 0);
        companyPLInField.Add("JWHProfit", 0);
        companyPLInField.Add("JWHWhalesHunted", 0);
        initialized = true;
    }
    
    public static int GetWhalesHunted()
    {
        return GetValue("JWHWhalesHunted");
    }

    public static void SetWhalesHunted(int value)
    {
        SetValue("JWHWhalesHunted", value);
    }

    public static int GetLoss(string companyName)
    {
        return GetValue(companyName + "Loss");
    }

    public static int GetProfit(string companyName)
    {
        return GetValue(companyName + "Profit");
    }
    
    public static void SetLoss(string companyName, int loss)
    {
        SetValue(companyName + "Loss", loss);
    }

    public static void SetProfit(string companyName, int profit)
    {
        SetValue(companyName + "Profit", profit);
    }
    
    static void SetValue(string key, int value)
    {
        if (!initialized)
            Start();
        companyPLInField.Remove(key);
        companyPLInField.Add(key, value);
    }

    static int GetValue(string key)
    {
        if (!initialized)
            Start();
        int retVal = 0;
        companyPLInField.TryGetValue(key, out retVal);
        return retVal;
    }
}
