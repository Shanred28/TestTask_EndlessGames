using TMPro;
using UniRx;
using UnityEngine;

public class UI_PanelValue : MonoBehaviour
{
    [SerializeField] private FloatReactiveProperty _speed = new FloatReactiveProperty();
    public FloatReactiveProperty Speed => _speed;
    [SerializeField] private FloatReactiveProperty _distance = new FloatReactiveProperty();
    public FloatReactiveProperty Distance => _distance;
    [SerializeField] private FloatReactiveProperty _time = new FloatReactiveProperty();
    public FloatReactiveProperty Time => _time;



    [SerializeField] private TMP_InputField _InputFieldSpeed;
    [SerializeField] private TMP_InputField _InputFieldDistance;
    [SerializeField] private TMP_InputField _InputFieldTimeSpawn;

    private void Start()
    {
        _InputFieldSpeed.text = "10";
        _InputFieldDistance.text = "100";
        _InputFieldTimeSpawn.text = "2";

        _InputFieldSpeed.onValueChanged.AddListener(delegate { ValueCheck(_InputFieldSpeed,_InputFieldSpeed.text); });
        _InputFieldDistance.onValueChanged.AddListener(delegate { ValueCheck(_InputFieldDistance, _InputFieldSpeed.text); });
        _InputFieldTimeSpawn.onValueChanged.AddListener(delegate { ValueCheck(_InputFieldTimeSpawn, _InputFieldSpeed.text); });

        _InputFieldSpeed.onSubmit.AddListener(delegate { SetValue(_InputFieldSpeed, _speed, _InputFieldSpeed.text);  });
        _InputFieldDistance.onSubmit.AddListener(delegate { SetValue(_InputFieldDistance, _distance, _InputFieldDistance.text); });
        _InputFieldTimeSpawn.onSubmit.AddListener(delegate { SetValue(_InputFieldTimeSpawn, _time, _InputFieldTimeSpawn.text); });
       
    }

    //Проверка ввода
    private void ValueCheck(TMP_InputField _input,string text)
    {
        if (text == "")
        {
            _input.text = "1";
        }
    }

    //Применение значения из текста
    private void SetValue(TMP_InputField _input, FloatReactiveProperty value, string text)
    {
        if (text != "0" && text != "")
        {
            value.Value = float.Parse(text);
        }
        else
        {
            value.Value = 1;
            _input.text = value.ToString();
        }
    }
}
