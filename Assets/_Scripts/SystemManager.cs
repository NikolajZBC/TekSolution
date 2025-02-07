using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SystemManager : MonoBehaviour
{
    [SerializeField] CityController[] cityControllers;
    [SerializeField] Energy_Production[] energyProductions;
    float recievedEnergy = 0f;
    float cityCombinedUsage = 0f;
    float energyBalance = 0f;
    [SerializeField] TMP_Text balanceText;
    [SerializeField] GameObject gameOverPanel;
    
    public static SystemManager instance;
    bool gameOver = false;

    float percentageDistribution = 5f;

    private void Start() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
        gameOverPanel.SetActive(false);
        float countryPopulation = 0f;
        foreach (CityController city in cityControllers) {
            countryPopulation += city.cityPopulation;
        }
        foreach (CityController city in cityControllers) {
            city.gridPercentage = city.cityPopulation / countryPopulation;
        }
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        if (gameOver) { 
            return;
        }
        
        recievedEnergy = GetProducedEnergy();
        cityCombinedUsage = GetCityUsage();
        energyBalance = recievedEnergy - cityCombinedUsage;
        balanceText.text = energyBalance.ToString("00.00");
        float tempRemainingEnergy = (recievedEnergy + energyBalance);
        float testTemp = 0f;
        foreach (var city in cityControllers) {
            float tempCityDist = (city.allowedPowerUsagePercentage * city.gridPercentage);
            city.RecievedEnergy(tempRemainingEnergy * tempCityDist);
            testTemp += tempRemainingEnergy * tempCityDist;
        }
    }

    float GetProducedEnergy() {
        float sum = 0f;
        foreach (var energyProduction in energyProductions) {
            sum += energyProduction.GetProduction();
        }
        return sum;
    }

    float GetCityUsage() {
        float sum = 0f;
        percentageDistribution = 0f;
        foreach (var cityController in cityControllers) {
            sum += cityController.GetCurrentAllowedUsage();
        }

        return sum; 
    }
    
    public void GameOver() {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }
    public void ReloadGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame() {
        Application.Quit();
    }

}
