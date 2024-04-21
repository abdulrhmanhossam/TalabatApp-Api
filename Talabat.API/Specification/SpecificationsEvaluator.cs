﻿using Microsoft.EntityFrameworkCore;
using Talabat.DAL.Entities;

namespace Talabat.API.Specification
{
    public class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity 
    {
        // build Query
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);

            if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);

            query = specification.Includes.
                Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

            return query;
        }
    }
}
