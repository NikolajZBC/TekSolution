using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CityController : MonoBehaviour
{
//    [SerializeField] float energyRecieved = 0f;
    [SerializeField] float usagePerHourPerInhapitant = 0.18f;
    [SerializeField] float cityPowerUsagePerHour = 0f;
    public int cityPopulation = 0;
    [SerializeField] float balance = 0f;
    [Range(0f, 1f)]
    public float allowedPowerUsagePercentage = 1f;
    
    [SerializeField] Slider powerUsageSlider;
    [Range(0f, 100f)]
    [SerializeField] float happiness = 100f;
    [SerializeField] string cityName = "";
    [SerializeField] TMP_Text cityNameTextBox;
    [SerializeField] float maxHappinessReductionPerSec = 2f;
    [SerializeField] Emotion_Change emotionIcon;
    public float gridPercentage;
    int happinessMid = 66;
    bool reachedMid = false;
    int happinessLow = 33;
    bool reachedLow = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cityPowerUsagePerHour = cityPopulation * usagePerHourPerInhapitant;
        cityNameTextBox.text = cityName;
    }

    // Update is called once per physics update
    void CalculateBalance(float energyRecived)
    {
//        print("Energy recived: " + energyRecived.ToString("00.00"));
//        print(cityName + " Energy usage: " + (cityPowerUsagePerHour * allowedPowerUsagePercentage).ToString("00.00")); 
        balance = energyRecived - cityPowerUsagePerHour;
//        print(cityName + " Balance: " + balance.ToString("00.00"));
        if (balance < 0f)
        {
            happiness -= Time.fixedDeltaTime * Mathf.Clamp(Mathf.Abs(balance), 1, maxHappinessReductionPerSec);
            if (happiness < happinessMid && !reachedMid) { 
                reachedMid = true;
                emotionIcon.NextIcon();
            }
            if (happiness < happinessLow && !reachedLow) {
                reachedLow = true;
                emotionIcon.NextIcon();
            }
            if (happiness <= 0) {
                SystemManager.instance.GameOver();
            }
        }
    }
    public void RecievedEnergy(float energy) {
        CalculateBalance(energy);

    }
    public float GetCurrentAllowedUsage() {
        allowedPowerUsagePercentage = powerUsageSlider.value / 100f;
        return (cityPowerUsagePerHour * allowedPowerUsagePercentage);
    }
}