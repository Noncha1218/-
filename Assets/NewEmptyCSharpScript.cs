using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock; // DualSense含む

public class HeartbeatHaptics : MonoBehaviour
{
    [Header("心拍設定")]
    public float bpm = 70f; // 心拍数 (BPM)

    private Gamepad gamepad;
    private float timer = 0f;
    private bool isBeating = false;

    void Start()
    {
        gamepad = Gamepad.current;
    }

    void Update()
    {
        if (gamepad == null) return;

        float beatInterval = 60f / bpm; // 1拍の間隔（秒）
        timer += Time.deltaTime;

        // 心臓の「ドクン」は2段階振動（lub-dub）
        if (timer >= beatInterval)
        {
            timer = 0f;
            StartCoroutine(HeartbeatPulse());
        }
    }

    System.Collections.IEnumerator HeartbeatPulse()
    {
        // --- 第1音（lub：強め）---
        gamepad.SetMotorSpeeds(1f, 0f); // 低周波, 高周波
        yield return new WaitForSeconds(0.06f);
        gamepad.SetMotorSpeeds(0f, 0f);

        yield return new WaitForSeconds(0.05f);

        // --- 第2音（dub：弱め）---
        gamepad.SetMotorSpeeds(0f, 0.05f);
        yield return new WaitForSeconds(0.04f);
        gamepad.SetMotorSpeeds(0f, 0f);
    }

    void OnDisable()
    {
        gamepad?.SetMotorSpeeds(0f, 0f);
    }
}