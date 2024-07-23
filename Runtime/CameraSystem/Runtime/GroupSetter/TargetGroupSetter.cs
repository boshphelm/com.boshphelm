using Cinemachine;
using UnityEngine;
namespace Boshphelm.CameraSystem
{
    public abstract class TargetGroupSetter : MonoBehaviour
    {  
        protected CinemachineTargetGroup _cinemachineTargetGroup; 
        public void Init(CinemachineTargetGroup cinemachineTargetGroup)
        {
            _cinemachineTargetGroup = cinemachineTargetGroup;
        } 
        public virtual void Add(Transform member, float weight, float radius)   
        {        
            if(IsItMember(member)) return;
            _cinemachineTargetGroup.AddMember(member, weight, radius);
        }
        public virtual void Remove(Transform member)
        {        
            if(!IsItMember(member)) return;
            _cinemachineTargetGroup.RemoveMember(member);
        } 

        public virtual void Reset()
        {
            if(_cinemachineTargetGroup.m_Targets.Length <= 1) return;
            for(int i = 1; i < _cinemachineTargetGroup.m_Targets.Length; i++)  
                _cinemachineTargetGroup.RemoveMember(_cinemachineTargetGroup.m_Targets[i].target); 
        }

        protected bool IsItMember(Transform member)
        { 
            if(_cinemachineTargetGroup.m_Targets.Length == 0) return false;
            for(int i = 0; i < _cinemachineTargetGroup.m_Targets.Length; i++) 
            {
                if(_cinemachineTargetGroup.m_Targets[i].target.GetInstanceID() != member.GetInstanceID()) continue;
                return true;
            }
            return false;
        }  
    }
}