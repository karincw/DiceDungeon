using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace karin
{
    public class Gauge : MonoBehaviour
    {
        [SerializeField] private Transform Bar1;
        [SerializeField] private Transform Bar2;
        [SerializeField] private TMP_Text hpText;
        [HideInInspector] public int StartGauge1;
        [HideInInspector] public int currentGauge1;
        [HideInInspector] public float StartGauge2;
        [HideInInspector] public float currentGauge2;

        private void Start()
        {
            StartGauge2 = StartGauge1;
            currentGauge2 = currentGauge1;
        }

        private void Update()
        {
            Bar1.localScale = new Vector3((float)currentGauge1 / (float)StartGauge1, 0.6f, 1);
            Bar2.localScale = new Vector3(currentGauge2 / StartGauge2, 0.6f, 1);
            hpText.text = currentGauge1.ToString();
        }

        public void GaugeDecrease(int value)
        {
            var destination = Mathf.Clamp(currentGauge1 - value, 0, StartGauge1);
            DOTween.To(() => currentGauge1, v => currentGauge1 = v, destination, 0.4f);
            DOTween.To(() => currentGauge2, v => currentGauge2 = v, destination, 1.3f);
        }
        public void GaugeIncrease(int value)
        {
            DOTween.To(() => currentGauge1, v => currentGauge1 = v, Mathf.Clamp(currentGauge1 + value, 0, StartGauge1), 0.2f);
            currentGauge2 = Mathf.Clamp(currentGauge1 + value, 0, StartGauge1);
        }

    }
}