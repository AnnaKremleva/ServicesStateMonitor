using System.Collections.Generic;

namespace ServicesStateMonitor.Models
{
    public class Mapper
    {
        private readonly List<Service> _currentServices;
        private readonly int[][] _dependentsMap;

        public Mapper(List<Service> currentServices)
        {
            _currentServices = currentServices;
            _dependentsMap = BuildDependentsMap();
        }

        public IEnumerable<int> FindDependentIndexes(Service startService)
        {
            var result = new List<int>();

            if (_currentServices.Contains(startService))
            {
                var visited = new int[_currentServices.Count];
                var selected = new Stack<int>();
                int startIndex = _currentServices.IndexOf(startService);

                selected.Push(startIndex);
                while (selected.TryPop(out int top))
                {
                    for (int dependent = 0; dependent < _dependentsMap[top].Length; dependent++)
                    {
                        int current = _dependentsMap[top][dependent];
                        if (visited[current] != 1)
                        {
                            visited[current] = 1;
                            selected.Push(current);
                            result.Add(current);
                        }
                    }
                }

                //see depth first search algorithm
            }
            return result;
        }

        private int[][] BuildDependentsMap()
        {
            var map = new int[_currentServices.Count][];
            for (int serviceNumber = 0; serviceNumber < _currentServices.Count; serviceNumber++)
            {
                var currentDependents = GetDependents(serviceNumber);
                map[serviceNumber] = new int[currentDependents.Count];
                for (int dependentNumber = 0; dependentNumber < currentDependents.Count; dependentNumber++)
                {
                    map[serviceNumber][dependentNumber] = _currentServices.IndexOf(currentDependents[dependentNumber]);
                }
            }
            return map;
        }

        private List<Service> GetDependents(int index)
            => _currentServices
                .FindAll(dependentService
                    => _currentServices[index].Dependents.Contains(dependentService));
    }
}