using UniRx;
using UnityEngine;

public class SpawnerCube : MonoBehaviour
{
    private CompositeDisposable _disposables = new CompositeDisposable();

    [SerializeField] private MoveCube _prefCube;
    [SerializeField] private UI_PanelValue _panelValue;

    private float _timeSpawn;
    private float _timer;

    private void Start()
    {
        _panelValue.Time.Subscribe(v =>
        {
            _timeSpawn = v;
            _timer = _timeSpawn;
        }).AddTo(_disposables);        
    }
    private void Update()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else
            Spawn();
    }

    private void Spawn()
    {
        _timer = _timeSpawn;
        MoveCube obj = Instantiate(_prefCube,transform.position, Quaternion.identity);
        obj.SetParametrs(_panelValue.Speed, _panelValue.Distance);
    }
}
