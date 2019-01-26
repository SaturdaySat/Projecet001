using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.Physics2D)]
	public class SetPhysics2DConstraints : ComponentAction<Rigidbody2D>
	{

        [RequiredField]
        [CheckForComponent(typeof(Rigidbody2D))]
        [Tooltip("The GameObject with the Rigidbody2D attached")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        public FsmBool isConstraintPosX;

        [RequiredField]
        public FsmBool isConstraintPosY;

        [RequiredField]
        public FsmBool isConstraintRotZ;


        // Code that runs on entering the state.
        public override void OnEnter()
		{
            SetConsains();
			Finish();
		}

        private void SetConsains()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go))
            {
                return;
            }

            RigidbodyConstraints2D constraint = RigidbodyConstraints2D.None;
            if (isConstraintPosX.Value)
            {
                constraint |= RigidbodyConstraints2D.FreezePositionX;
            }
            if (isConstraintPosY.Value)
            {
                constraint |= RigidbodyConstraints2D.FreezePositionY;
            }
            if (isConstraintRotZ.Value)
            {
                constraint |= RigidbodyConstraints2D.FreezeRotation;
            }

            rigidbody2d.constraints = constraint;

        }

	}

}
