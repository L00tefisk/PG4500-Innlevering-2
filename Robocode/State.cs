namespace PG4500_2015_Innlevering2.Robocode
{
	/// <summary>
	/// Base class for states in the FSM. Should be inherited from.
	/// version: 1.0
	/// author: Tomas Sandnes - santom@westerdals.no
	/// </summary>
	public abstract class State
	{
		// P R I V A T E / P R O T E C T E D   V A R S
		// -------------------------------------------

		protected AdvancedRobotEx Robot;


		// P R O P E R T I E S
		// -------------------

		public string Id { get; private set; }
		public bool IsDeferred { get; private set; }

		// P U B L I C   M E T H O D S 
		// ---------------------------

		/// <summary>
		/// Constructor.
		/// </summary>
		protected State(string stateName)
		{
			Id = stateName;
		}


		/// <summary>
		/// Setting robot reference - should be called by owning FSM.
		/// </summary>
		public virtual void Init(AdvancedRobotEx ourRobot)
		{
			Robot = ourRobot;
		}


		/// <summary>
		/// Called repeatedly during Update() in the "owning" StateMachine, as long as there are queued states.
		/// </summary>
		public virtual bool DoTransition(string nextStateId)
		{
			if (Id != nextStateId) {
				return true;
			}
			return false;
		}

	
		/// <summary>
		/// Called once when we transition into this state.
		/// </summary>
		public virtual void EnterState() 
		{
			Robot.Out.WriteLine("{0,6} [{1}] entered.", Robot.Time, Id);
		}


		/// <summary>
		/// Called once when we transition out of this state.
		/// </summary>
		public virtual void ExitState()
		{
			// Intentionally left blank.
		}


		/// <summary>
		/// Called once for every Update() in the "owning" StateMachine, as long as this state is queued or it is the active one with an empty queue.
		/// </summary>
		public abstract string ProcessState();
	}
}
