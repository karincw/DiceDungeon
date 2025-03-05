using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Karin
{
    public class Gauge : MonoBehaviour
    {
        [SerializeField] GameObject Bar;
        private float StartGauge;
        private float currentGuage;

        private void Awake()
        {
            currentGuage = StartGauge;
        }

        private void Update()
        {
            Bar.transform.localScale = new Vector3(currentGuage / StartGauge, 0.9f, 1);
        }

        public void GaugeDecrease(float value)
        {
            currentGuage -= value;
            currentGuage = Mathf.Clamp(currentGuage, 0, StartGauge);
        }
        public void GaugeIncrease(float value)
        {
            currentGuage += value;
            currentGuage = Mathf.Clamp(currentGuage, 0, StartGauge);
        }

    }
}