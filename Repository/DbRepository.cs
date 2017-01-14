using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using RepositoryInterface;
using SourceModel;

namespace Repository
{
    public class DbRepository : IDbRepository
    {
        private string _connectionString;
        private const string ReferencingObjectName = "referencing_object_name";
        private const string ReferencingObjectType = "referencing_object_type";
        private const string ReferencedObjectName = "referenced_object_name";
        private const string ReferencedObjectType = "referenced_object_type";
        private const string Sql = @"SELECT
	                                        referencing_object_name = o.name,
	                                        referencing_object_type = o.type_desc,
	                                        referenced_object_name = referenced_entity_name,
	                                        referenced_object_type = o1.type_desc
                                        FROM sys.sql_expression_dependencies sed
                                        INNER JOIN sys.objects o ON sed.referencing_id = o.[object_id]
                                        LEFT OUTER JOIN sys.objects o1 ON sed.referenced_id = o1.[object_id]
                                        where o.type_desc <> 'SYSTEM_TABLE'
	                                        and o.type_desc <> 'CHECK_CONSTRAINT'
	                                        and o.type_desc <> 'SQL_SCALAR_FUNCTION'
	                                        and o.type_desc <> 'SQL_INLINE_TABLE_VALUED_FUNCTION'
	                                        and o.type_desc <> 'SQL_TRIGGER'";

        public bool Login(string userName, string password)
        {
            var result = false;
            var connectionString = ConfigurationManager.ConnectionStrings["DefaulConnection"].ConnectionString;
            _connectionString = string.Format(connectionString, userName, password);
            var connection = new SqlConnection(_connectionString);

            try
            {
                connection.Open();
                result = true;
            }
            catch (Exception exception)
            {
                _connectionString = null;
                result = true;
                throw new Exception("Failed to login!", exception);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public bool LoggOut()
        {
            // TODO: Implement.
            return true;
        }

        public List<Item> GetSource()
        {
            var resultList = new List<Item>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(Sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            resultList.Add(ReadItem(reader));
                        }
                    }
                    reader.Close();
                }
            }

            return resultList;
        }

        public string GetDbObjectSql(string objectType, string objectName)
        {
            string result;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(GetObjectSourceSql(objectType, objectName), connection))
                {
                    connection.Open();
                    switch (objectType)
                    {
                        case ObjectTypes.SQL_STORED_PROCEDURE:
                        case ObjectTypes.VIEW:
                            result = ExecuteReader(command);
                            break;
                        default:
                            result = ExecuteScalar(command, objectType);
                            break;
                    }
                    connection.Close();
                }
            }
            return result;
        }

        public async Task<string> GetDbObjectSqlAsync(string objectType, string objectName)
        {
            string result;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(GetObjectSourceSql(objectType, objectName), connection))
                {
                    await connection.OpenAsync();
                    switch (objectType)
                    {
                        case ObjectTypes.SQL_STORED_PROCEDURE:
                        case ObjectTypes.VIEW:
                            result = await ExecuteReaderAsync(command);
                            break;
                        default:
                            result = ExecuteScalar(command, objectType);
                            break;
                    }
                }
            }
            return result;
        }

        private static string ExecuteReader(SqlCommand command)
        {
            var result = new StringBuilder();
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Append(reader.GetString(0));
                }
            }
            reader.Close();
            return result.ToString();
        }

        private static async Task<string> ExecuteReaderAsync(SqlCommand command)
        {
            var result = new StringBuilder();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Append(reader.GetString(0));
                }
            }
            return result.ToString();
        }

        private static string ExecuteScalar(SqlCommand command, string objectType)
        {
            return string.Format("GetDbObjectSql for {0} is not implemented", objectType);
            //var value = command.ExecuteScalar().ToString();
            //result.Append(value);
        }

        private string GetObjectSourceSql(string objectType, string objectName)
        {
            switch (objectType)
            {
                case ObjectTypes.SQL_STORED_PROCEDURE:
                case ObjectTypes.VIEW:
                    return string.Format(@"EXEC sp_HelpText '{0}'", objectName);
                default:
                    return string.Format(@"SELECT OBJECT_DEFINITION (OBJECT_ID(N'{0}'))", objectName);
            }
        }

        private static Item ReadItem(SqlDataReader reader)
        {
            return new Item(GetStringValue(reader, ReferencingObjectName),
                            GetStringValue(reader, ReferencingObjectType),
                            GetStringValue(reader, ReferencedObjectName),
                            GetStringValue(reader, ReferencedObjectType));
        }

        private static string GetStringValue(SqlDataReader reader, string fieldName)
        {
            var index = reader.GetOrdinal(fieldName);
            var dbValue = reader.GetValue(index);
            return !DBNull.Value.Equals(dbValue) ? dbValue.ToString() : null;
        }
    }
}
