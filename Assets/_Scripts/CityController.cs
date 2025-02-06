using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CityController : MonoBehaviour
{
    [SerializeField] float energyRecieved = 0f;
    [SerializeField] float usagePerHourPerInhapitant = 0.18f;
    [SerializeField] float cityPowerUsagePerHour = 0f;
    [SerializeField] int cityPopulation = 0;
    [SerializeField] float balance = 0f;
    [Range(0f, 100f)]
    [SerializeField] float allowedPowerUsage = 100f;
    [SerializeField] Slider powerUsageSlider;
    [Range(0f, 100f)]
    [SerializeField] float happiness = 100f;
    [SerializeField] string cityName = "";
    [SerializeField] TMP_Text cityNameTextBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cityPowerUsagePerHour = cityPopulation * usagePerHourPerInhapitant;
        cityNameTextBox.text = cityName;
    }

    // Update is called once per physics update
    void FixedUpdate()
    {
        allowedPowerUsage = powerUsageSlider.value / 100f;
        balance = (cityPowerUsagePerHour * allowedPowerUsage) - energyRecieved;

        if (balance < 0f)
        {
            happiness -= Mathf.Clamp(Mathf.Abs(balance), 0, 0.1f);
        }
    }
}