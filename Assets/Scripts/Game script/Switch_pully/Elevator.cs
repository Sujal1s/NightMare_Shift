using UnityEngine;

using Cainos.LucidEditor;
using Cainos.Common;

namespace Cainos.PixelArtPlatformer_Dungeon
{
    public class Elevator : MonoBehaviour
    {
        [FoldoutGroup("Params")] public Vector2 lengthRange = new Vector2(2, 5);
        [FoldoutGroup("Params")] public float waitTime = 1.0f;
        [FoldoutGroup("Params")] public float moveSpeed = 3.0f;
        [FoldoutGroup("Params")] public State startState = State.Up;

        [FoldoutGroup("Reference")] public Rigidbody2D platform;
        [FoldoutGroup("Reference")] public SpriteRenderer chainL;
        [FoldoutGroup("Reference")] public SpriteRenderer chainR;

        [FoldoutGroup("Runtime"), ShowInInspector]
        public float Length
        {
            get { return length; }
            set
            {
                if (value < 0) value = 0.0f;
                this.length = value;

                platform.transform.localPosition = new Vector3(0.0f, -value, 0.0f);
                chainL.size = new Vector2(0.09375f, value + chainLengthOffset);
                chainR.size = new Vector2(0.09375f, value + chainLengthOffset);
            }
        }
        private float length;

        [FoldoutGroup("Runtime"), ShowInInspector]
        public State CurState
        {
            get { return curState; }
            set
            {
                curState = value;
            }
        }
        private State curState;


        [FoldoutGroup("Runtime"), ShowInInspector]
        public bool IsWaiting
        {
            get { return isWaiting; }
            set
            {
                if (isWaiting == value) return;
                isWaiting = value;
                waitTimer = 0.0f;
            }
        }
        private bool isWaiting = false;

        // New property to control if the elevator is active (moving) or not
        [FoldoutGroup("Runtime"), ShowInInspector]
        public bool IsActive 
        { 
            get { return isActive; }
            set { isActive = value; }
        }
        private bool isActive = false;

        private float waitTimer;
        private float curSpeed;
        private float targetLength;
        private float chainLengthOffset;
        private SecondOrderDynamics secondOrderDynamics = new SecondOrderDynamics(4.0f, 0.3f, -0.3f);


        private void Start()
        {
            chainLengthOffset = chainL.GetComponent<SpriteRenderer>().size.y + platform.transform.localPosition.y;

            curState = startState;
            Length = curState == State.Up ? lengthRange.y : lengthRange.x;
            targetLength = Length;

            secondOrderDynamics.Reset(targetLength);
        }

        private void Update()
        {
            // Only process elevator movement if it's active
            if (!IsActive)
                return;
                
            if (IsWaiting)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer > waitTime) IsWaiting = false;
                curSpeed = 0.0f;
            }
            else
            {
                if (curState == State.Up)
                {
                    curSpeed = -moveSpeed;
                    if (targetLength < lengthRange.x)
                    {
                        curState = State.Down;
                        IsWaiting = true;
                    }
                }
                else if (curState == State.Down)
                {
                    curSpeed = moveSpeed;
                    if (targetLength > lengthRange.y)
                    {
                        curState = State.Up;
                        IsWaiting = true;
                    }
                }
            }

            targetLength += curSpeed * Time.deltaTime;
        }

        private void FixedUpdate()
        {
            // Only update physics if elevator is active
            if (IsActive)
            {
                Length = secondOrderDynamics.Update(targetLength, Time.fixedDeltaTime);
            }
        }

        // Method to control elevator state - can be called from other scripts
        public void SetActiveState(bool active)
        {
            IsActive = active;
        }

        public enum State
        {
            Up,
            Down
        }
    }
}