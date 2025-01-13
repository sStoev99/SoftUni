using System.Linq;
using FootballManager.Core.Contracts;
using FootballManager.Models;
using FootballManager.Models.Contracts;
using FootballManager.Repositories;
using FootballManager.Utilities.Messages;

namespace FootballManager.Core
{
    public class Controller : IController
    {
        private readonly TeamRepository championship;

        public Controller()
        {
            championship = new TeamRepository();
        }

        public string JoinChampionship(string teamName)
        {
            if (championship.Models.Count >= championship.Capacity)
                return OutputMessages.ChampionshipFull;

            if (championship.Exists(teamName))
                return string.Format(OutputMessages.TeamWithSameNameExisting, teamName);

            var team = new Team(teamName);
            championship.Add(team);

            return string.Format(OutputMessages.TeamSuccessfullyJoined, teamName);
        }

        public string SignManager(string teamName, string managerTypeName, string managerName)
        {
            var team = championship.Get(teamName);

            if (team == null)
                return string.Format(OutputMessages.TeamDoesNotTakePart, teamName);

            if (team.TeamManager != null)
                return string.Format(OutputMessages.TeamSignedWithAnotherManager, teamName, team.TeamManager.Name);

            if (!IsValidManagerType(managerTypeName))
                return string.Format(OutputMessages.ManagerTypeNotPresented, managerTypeName);

            if (championship.Models.Any(t => t.TeamManager?.Name == managerName))
                return string.Format(OutputMessages.ManagerAssignedToAnotherTeam, managerName);

            IManager manager = CreateManager(managerTypeName, managerName);
            team.SignWith(manager);

            return string.Format(OutputMessages.TeamSuccessfullySignedWithManager, managerName, teamName);
        }

        public string MatchBetween(string teamOneName, string teamTwoName)
        {
            var teamOne = championship.Get(teamOneName);
            var teamTwo = championship.Get(teamTwoName);

            if (teamOne == null || teamTwo == null)
                return OutputMessages.OneOfTheTeamDoesNotExist;

            if (teamOne.PresentCondition > teamTwo.PresentCondition)
            {
                HandleMatchResult(teamOne, teamTwo, 3, -5);
                return string.Format(OutputMessages.TeamWinsMatch, teamOneName, teamTwoName);
            }
            else if (teamOne.PresentCondition < teamTwo.PresentCondition)
            {
                HandleMatchResult(teamTwo, teamOne, 3, -5);
                return string.Format(OutputMessages.TeamWinsMatch, teamTwoName, teamOneName);
            }
            else
            {
                teamOne.GainPoints(1);
                teamTwo.GainPoints(1);
                return string.Format(OutputMessages.MatchIsDraw, teamOneName, teamTwoName);
            }
        }

        public string PromoteTeam(string droppingTeamName, string promotingTeamName, string managerTypeName, string managerName)
        {
            var droppingTeam = championship.Get(droppingTeamName);

            if (droppingTeam == null)
                return string.Format(OutputMessages.DroppingTeamDoesNotExist, droppingTeamName);

            if (championship.Exists(promotingTeamName))
                return string.Format(OutputMessages.TeamWithSameNameExisting, promotingTeamName);

            var promotingTeam = new Team(promotingTeamName);

            if (IsValidManagerType(managerTypeName) && !championship.Models.Any(t => t.TeamManager?.Name == managerName))
            {
                IManager manager = CreateManager(managerTypeName, managerName);
                promotingTeam.SignWith(manager);
            }

            foreach (var team in championship.Models)
                team.ResetPoints();

            championship.Remove(droppingTeamName);
            championship.Add(promotingTeam);

            return string.Format(OutputMessages.TeamHasBeenPromoted, promotingTeamName);
        }

        public string ChampionshipRankings()
        {
            var orderedTeams = championship.Models
                .OrderByDescending(t => t.ChampionshipPoints)
                .ThenByDescending(t => t.PresentCondition)
                .Select((team, index) => $"{index + 1}. {team}/{team.TeamManager}");

            return $"***Ranking Table***\n{string.Join("\n", orderedTeams)}";
        }

        private bool IsValidManagerType(string managerTypeName)
        {
            return managerTypeName == nameof(AmateurManager) ||
                   managerTypeName == nameof(SeniorManager) ||
                   managerTypeName == nameof(ProfessionalManager);
        }

        private IManager CreateManager(string managerTypeName, string managerName)
        {
            return managerTypeName switch
            {
                nameof(AmateurManager) => new AmateurManager(managerName),
                nameof(SeniorManager) => new SeniorManager(managerName),
                nameof(ProfessionalManager) => new ProfessionalManager(managerName),
                _ => throw new InvalidOperationException("Invalid manager type.")
            };
        }

        private void HandleMatchResult(ITeam winner, ITeam loser, int winnerPoints, int loserPenalty)
        {
            winner.GainPoints(winnerPoints);
            winner.TeamManager?.RankingUpdate(5);

            loser.TeamManager?.RankingUpdate(loserPenalty);
        }
    }
}
