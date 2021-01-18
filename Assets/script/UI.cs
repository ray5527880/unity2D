using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Text UIString;

    public float MaxValue;

    private float curenValue;
    [SerializeField]
    public Text Text;

    private float curenFull;

    public float MyCurenValue
    {
        get
        {
            return curenValue;
        }
        set
        {
            if (value > MaxValue)
                curenValue = MaxValue;
            else if (value < 0)
                curenValue = 0;
            else
                curenValue = value;
            curenFull = curenValue / MaxValue;
        }
        
    }

    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        //if (image.GetComponentInChildren<Text>() != null)
        //{
        //    UIString = image.GetComponentInChildren<Text>();
        //}
        MyCurenValue = MaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Text != null)
        {
            Text.text = MyCurenValue.ToString() + "/" + MaxValue.ToString();
        }
        if (curenFull != image.fillAmount)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, curenFull, Time.deltaTime);
           
        }
    }
}
