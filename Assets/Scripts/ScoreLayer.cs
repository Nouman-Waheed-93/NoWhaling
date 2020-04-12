using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLayer : MonoBehaviour {

    public static ScoreLayer instance;

    ScoreDeltaText templateScore;
    
    GameObject WhlngIndctrTemp;

    void Awake () {
        instance = this;
        templateScore = Resources.Load<ScoreDeltaText>("ScoreTxt");
        WhlngIndctrTemp = Resources.Load<GameObject>("WhalingIndicator");
	}
	
    public void CreateScore(Vector3 position, string operation, string score, Color color, Vector3 speed, ScoreDeltaText.ScoreDirection dir)
    {
        Instantiate(templateScore, transform).AppearAtPosition(position, operation, score, color, speed, dir);
    }

    public GameObject CreateWhalingIndctor(Vector3 position)
    {
        return Instantiate(WhlngIndctrTemp, position, Quaternion.identity, transform);
    }

}
