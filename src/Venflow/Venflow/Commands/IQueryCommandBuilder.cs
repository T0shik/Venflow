﻿using System.Threading;
using System.Threading.Tasks;

namespace Venflow.Commands
{
    /// <summary>
    /// Represents a command builder to configure the query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity which will be queried.</typeparam>
    /// <typeparam name="TReturn">The return type of the query.</typeparam>
    public interface IQueryCommandBuilder<TEntity, TReturn> : ISpecficVenflowCommandBuilder<IQueryCommand<TEntity, TReturn>, IBaseQueryRelationBuilder<TEntity, TEntity, TReturn>>
        where TEntity : class, new()
        where TReturn : class, new()
    {
        /// <summary>
        /// Determines whether or not to return change tracked entities from the query.
        /// </summary>
        /// <param name="trackChanges">Determines if change tracking should be applied.</param>
        /// <returns>An object that can be used to further configure the operation.</returns>
        IBaseQueryRelationBuilder<TEntity, TEntity, TReturn> TrackChanges(bool trackChanges = true);

        /// <summary>
        /// Asynchronously performs queries and materializes the result.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, which is used to cancel the operation</param>
        /// <returns>A task representing the asynchronous operation, with the materialized result of the query; <see langword="null"/> otherwise.</returns>
        Task<TReturn?> QueryAsync(CancellationToken cancellationToken = default);
    }
}