using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour {
    public class OnTickEventArgs : EventArgs {
        public int Tick;
    }

    public static event EventHandler<OnTickEventArgs> OnTick;
    public static event EventHandler<OnTickEventArgs> OnTick5;

    private const float TickTimerMax = 0.2f;
    private int Tick;
    private float TickTimer;

    void Awake() {
        Tick = 0;
    }

    void Update() {
        TickTimer += Time.deltaTime;

        if (TickTimer >= TickTimerMax ) {
            TickTimer -= TickTimerMax;
            Tick++;

            OnTick?.Invoke(this, new OnTickEventArgs { Tick = Tick });

            if (Tick % 5  == 0 ) OnTick5?.Invoke(this, new OnTickEventArgs { Tick = Tick });
        }
    }
}