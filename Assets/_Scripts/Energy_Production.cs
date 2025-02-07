using UnityEngine;

public class Energy_Production : MonoBehaviour
{

    public float produktion = 20000f;
    public float variationMin = -300f;
    public float variationMax = 300f;
    public float minProduction = 1000f;
    public float maxProduction = 21000f;

    public float GetProduction() {
        produktion += Random.Range(variationMin, variationMax);
        if (produktion > maxProduction) {
            produktion = maxProduction;
        }
        if (produktion < minProduction) {
            produktion = minProduction;
        }
        return produktion;
    }
}
