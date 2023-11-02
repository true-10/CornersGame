using System.Collections.Generic;
using System.Linq;

namespace Corners
{
    public sealed class StateManager
    {
        private List<IState> statesList;
        private StateFabric stateFabric;

        public StateManager(StateFabric stateFabric)
        {
            this.stateFabric = stateFabric;
            var states = stateFabric.GenerateStates();
            Init(states);
        }

        public void Init(List<IState> statesList)
        {
            this.statesList = statesList;
        }

        public IState GetStateByType(int stateType)
        {
            return statesList.FirstOrDefault(x => x.Type == stateType);
        }
    }
}

