using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Dapper.PagingSample.Common;
using MiniTool.Entity;

namespace MiniTool.Repository
{
    public class LogRepository : RepositoryBase
    {
        /// <summary>
        /// LogRepository
        /// </summary>
        /// <param name="connectionString"></param>
        public LogRepository(string connectionString): base(connectionString)
        {
        }


 
        public void ClearTable(string type)
        {
            using (IDbConnection connection = base.OpenConnection())
            {
                // connection.ExecuteScalar("delete from dbo.Logs where Type=" + type);
                connection.ExecuteScalar("delete from dbo.Logs");
            }
        }

        public Tuple<IEnumerable<Log>, int> FindWithOffsetFetch(LogSearchCriteria criteria
                                                , int pageIndex
                                                , int pageSize
                                                , List<SortDescriptor> sortings)
        {
            using (IDbConnection connection = base.OpenConnection())
            {
                //, lv.Name AS [Level]
                // INNER JOIN [Level] lv ON l.LevelId = lv.Id

                const string selectQuery = @" ;WITH _data AS (
                                            SELECT l.* 
                                            FROM  dbo.Logs l
                                            /**where**/
                                        ),
                                            _count AS (
                                                SELECT COUNT(1) AS TotalCount FROM _data
                                        )
                                        SELECT * FROM _data CROSS APPLY _count /**orderby**/ OFFSET @PageIndex * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";

                SqlBuilder builder = new SqlBuilder();
                
                var selector = builder.AddTemplate(selectQuery, new { PageIndex = pageIndex, PageSize = pageSize });

                if (!string.IsNullOrEmpty(criteria.VenNum))
                    builder.Where("l.VenNum = @vennum", new { vennum = criteria.VenNum });

                if (!string.IsNullOrEmpty(criteria.VenName))
                {
                    builder.Where("l.VenName = @venname", new { venname = criteria.VenName });
                }
                   

                if (!string.IsNullOrEmpty(criteria.VenID))
                {
                    builder.Where("l.VenID = @venid", new { venid = criteria.VenID });
                }
                   

                if (!string.IsNullOrEmpty(criteria.ResponseXML))
                {
                    var msg = "%" + criteria.ResponseXML + "%";
                    builder.Where("l.responseXML Like @v1", new { v1 = msg });
                }
                if (!string.IsNullOrEmpty(criteria.RequestXML))
                {
                    var msg = "%" + criteria.RequestXML + "%";
                    builder.Where("l.requestXML Like @v2", new { v2 = msg });
                }


                if (criteria.ResponseTime !=null)
                {
                    builder.Where("l.responseTime = @v3", new { v3 = criteria.ResponseTime });
                }

                if (criteria.fromResponseTime != null)
                {
                    builder.Where("l.responseTime >= @v4", new { v4 = criteria.fromResponseTime });
                }

                if (criteria.toResponseTime != null)
                {
                    builder.Where("l.responseTime <= @v5", new { v5 = criteria.toResponseTime });
                }
                if (criteria.Date != null)
                {
                    builder.Where("cast (l.Date as date) = @v6", new { v6 = criteria.Date });
                }
                if (criteria.toDate != null)
                {
                    builder.Where("l.Date <= @v8", new { v8 = criteria.toDate });
                }
                if (criteria.fromDate != null)
                {
                    builder.Where("l.Date >= @v7", new { v7 = criteria.fromDate });
                }

                if (criteria.ResponseCode != null)
                {
                    builder.Where("l.ResponseCode = @v10", new { v10 = criteria.ResponseCode });
                }

                if (!string.IsNullOrWhiteSpace( criteria.ResponseType))
                {
                    builder.Where("l.responseType = @v12", new { v12 = criteria.ResponseType });
                }

                if (!string.IsNullOrWhiteSpace( criteria.RequestType))
                {
                    builder.Where("l.requestType = @v13", new { v13 = criteria.RequestType });
                }

                if (!string.IsNullOrWhiteSpace( criteria.ResponseDescription) )
                {
                    var msg = "%" + criteria.ResponseDescription + "%";
                    builder.Where("l.ResponseDescription Like @v14", new { v14 = msg });
                   
                }
                

                foreach (var sorting in sortings)
                {
                    if (string.IsNullOrWhiteSpace(sorting.Field))
                        continue;

                    if (sorting.Direction == SortDescriptor.SortingDirection.Ascending)
                        builder.OrderBy(sorting.Field);
                    else if (sorting.Direction == SortDescriptor.SortingDirection.Descending)
                        builder.OrderBy(sorting.Field + " desc");
                }

                var rows = connection.Query<Log>(selector.RawSql, selector.Parameters).ToList();

                if(rows.Count == 0)
                    return new Tuple<IEnumerable<Log>, int>(rows, 0);
                

                return new Tuple<IEnumerable<Log>, int>(rows, rows[0].TotalCount);
                
            }
        }


    }
}
