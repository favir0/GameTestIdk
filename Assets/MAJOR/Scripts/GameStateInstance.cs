using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameStateInstance : MonoBehaviour
{
    [SerializeField] private ProgressBar progressBar;

    private GameController _gameController;
    private DamageIndicator _damageIndicator;
    public UnityEvent stageCompleteEvent;

    private bool _isDone = false;
    private int randopaMax = 4;
    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _damageIndicator = FindObjectOfType<DamageIndicator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        progressBar.currentPercent = 100;
    }

    public void DealDamage(int damage)
    {
        damage += Random.Range(-randopaMax, randopaMax);
        if (damage <= 0) damage = 1;
        if (_isDone) return;
        if (progressBar.currentPercent - damage <= 0)
        {
            _damageIndicator.IndicateDamage(damage);
            stageCompleteEvent.Invoke();
            _gameController.CompleteStage();
            _isDone = true;
            return;
        }
        _damageIndicator.IndicateDamage(damage);
        progressBar.currentPercent -= damage;
    }
}
