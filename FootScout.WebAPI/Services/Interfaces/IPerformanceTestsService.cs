﻿namespace FootScout.WebAPI.Services.Interfaces
{
    public interface IPerformanceTestsService
    {
        Task SeedComponents(int testCounter);
        Task ClearDatabaseOfSeededComponents();
    }
}