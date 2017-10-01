using KofileAPI.DomainModels;
using System.Collections.Generic;

namespace KofileAPI
{
    public interface IFeesAndDistributionsService
    {
        IEnumerable<FeesAndDistributions> FeesAndDistributions { get; }
    }
    public class FeesAndDistributionService : IFeesAndDistributionsService
    {
        public IEnumerable<FeesAndDistributions> FeesAndDistributions { get; }

        public FeesAndDistributionService() 
            => FeesAndDistributions = CreateFeesAndDistributions();
       
        #region Helper functions

        private IEnumerable<FeesAndDistributions> CreateFeesAndDistributions()
        {
            return new List<FeesAndDistributions>
            {
                new FeesAndDistributions
                {
                    OrderItemType = "Real Property Recording",
                    Fees = new List<Fee>
                    {
                        new Fee
                        {
                            Name = "Recording (first page)",
                            Amount = 26.00m,
                            Type = "flat"
                        },
                        new Fee
                        {
                            Name = "Recording (additional pages)",
                            Amount = 1.00m,
                            Type = "per-page"
                        }
                    },
                    Distributions = new List<Distribution>
                    {
                        new Distribution
                        {
                            Name = "Recording Fee",
                            Amount = 5.00m
                        },
                        new Distribution
                        {
                            Name = "Records Management and Preservation Fee",
                            Amount = 10.00m
                        },
                        new Distribution
                        {
                            Name = "Records Archive Fee",
                            Amount = 10.00m
                        },
                        new Distribution
                        {
                            Name = "Courthouse Security",
                            Amount = 1.00m
                        }
                    }
                },
                new FeesAndDistributions
                {
                    OrderItemType = "Birth Certificate",
                    Fees = new List<Fee>
                    {
                        new Fee
                        {
                            Name = "Birth Certificate Fees",
                            Amount = 23.00m,
                            Type = "flat"
                        }
                    },
                    Distributions = new List<Distribution>
                    {
                        new Distribution
                        {
                            Name = "County Clerk Fee",
                            Amount = 20.00m
                        },
                        new Distribution
                        {
                            Name = "Vital Statistics Fee",
                            Amount = 1.00m
                        },
                        new Distribution
                        {
                            Name = "Vital Statistics Preservation Fee",
                            Amount = 1.00m
                        }
                    }
                }
            };            
        }

        #endregion
    }
}
