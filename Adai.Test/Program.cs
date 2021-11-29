using Adai.DbContext;
using Adai.DbContext.MySql;
using Adai.Standard;
using Adai.Standard.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Adai.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"Hello World!=>{args}");

			// 初始化数据库配置
			CommonHelper.InitDbConfig();

			TestDbTrans();
		}


		static void TestDbTrans()
		{
			/*
		CREATE TABLE `User` (
  `Id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `Username` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '用户名',
  `Nickname` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '昵称',
  `Avatar` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '头像地址',
  `FirstName` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '名字',
  `LastName` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '姓氏',
  `Email` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '电子邮箱',
  `Mobile` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '手机号码',
  `Tel` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '电话号码',
  `Roles` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '角色',
  `SecretKey` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '密钥',
  `Status` int NOT NULL COMMENT '状态',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `UpdateTime` datetime NOT NULL COMMENT '修改时间',
  `Note` varchar(512) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '说明',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci ROW_FORMAT=DYNAMIC COMMENT='用户';
			 
			 */
			var eventId = Guid.NewGuid().ToString("N");
			var dbContext = new MySqlDbContext(eventId);

			var sql = $"INSERT INTO `User` (Username,Nickname,Roles,SecretKey,Status,CreateTime) VALUES ('test02','test02','','',0,'{DateTime.Now:yyyy-MM-dd HH:mm:ss}');";
			var sql0 = $"INSERT INTO `User` (Username,Nickname,Roles,SecretKey,Status,CreateTime) VALUES ('test02','test02','',NULL,0,'{DateTime.Now:yyyy-MM-dd HH:mm:ss}');";

			var cmds = new List<IDbCommand>() {
				dbContext.CreateCommand(sql),
				//dbContext.CreateCommand(sql0)
			};

			var cmds0 = new List<IDbCommand>() {
				//dbContext.CreateCommand(sql),
				dbContext.CreateCommand(sql0)
			};

			var dictCmds = new Dictionary<string, ICollection<IDbCommand>>()
			{
				{ DbConfig.Basic, cmds },
				{ DbConfig.Basic0, cmds0 }
			};
		}
	}
}
