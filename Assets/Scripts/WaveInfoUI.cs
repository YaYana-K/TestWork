using UnityEngine;
using TMPro;

public class WaveInfoUI : MonoBehaviour
{
    private TMP_Text waveText;
    private void Start()
    {
        waveText = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        SceneManager.OnWaveChanged += UpdateWaveText;
    }
    private void OnDisable()
    {
        SceneManager.OnWaveChanged -= UpdateWaveText;
    }
    private void UpdateWaveText(int totalWaves, int curWave)
    {
        waveText.text = string.Format("Waves: {0}/{1}", curWave, totalWaves);
    }
}
