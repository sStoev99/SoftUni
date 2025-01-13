using FootballManager.Core.Contracts;
using FootballManager.Models;
using FootballManager.Models.Contracts;
using FootballManager.Repositories;
using FootballManager.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Core
{
    public class Controller : IController
    {
        private TeamRepository championship;
        private string[] managerTypes = new string[]
        {
            typeof(AmateurManager).Name,
            typeof(SeniorManager).Name,
            typeof(ProfessionalManager).Name
        };
        public Controller()
        {
            this.championship = new TeamRepository();
        }

      
        public string ChampionshipRankings()
        {
            StringBuilder result = new StringBuilder();

            List<ITeam> teams = championship.Models
                .OrderByDescending(t => t.ChampionshipPoints)
                .ThenByDescending(t => t.PresentCondition).ToList();

            result.AppendLine("***Ranking Table***");

            int row = 1;
            foreach (var team in teams)
            {
                result.AppendLine($"{row++}. {team}/{team.TeamManager}");
            }

            return result.ToString();
        }
        

        public string JoinChampionship(string teamName)
        {
            if (championship.Capacity == championship.Models.Count)

                return OutputMessages.ChampionshipFull;

            if (championship.Exists(teamName))

                return string.Format(OutputMessages.TeamWithSameNameExisting, teamName);

            Team team = new Team(teamName);
            championship.Add(team);
            return String.Format(OutputMessages.TeamSuccessfullyJoined, teamName);
        }

        public string MatchBetween(string teamOneName, string teamTwoName)
        {
            if (!championship.Exists(teamOneName) || !championship.Exists(teamTwoName))

                return OutputMessages.OneOfTheTeamDoesNotExist;

            ITeam teamOne = championship.Get(teamOneName);
            ITeam teamTwo = championship.Get(teamTwoName);

            ITeam winningTeam = teamOne;
            ITeam losingTeam = teamTwo;

            if (teamOne.PresentCondition < teamTwo.PresentCondition)
            {

                winningTeam = teamTwo;
                losingTeam = teamOne;
            }

            else if (teamTwo.PresentCondition == teamOne.PresentCondition)
            {

                teamOne.GainPoints(1);
                teamTwo.GainPoints(1);

                return String.Format(OutputMessages.MatchIsDraw, teamOne, teamTwo);
            }
            winningTeam.GainPoints(3);

            if (winningTeam.TeamManager != null)

                winningTeam.TeamManager.RankingUpdate(5);

            if (losingTeam.TeamManager != null)

                losingTeam.TeamManager.RankingUpdate(-5);

            return String.Format(OutputMessages.TeamWinsMatch, winningTeam.Name, losingTeam.Name);


        }

        public string SignManager(string teamName, string managerTypeName, string managerName)
        {
            if (!championship.Exists(teamName))
                return String.Format(OutputMessages.TeamDoesNotTakePart, teamName);

            if (!managerTypes.Contains(managerTypeName))
                return String.Format(OutputMessages.ManagerTypeNotPresented, managerTypeName);

            ITeam team = championship.Get(teamName);

            if (team.TeamManager != null)
            {
                return String.Format(OutputMessages.TeamSignedWithAnotherManager, teamName, team.TeamManager.Name);
            }

            foreach (var currentTeam in championship.Models)
            {
                if (currentTeam.TeamManager?.Name == managerName)

                    return String.Format(OutputMessages.ManagerAssignedToAnotherTeam, managerName);

            }

            IManager manager = null;

            if (managerTypeName == typeof(AmateurManager).Name)
            {
                manager = new AmateurManager(managerName);
            }

            if (managerTypeName == typeof(SeniorManager).Name)
            {
                manager = new SeniorManager(managerName);
            }

            if (managerTypeName == typeof(ProfessionalManager).Name)
            {
                manager = new ProfessionalManager(managerName);
            }

            team.SignWith(manager);

            return String.Format(OutputMessages.TeamSuccessfullySignedWithManager, managerName, teamName);

        }
        public string PromoteTeam(string droppingTeamName, string promotingTeamName, string managerTypeName, string managerName)
        {
            if(!championship.Exists(droppingTeamName))

                    return String.Format(OutputMessages.DroppingTeamDoesNotExist, droppingTeamName);

            if(championship.Exists(promotingTeamName))

                return String.Format(OutputMessages.TeamWithSameNameExisting, promotingTeamName);

            ITeam team = new Team(promotingTeamName);

            if (managerTypeName.Contains(managerTypeName))
            {
                bool hasManagerSighed =false;
                foreach (var currentTeam in championship.Models)
                {
                    if(currentTeam.TeamManager?.Name == managerName)
                        hasManagerSighed = true;


                }

                if (!hasManagerSighed)
                {
                    IManager manager = null;

                    if (managerTypeName == typeof(AmateurManager).Name)
                    {
                        manager = new AmateurManager(managerName);
                    }

                    if (managerTypeName == typeof(SeniorManager).Name)
                    {
                        manager = new SeniorManager(managerName);
                    }

                    if (managerTypeName == typeof(ProfessionalManager).Name)
                    {
                        manager = new ProfessionalManager(managerName);
                    }

                    team.SignWith(manager);

                }
                
            }
           foreach (var currentTeam in championship.Models)
            {
                currentTeam.ResetPoints();
            }

           championship.Remove(droppingTeamName);
           championship.Add(team);

            return String.Format(OutputMessages.TeamHasBeenPromoted, promotingTeamName);
        }
    
        
    }
}
