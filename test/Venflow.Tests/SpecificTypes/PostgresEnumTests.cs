﻿using System.Threading.Tasks;
using Venflow.Tests.Models;
using Xunit;

namespace Venflow.Tests.SpecificTypes
{
    public class PostgresEnumTests : TestBase
    {
        [Fact]
        public async Task Query()
        {
            var dummy = new UncommonType
            {
                PostgreEnum = PostgreEnum.Foo
            };

            Assert.Equal(1, await Database.UncommonTypes.InsertAsync(dummy));

            dummy = await Database.UncommonTypes.QueryInterpolatedSingle($@"SELECT * FROM ""UncommonTypes"" WHERE ""PostgreEnum"" = {dummy.PostgreEnum}").Build().QueryAsync();

            Assert.False(dummy.NPostgreEnum.HasValue);

            await Database.UncommonTypes.DeleteAsync(dummy);
        }

        [Fact]
        public async Task QueryNullableValue()
        {
            var dummy = new UncommonType
            {
                NPostgreEnum = PostgreEnum.Foo
            };

            Assert.Equal(1, await Database.UncommonTypes.InsertAsync(dummy));

            dummy = await Database.UncommonTypes.QueryInterpolatedSingle($@"SELECT * FROM ""UncommonTypes"" WHERE ""NPostgreEnum"" = {dummy.NPostgreEnum}").Build().QueryAsync();

            Assert.Equal(PostgreEnum.Foo, dummy.NPostgreEnum);

            await Database.UncommonTypes.DeleteAsync(dummy);
        }

        [Fact]
        public async Task Insert()
        {
            var dummy = new UncommonType
            {
                PostgreEnum = PostgreEnum.Foo
            };

            Assert.Equal(1, await Database.UncommonTypes.InsertAsync(dummy));

            dummy = await Database.UncommonTypes.QueryInterpolatedSingle($@"SELECT * FROM ""UncommonTypes"" WHERE ""Id"" = {dummy.Id}").Build().QueryAsync();

            Assert.Equal(PostgreEnum.Foo, dummy.PostgreEnum);

            await Database.UncommonTypes.DeleteAsync(dummy);
        }

        [Fact]
        public async Task InsertNullable()
        {
            var dummy = new UncommonType
            {
                NPostgreEnum = null
            };

            Assert.Equal(1, await Database.UncommonTypes.InsertAsync(dummy));

            dummy = await Database.UncommonTypes.QueryInterpolatedSingle($@"SELECT * FROM ""UncommonTypes"" WHERE ""Id"" = {dummy.Id}").Build().QueryAsync();

            Assert.False(dummy.NPostgreEnum.HasValue);

            await Database.UncommonTypes.DeleteAsync(dummy);
        }

        [Fact]
        public async Task InsertNullableValue()
        {
            var dummy = new UncommonType
            {
                NPostgreEnum = PostgreEnum.Foo
            };

            Assert.Equal(1, await Database.UncommonTypes.InsertAsync(dummy));

            dummy = await Database.UncommonTypes.QueryInterpolatedSingle($@"SELECT * FROM ""UncommonTypes"" WHERE ""Id"" = {dummy.Id}").QueryAsync();

            Assert.Equal(PostgreEnum.Foo, dummy.NPostgreEnum);

            await Database.UncommonTypes.DeleteAsync(dummy);
        }

        [Fact]
        public async Task Update()
        {
            var dummy = new UncommonType();

            Assert.Equal(1, await Database.UncommonTypes.InsertAsync(dummy));

            Database.UncommonTypes.TrackChanges(ref dummy);

            dummy.PostgreEnum = PostgreEnum.Foo;

            await Database.UncommonTypes.UpdateAsync(dummy);

            dummy = await Database.UncommonTypes.QueryInterpolatedSingle($@"SELECT * FROM ""UncommonTypes"" WHERE ""Id"" = {dummy.Id}").QueryAsync();

            Assert.Equal(PostgreEnum.Foo, dummy.PostgreEnum);

            await Database.UncommonTypes.DeleteAsync(dummy);
        }

        [Fact]
        public async Task UpdateNullable()
        {
            var dummy = new UncommonType
            {
                NPostgreEnum = PostgreEnum.Foo
            };

            Assert.Equal(1, await Database.UncommonTypes.InsertAsync(dummy));

            Database.UncommonTypes.TrackChanges(ref dummy);

            dummy.NPostgreEnum = null;

            await Database.UncommonTypes.UpdateAsync(dummy);

            dummy = await Database.UncommonTypes.QueryInterpolatedSingle($@"SELECT * FROM ""UncommonTypes"" WHERE ""Id"" = {dummy.Id}").QueryAsync();

            Assert.False(dummy.NPostgreEnum.HasValue);

            await Database.UncommonTypes.DeleteAsync(dummy);
        }

        [Fact]
        public async Task UpdateNullableValue()
        {
            var dummy = new UncommonType
            {
                NPostgreEnum = null
            };

            Assert.Equal(1, await Database.UncommonTypes.InsertAsync(dummy));

            Database.UncommonTypes.TrackChanges(ref dummy);

            dummy.NPostgreEnum = PostgreEnum.Foo;

            await Database.UncommonTypes.UpdateAsync(dummy);

            dummy = await Database.UncommonTypes.QueryInterpolatedSingle($@"SELECT * FROM ""UncommonTypes"" WHERE ""Id"" = {dummy.Id}").QueryAsync();

            Assert.Equal(PostgreEnum.Foo, dummy.NPostgreEnum);

            await Database.UncommonTypes.DeleteAsync(dummy);
        }
    }
}
