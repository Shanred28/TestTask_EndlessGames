using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UniRx;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    private CompositeDisposable _disposables = new CompositeDisposable();

    private float _distance;
    private float _speed;
    private float _speedDuration;

    private TweenerCore<Vector3, Vector3, VectorOptions> tweener;

    public void DestroyCube()
    {
        _disposables.Clear();
        Destroy(gameObject);
    }

    public void SetParametrs(FloatReactiveProperty speed, FloatReactiveProperty distance)
    {
        
        speed.Subscribe(v =>
        {
            
            _speed = v;

        }).AddTo(_disposables);

        distance.Subscribe(v =>
        {
              _distance = v;
        }).AddTo(_disposables);
        DoMove();
    }

    private void ChangeValue()
    {
        if (_distance != tweener.endValue.z )
            tweener.ChangeEndValue(new Vector3(0, 0, _distance), _speed);

        if (_speed != _speedDuration)
        {
            _speedDuration = _speed;
            tweener.ChangeEndValue(tweener.endValue, _speedDuration);
        }
    }

    public void DoMove()
    {
        _speedDuration = _speed;
        tweener = transform.DOMoveZ(endValue:_distance, _speed).SetEase(Ease.Linear).OnComplete(DestroyCube).SetSpeedBased();

        tweener.OnUpdate(ChangeValue);       
    }
}
