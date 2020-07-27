﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Dapper;
using Microsoft.EntityFrameworkCore;
using RepoDb;
using RepoDb.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Venflow.Benchmarks.Benchmarks.Models;

namespace Venflow.Benchmarks.Benchmarks.QueryBenchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [RPlotExporter]
    public class QueryBatchAsyncBenchmark : BenchmarkBase
    {
        [Params(10, 100, 1000, 10000)]
        public int QueryCount { get; set; }

        private string sql => @"SELECT ""Id"", ""Name"" FROM ""People"" LIMIT " + QueryCount;

        [GlobalSetup]
        public override async Task Setup()
        {
            await base.Setup();

            await EfCoreQueryBatchAsync();
            await EfCoreQueryBatchNoChangeTrackingAsync();
            await EfCoreQueryBatchRawNoChangeTrackingAsync();
            await VenflowQueryBatchAsync();
            await VenflowQueryBatchNoChangeTrackingAsync();
            await RepoDbQueryBatchAsync();
            await DapperQueryBatchAsync();
        }

        [Benchmark(Baseline = true)]
        public Task<List<Person>> EfCoreQueryBatchAsync()
        {
            return PersonDbContext.People.Take(QueryCount).ToListAsync();
        }

        [Benchmark]
        public Task<List<Person>> EfCoreQueryBatchNoChangeTrackingAsync()
        {
            return PersonDbContext.People.Take(QueryCount).AsNoTracking().ToListAsync();
        }

        [Benchmark]
        public Task<List<Person>> EfCoreQueryBatchRawNoChangeTrackingAsync()
        {
            return PersonDbContext.People.FromSqlRaw(sql).AsNoTracking().ToListAsync();
        }

        [Benchmark]
        public Task<List<Person>> VenflowQueryBatchAsync()
        {
            return Configuration.People.QueryBatch(sql).TrackChanges().Build().QueryAsync();
        }

        [Benchmark]
        public Task<List<Person>> VenflowQueryBatchNoChangeTrackingAsync()
        {
            return Configuration.People.QueryBatch(sql).Build().QueryAsync();
        }

        [Benchmark]
        public async Task<List<Person>> RepoDbQueryBatchAsync()
        {
            return EnumerableExtension.AsList(await DbConnectionExtension.QueryAsync<Person>(Configuration.GetConnection(), whereOrPrimaryKey: null, top: QueryCount));
        }

        [Benchmark]
        public async Task<List<Person>> DapperQueryBatchAsync()
        {
            return SqlMapper.AsList(await SqlMapper.QueryAsync<Person>(Configuration.GetConnection(), sql));
        }
        [GlobalCleanup]
        public override Task Cleanup()
        {
            return base.Cleanup();
        }
    }
}