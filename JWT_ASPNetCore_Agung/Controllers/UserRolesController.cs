﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using JWT_ASPNetCore_Agung.Services;
using JWT_ASPNetCore_Agung.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_ASPNetCore_Agung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IDapper _dapper;
        public UserRolesController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(UserRole data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("UserId", data.UserId, DbType.Int32);
            dbparams.Add("RoleId", data.RoleId, DbType.Int32);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Insert_UserRole]", dbparams, commandType: CommandType.StoredProcedure));
            return result;
        }
        [HttpGet(nameof(GetById))]
        public async Task<UserRole> GetById(int id)
        {
            var result = await Task.FromResult(_dapper.Get<UserRole>($"Select * from TB_M_UserRole where Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetAllData))]
        public List<UserRole> GetAllData()
        {
            var result = (_dapper.GetAll<UserRole>($"Select * from TB_M_UserRole", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int id)
        {
            var result = await Task.FromResult(_dapper.Execute($"DELETE FROM TB_M_UserRole WHERE Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(UserRole data)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", data.Id);
            dbPara.Add("UserId", data.UserId, DbType.Int32);
            dbPara.Add("RoleId", data.RoleId, DbType.Int32);
            var updateUserRole = Task.FromResult(_dapper.Update<int>("[dbo].[SP_Update_UserRole]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateUserRole;
        }
    }
}
