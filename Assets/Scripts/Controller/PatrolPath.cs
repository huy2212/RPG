using UnityEngine;

namespace RPG.Control
{
	public class PatrolPath : MonoBehaviour
	{
		[SerializeField] private float waypointGizmosRadius = .3f;

		void OnDrawGizmos()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				int j = GetNextWayPointIndex(i);
				Gizmos.DrawSphere(GetWaypoint(i), waypointGizmosRadius);
				Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
			}
		}

		public int GetNextWayPointIndex(int i)
		{
			if (i + 1 == transform.childCount)
			{
				return 0;
			}
			return i + 1;
		}

		public Vector3 GetWaypoint(int i)
		{
			return transform.GetChild(i).position;
		}
	}
}