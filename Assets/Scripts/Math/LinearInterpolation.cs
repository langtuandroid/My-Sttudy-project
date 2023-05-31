using UnityEngine;

namespace Math
{
    public class LinearInterpolation : MonoBehaviour
    {
        [SerializeField] private Vector3 mStartPos;
        [SerializeField] private Vector3 mEndPos;
        [SerializeField] private float mLerpSpeed;
        [SerializeField] private GameObject mSpare;

        private float _time;

        private void Start()
        {
            mSpare.transform.position = mStartPos;
        }

        private void Update()
        {
            _time += mLerpSpeed * Time.deltaTime;
            _time = Mathf.Clamp(_time, 0f, 1f);
            
            mSpare.transform.position = Vector3.Lerp(mStartPos, mEndPos, _time);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(mStartPos, 0.2f);
            
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(mEndPos, 0.2f);
        }
    }
}
