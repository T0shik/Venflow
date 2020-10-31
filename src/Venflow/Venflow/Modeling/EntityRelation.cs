﻿using System.Reflection;
using Venflow.Enums;

namespace Venflow.Modeling
{
    internal class EntityRelation
    {
        internal uint RelationId { get; }

        internal Entity LeftEntity { get; }
        internal PropertyInfo? LeftNavigationProperty { get; }
        internal bool IsLeftNavigationPropertyNullable { get; }

        internal Entity RightEntity { get; }
        internal PropertyInfo? RightNavigationProperty { get; }
        internal bool IsRightNavigationPropertyNullable { get; }

        internal EntityColumn ForeignKeyColumn { get; }
        internal RelationType RelationType { get; }
        internal ForeignKeyLocation ForeignKeyLocation { get; }

        internal EntityRelation Sibiling { get; set; }

        internal RelationInformation? Information { get; }

        internal EntityRelation(uint relationId, Entity leftEntity, PropertyInfo? leftNavigationProperty, bool isLeftNavigationPropertyNullable, Entity rightEntity,
            PropertyInfo? rightNavigationProperty, bool isRightNavigationPropertyNullable, EntityColumn foreignKeyColumn, RelationType relationType, ForeignKeyLocation foreignKeyLocation, RelationInformation? information)
        {
            RelationId = relationId;
            LeftEntity = leftEntity;
            LeftNavigationProperty = leftNavigationProperty;
            IsLeftNavigationPropertyNullable = isLeftNavigationPropertyNullable;
            RightEntity = rightEntity;
            RightNavigationProperty = rightNavigationProperty;
            IsRightNavigationPropertyNullable = isRightNavigationPropertyNullable;
            ForeignKeyColumn = foreignKeyColumn;
            RelationType = relationType;
            ForeignKeyLocation = foreignKeyLocation;
            Information = information;
        }
    }
}