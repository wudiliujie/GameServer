using ETModel;
using Model.Module.MySql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace ETHotfix.Module.Sql
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public byte[] UserData { get; set; }

    }
    [ObjectSystem]
    public class SqlComponentSystem : AwakeSystem<SqlComponent, string>
    {
        public override void Awake(SqlComponent self, string conn)
        {
            self.Awake(conn);
        }
    }
    public static class SqlComponentEx
    {
        public static void Awake(this SqlComponent self, string conn)
        {
            self.ConnectionString = conn;
        }
        public static IDbConnection GetDBConnection(this SqlComponent self)
        {
            var db = new MySql.Data.MySqlClient.MySqlConnection(self.ConnectionString);
            db.Open();
            return db;
        }
        public async static Task<RoleDbInfo> GetUserDbInfo(this SqlComponent self, int UserId)
        {
            string sql = "select * from User_Info where UserId = @UserId";
            using (var db = self.GetDBConnection())
            {
                var ret = await db.QueryFirstOrDefaultAsync<UserInfo>(sql, new { UserId });

                if (ret == null)
                {
                    return null;
                }
                else
                {
                    RoleDbInfo role = RoleDbInfo.Parser.ParseFrom(ret.UserData);
                    return role;
                }
            }
        }
    }
}
