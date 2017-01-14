using System;
using SourceModel;

namespace Model
{
    public static class ObjectTypeExtensions
    {
        public static ObjectType ToDbObjectType(this string objectType)
        {
            switch (objectType)
            {
                case ObjectTypes.DATABASE:
                    return ObjectType.Database;
                case ObjectTypes.USER_TABLE:
                    return ObjectType.Table;
                case ObjectTypes.VIEW:
                    return ObjectType.View;
                case ObjectTypes.SQL_STORED_PROCEDURE:
                    return ObjectType.StoredProcedure;
                case ObjectTypes.CHECK_CONSTRAINT:
                    return ObjectType.Constraint;
                case ObjectTypes.SQL_INLINE_TABLE_VALUED_FUNCTION:
                    return ObjectType.InlineTableValuedFunction;
                case ObjectTypes.SQL_SCALAR_FUNCTION:
                    return ObjectType.ScalarFunction;
                case ObjectTypes.SQL_TABLE_VALUED_FUNCTION:
                    return ObjectType.TableValuedFunction;
                case ObjectTypes.SQL_TRIGGER:
                    return ObjectType.Trigger;
                case ObjectTypes.FILE:
                    return ObjectType.ProjectFile;
                case ObjectTypes.INVALID:
                case null:
                    return ObjectType.InvalidDbObject;
                default:
                    throw new Exception(string.Format("Invalid object type: {0}", objectType));
            }
        }

        public static string ToObjectTypeString(this ObjectType objectType)
        {
            switch (objectType)
            {
                case ObjectType.Database:
                    return ObjectTypes.DATABASE;
                case ObjectType.Table:
                    return ObjectTypes.USER_TABLE;
                case ObjectType.View:
                    return ObjectTypes.VIEW;
                case ObjectType.StoredProcedure:
                    return ObjectTypes.SQL_STORED_PROCEDURE;
                case ObjectType.Constraint:
                    return ObjectTypes.CHECK_CONSTRAINT;
                case ObjectType.InlineTableValuedFunction:
                    return ObjectTypes.SQL_INLINE_TABLE_VALUED_FUNCTION;
                case ObjectType.ScalarFunction:
                    return ObjectTypes.SQL_SCALAR_FUNCTION;
                case ObjectType.TableValuedFunction:
                    return ObjectTypes.SQL_TABLE_VALUED_FUNCTION;
                case ObjectType.Trigger:
                    return ObjectTypes.SQL_TRIGGER;
                case ObjectType.ProjectFile:
                    return ObjectTypes.FILE;
                case ObjectType.InvalidDbObject:
                    return ObjectTypes.INVALID;
                default:
                    throw new Exception(string.Format("Invalid object type: {0}", objectType));
            }
        }
    }
}