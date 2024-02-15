using Dapper;
using Microsoft.Data.SqlClient;
using MVPHBA.Common;
using MVPHBA.DataAccess.Interfaces;
using MVPHBA.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVPHBA.DataAccess.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        public async Task<List<PropertyInfoListVM>> PropertyInfoList(SearchVM dto)
        {
            using (SqlConnection conn = new SqlConnection(Others.ConnStr))
            {
                var parameters = new { displayStart = dto.DisplayStart, displayLength = dto.DisplayLength, sortDir = dto.SortDir ?? "asc", sortCol = dto.SortCol, location = dto.Location ?? "", price = dto.Price, propertyType = dto.PropertyType ?? "" };
                await conn.OpenAsync();
                var data = await conn.QueryAsync<PropertyInfoListVM>("Sp_PropertyInfoListGet", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data.ToList();
            }
        }
    }
}
