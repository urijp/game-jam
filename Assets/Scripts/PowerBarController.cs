using UnityEngine;
using UnityEngine.UI;

public class PowerBarController : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void setPower(float power)
    {
        slider.value = power;
    }
}
