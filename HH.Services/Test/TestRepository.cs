using HH.API.Entity;
using System;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using HH.API.Entity.Database;

namespace HH.API.Services
{
    public class TestRepository : RepositoryBase<TestParentEntity>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TestRepository()
        {

        }

        public override dynamic Insert(TestParentEntity t)
        {
            dynamic result;
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                result = conn.Insert<TestParentEntity>(t);
                conn.Insert<TestChildEntity>(t.TestChild);
                conn.Insert<TestUserEntity>(t.TestUser);
            }
            return result;
        }

        public override TestParentEntity GetObjectById(string objectId)
        {
            using (var conn = ConnectionFactory.DefaultConnection())
            {
                TestParentEntity t = conn.Get<TestParentEntity>(objectId);
                if (t == null) return null;
                // t.TestChild = conn.QuerySingle<TestChildEntity>("SELECT * FROM Test_Child WHERE RoleId=@RoleId", new { RoleId = objectId });

                var predicateChild = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                predicateChild.Predicates.Add(Predicates.Field<TestChildEntity>(u => u.RoleId, Operator.Eq, objectId));

                t.TestChild = conn.GetList<TestChildEntity>(predicateChild).SingleOrDefault<TestChildEntity>();

                var predicateUser = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                predicateUser.Predicates.Add(Predicates.Field<TestUserEntity>(u => u.RoleId, Operator.Eq, objectId));

                IList<ISort> sort = new List<ISort>();
                sort.Add(new Sort() { Ascending = false, PropertyName = EntityBase.PropertyName_CreatedTime });

                t.TestUser = conn.GetList<TestUserEntity>(predicateUser, sort).ToList<TestUserEntity>();
                return t;
            }
        }


    }
}