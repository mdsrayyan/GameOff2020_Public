using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravitySlider : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    Slider slider;
    [SerializeField]
    Gradient gradient;
    [SerializeField] Image fill;
    [SerializeField] Image border;
    [SerializeField] GameObject tickSound;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        tickSound.SetActive(false);
        player = GameObject.Find("Player");
        slider.maxValue = player.GetComponent<Player>().playerData.flipGravityTime;
        slider.value = slider.maxValue;
        fill.color = gradient.Evaluate(1f);

    }

    // Update is called once per frame
    void Update()
    {
        border.color = Color.white;
        slider.value = slider.maxValue-player.GetComponent<Player>().gravityTimer;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if(slider.normalizedValue < 0.25f)
        {
            timer += Time.deltaTime;
            Blink();
            tickSound.SetActive(true);
        }

        else
        {
            tickSound.SetActive(false);
        }
    }

    void Blink()
    {
        if(timer > 0.15f && timer < 0.25f)
        {
            fill.color = new Color(0, 0, 0, 0);
            border.color = new Color(0, 0, 0, 0);
        }
        else if(timer > 0.25f)
        {
            timer = 0f;
            border.color = Color.white;
        }
    }


}
