﻿using BookingApp.Data;
using BookingApp.Data.Models;
using BookingApp.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class ResourcesRepository 
        : ActEntityRepoBase<Resource, int, string>,
        IBasicRepositoryAsync<Resource, int>
    {
        public ResourcesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task UpdateAsync(Resource resource) => await UpdateSelectiveAsync<ResourceUpdateSubsetDto>(resource);

        /// <summary>
        /// Lists all resources adhering to the specified rule.
        /// </summary>
        public async Task<IEnumerable<Resource>> ListByRuleAsync(int ruleId) => await Entities.Where(r => r.RuleId == ruleId).ToListAsync();
    }
}