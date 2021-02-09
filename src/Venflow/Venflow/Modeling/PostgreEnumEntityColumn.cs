﻿using System;
using System.Reflection;
using Npgsql;

namespace Venflow.Modeling
{
    internal class PostgreEnumEntityColumn<TEntity> : EntityColumn<TEntity>, IPostgreEnumEntityColumn
        where TEntity : class, new()
    {
        internal PostgreEnumEntityColumn(PropertyInfo propertyInfo, string columnName, Func<TEntity, string, NpgsqlParameter> valueRetriever, bool isReadOnly) : base(propertyInfo, columnName, valueRetriever, false, isReadOnly)
        {

        }
    }

    internal interface IPostgreEnumEntityColumn
    {

    }
}
