using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStepBtnAssigner : MonoBehaviour {

    public int index;
    public Transform BtnParent;

    void Update () {
        if(!GetComponent<TutorialStep>().okBtn)
            GetComponent<TutorialStep>().okBtn = BtnParent.GetChild(index).GetComponent<Button>();
	}
	
}
