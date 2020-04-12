using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameView {
    public class BalanceView : MonoBehaviour {

        public Company company;
        public Slider slider;
        public float sliderMovementSpeed;

        int Balance;

        // Use this for initialization
        void Start() {
            slider = GetComponent<Slider>();
            slider.maxValue = int.MaxValue;
        }
        
        void OnBalanceChanged(int Balance) {
            this.Balance = Balance;
        }
        
    }
}
