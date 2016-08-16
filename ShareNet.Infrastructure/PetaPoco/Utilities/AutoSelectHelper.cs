// <copyright file="AutoSelectHelper.cs" company="PetaPoco - CollaboratingPlatypus">
//      Apache License, Version 2.0 https://github.com/CollaboratingPlatypus/PetaPoco/blob/master/LICENSE.txt
// </copyright>
// <author>PetaPoco - CollaboratingPlatypus</author>
// <date>2015/12/05</date>

using System.Linq;
using System.Text.RegularExpressions;
using PetaPoco.Core;

namespace PetaPoco.Internal
{
    internal static class AutoSelectHelper
    {
        private static Regex rxSelect = new Regex(@"\A\s*(SELECT|EXECUTE|CALL|WITH|SET|DECLARE)\s",
            RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private static Regex rxFrom = new Regex(@"\A\s*FROM\s",
            RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public static string AddSelectClause<T>(IProvider provider, string sql, IMapper defaultMapper=null)
        {
            if (sql.StartsWith(";"))
                return sql.Substring(1);

            if (!rxSelect.IsMatch(sql))
            {
                //var pd = PocoData.ForType(typeof(T), defaultMapper);
                var pd = PocoData.ForType(typeof(T));
                var tableName = provider.EscapeTableName(pd.TableInfo.TableName);
                string cols = pd.Columns.Count != 0
                    ? string.Join(", ", (from c in pd.QueryColumns select tableName + "." + provider.EscapeSqlIdentifier(c)).ToArray())
                    : "NULL";
                if (!rxFrom.IsMatch(sql))
                    sql = string.Format("SELECT {0} FROM {1} {2}", cols, tableName, sql);
                else
                    sql = string.Format("SELECT {0} {1}", cols, sql);
            }
            return sql;
        }
        public static string AddSelectClauseMapper<T>(IProvider provider, string sql, IMapper defaultMapper )
        {
            if (sql.StartsWith(";"))
                return sql.Substring(1);

            if (!rxSelect.IsMatch(sql))
            {
                var pd = PocoData.ForType(typeof(T), defaultMapper);
                
                var tableName = provider.EscapeTableName(pd.TableInfo.TableName);
                string cols = pd.Columns.Count != 0
                    ? string.Join(", ", (from c in pd.QueryColumns select tableName + "." + provider.EscapeSqlIdentifier(c)).ToArray())
                    : "NULL";
                if (!rxFrom.IsMatch(sql))
                    sql = string.Format("SELECT {0} FROM {1} {2}", cols, tableName, sql);
                else
                    sql = string.Format("SELECT {0} {1}", cols, sql);
            }
            return sql;
        }

        public static string AddSelectClause<T>(IProvider provider, string sql, string primaryKey = null)
        {
            if (sql.StartsWith(";"))
            {
                return sql.Substring(1);
            }
            if (!rxSelect.IsMatch(sql))
            {
                PocoData data = PocoData.ForType(typeof(T));
                string tableName = provider.EscapeTableName(data.TableInfo.TableName);
                string str = string.Empty;
                if (string.IsNullOrEmpty(primaryKey))
                {
                    str = (data.Columns.Count != 0) ? string.Join(", ", (from c in data.QueryColumns select tableName + "." + provider.EscapeSqlIdentifier(c)).ToArray<string>()) : "NULL";
                }
                else
                {
                    str = primaryKey;
                }
                if (!rxFrom.IsMatch(sql))
                {
                    sql = string.Format("SELECT {0} FROM {1} {2}", str, tableName, sql);
                    return sql;
                }
                sql = string.Format("SELECT {0} {1}", str, sql);
            }
            return sql;
        }





    }
}