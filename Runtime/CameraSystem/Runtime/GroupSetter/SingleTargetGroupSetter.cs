using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Boshphelm.CameraSystem
{
    public class SingleTargetGroupSetter : TargetGroupSetter
    {  
        private Coroutine _coroutine;   
        public void FocusOnAim(CinemachineVirtualCamera combotCam)
        { 
            CameraSwitcher.Instance.SwitchCamera(combotCam);
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(SmoothSetWeight(1, 1f)); 
        }
        public void FocusOffAim()
        { 
            CameraSwitcher.Instance.SetDefaultCameraWithDefaultBlend();
            if (_coroutine != null) StopCoroutine(_coroutine);
            _cinemachineTargetGroup.m_Targets[1].weight = 0;
        }

        private IEnumerator SmoothSetWeight(float weightTargetValue, float speed = 1)
        {
            float startWeight = _cinemachineTargetGroup.m_Targets[1].weight;
            float timer = 0; 
            while (timer < 1f)
            {
                timer += Time.deltaTime * speed;
                _cinemachineTargetGroup.m_Targets[1].weight = Mathf.Lerp(startWeight, weightTargetValue, timer);
                yield return null;
            }
        }
    }
}
