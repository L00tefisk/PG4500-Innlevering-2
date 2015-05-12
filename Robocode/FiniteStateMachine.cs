using System.Collections.Generic;
using PG4500_2015_Innlevering2.Robocode;

namespace PG4500_2015_Innlevering2.Robocode
{
	/// <summary>
	/// Generic Finite State Machine class, for hardcoded FSMs.
	/// version: 1.0
	/// author: Tomas Sandnes - santom@westerdals.no
	/// </summary>
	public class FiniteStateMachine
	{
		// P R I V A T E / P R O T E C T E D   V A R S
		// -------------------------------------------

		private const int SpinCount = 10;  // Max number of state transitions in one round before we trigger spin.
		private readonly State[] _possibleStates;
		private readonly Queue<State> _transitionQueue;
		private readonly Queue<State> _derefferedTransitionQueue;
		private State _currentState;


		// P U B L I C   M E T H O D S 
		// ---------------------------

		/// <summary>
		/// Constructor.
		/// </summary>
		public FiniteStateMachine(State[] statesToUse)
		{
			_possibleStates = statesToUse;
			_transitionQueue = new Queue<State>();
			_derefferedTransitionQueue = new Queue<State>();
		}


		/// <summary>
		/// Note: Init method MUST be called prior to the first call to Update() !!!
		/// </summary>
		public void Init(AdvancedRobotEx ourRobot) 
		{
			// Init all the states
			foreach (State state in _possibleStates) {
				state.Init(ourRobot);
			}
			// Set the first state in the array as the current state when the FSM is init'ed.
			_currentState = _possibleStates[0];
			_currentState.EnterState();
		}


		/// <summary>
		/// Return current state's Id.
		/// </summary>
		public string GetCurrentStateId()
		{
			return _currentState.Id;
		}


		/// <summary>
		/// Queue up a new state (as long as it is registered as a possible state for this FSM).
		/// </summary>
		public void Queue(string stateId)
		{
			State newState = null;
			foreach (State element in _possibleStates) {
				if (stateId == element.Id) {
					newState = element;
					break;
				}
			}
			if (_currentState.Id != newState.Id)
			{
				if (null != newState)
				{
					if (newState.IsDeferred)
						_derefferedTransitionQueue.Enqueue(newState);
					else
						_transitionQueue.Enqueue(newState);
				}
			}
		}


		/// <summary>
		/// Go through the state queue, processing each queued state once. (If no states are queued, process the current state once instead.)
		/// </summary>
		public void Update()
		{
			State nextState;
			string queueStateId;
			int loopCounter = 0;

			// Process any queued states (in order). If no new states are queued, process the current one.
			do {
				loopCounter++;
				if (SpinCount < loopCounter) {
					break;
				}

				// Swap to next state, if any are queued and current state allows a swap.
				if (0 < _transitionQueue.Count) {
					nextState = _transitionQueue.Dequeue();
					if (_currentState.DoTransition(nextState.Id)) {
						_currentState.ExitState();
						_currentState = nextState;
						_currentState.EnterState();
					}
				}

				// Process the AI action for the current state.
				queueStateId = _currentState.ProcessState();

				// If current AI action triggered a transition, queue it up.
				if (null != queueStateId) {
					Queue(queueStateId);
				}

			} while (0 < _transitionQueue.Count);

			foreach (State s in _derefferedTransitionQueue)
				Queue(s.Id);

			_derefferedTransitionQueue.Clear();
		}
	}
}
