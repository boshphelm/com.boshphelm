using UnityEngine;
using UnityEngine.TestTools;

namespace boshphelm.CameraSystem
{
    public class ThroughWallSync : MonoBehaviour
    {
        public static int PosID = Shader.PropertyToID("_PlayerPosition");
        public static int SizeID = Shader.PropertyToID("_Size");
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _hitLayers;
        private Material _wallMat;
        private Transform _target;

        public void Setup(Transform target, Material material)
        {
            _wallMat = material;
            //Debug.Log("TARGET SETTING : " + target, _target);
            _target = target;
            if (_camera == null) _camera = Camera.main;
            //Debug.Log("CAMERA SETTING : " + _camera, _camera);
            //Debug.Log("DIRECT MATERIAL INSTANCE ID : " + _wallMat.GetInstanceID());
        }

        private void OnApplicationQuit()
        {
            _wallMat.SetFloat(SizeID, 0);
            _wallMat.SetVector(PosID, Vector3.zero);
        }

        private void Update()
        {
            if (_camera == null) return;
            if (_target == null) return;

            Vector3 dir = _camera.transform.position - _target.position;
            Ray ray = new Ray(_target.position, dir.normalized);
            if (Physics.Raycast(ray, 3000, _hitLayers))
            {
                _wallMat.SetFloat(SizeID, .42f);
            }
            else
            {
                _wallMat.SetFloat(SizeID, 0);
            }

            Vector3 view = _camera.WorldToViewportPoint(_target.position);
            _wallMat.SetVector(PosID, view);
        }
    }
}