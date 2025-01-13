using FootballManager.Models.Contracts;
using FootballManager.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Repositories
{
    public class TeamRepository : IRepository<ITeam>
    {
        private readonly List<ITeam> teams;

        public TeamRepository()
        {
            teams = new List<ITeam>();
        }

        public IReadOnlyCollection<ITeam> Models => teams.AsReadOnly();

        public int Capacity => 10;

        public void Add(ITeam team)
        {
            if (teams.Count >= Capacity)
            {
                return; // Exceeding capacity is ignored.
            }

            teams.Add(team);
        }

        public bool Remove(string name)
        {
            var team = teams.FirstOrDefault(t => t.Name == name);
            if (team != null)
            {
                teams.Remove(team);
                return true;
            }
            return false;
        }

        public bool Exists(string name)
        {
            return teams.Any(t => t.Name == name);
        }

        public ITeam Get(string name)
        {
            return teams.FirstOrDefault(t => t.Name == name);
        }
    }
}