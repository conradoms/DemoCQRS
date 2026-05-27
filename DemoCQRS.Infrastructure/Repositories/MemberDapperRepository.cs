using Dapper;
using DemoCQRS.Domain.Abstractions;
using DemoCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DemoCQRS.Infrastructure.Repositories
{
    public class MemberDapperRepository : IMemberDapperRepository
    {
        private readonly IDbConnection _dbConnection;

        public MemberDapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
            string query = "SELECT * FROM public.\"Members\"";
            return await _dbConnection.QueryAsync<Member>(query);
        }

        public async Task<Member> GetMemberById(int id)
        {
            string query = "SELECT * FROM public.\"Members\" WHERE \"Id\" = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Member>(query, new { Id = id });
        }
    }
}
