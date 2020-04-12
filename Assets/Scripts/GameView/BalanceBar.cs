using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NoWhaling
{
    public class BalanceBar : MonoBehaviour
    {
        public Slider bar;
        public Company company;

        private void Start()
        {
            company.BalanceUpdated.AddListener(UpdateBalance);
            UpdateBalance();
        }

        void UpdateBalance()
        {
            bar.value = company.bankBalance;
        }
    }
}