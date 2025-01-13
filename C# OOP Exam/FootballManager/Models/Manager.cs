using FootballManager.Models.Contracts;
using FootballManager.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Models
{
    public abstract class Manager : IManager
    {
        private string name;
        private double ranking;

        protected Manager(string name, double ranking)
        {
            Name = name;
            Ranking = ranking;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ManagerNameNull);
                }
                name = value;
            }
        }

        public double Ranking
        {
            get => ranking;
            protected set => ranking = Math.Clamp(value, 0.00, 100.00);
        }

        public abstract void RankingUpdate(double updateValue);

        public override string ToString()
        {
            return $"{Name} - {GetType().Name} (Ranking: {Ranking:F2})";
        }
    }
}