using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Dapper.PagingSample.Common;
using MiniTool.Entity;

namespace MiniTool.Repository
{
    public class EventRepository : RepositoryBase
    {
        /// <summary>
        /// LogRepository
        /// </summary>
        /// <param name="connectionString"></param>
        public EventRepository(string connectionString): base(connectionString)
        {
        }


        public void ClearTable()
        {
            using (IDbConnection connection = base.OpenConnection())
            {
               
                connection.ExecuteScalar("delete from dbo.Events");
             
            }
        }


        public Tuple<IEnumerable<Event>, int> FindWithOffsetFetch(EventSearchCriteria criteria
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
                                            FROM  dbo.Events l
                                            /**where**/
                                        ),
                                            _count AS (
                                                SELECT COUNT(1) AS TotalCount FROM _data
                                        )
                                        SELECT * FROM _data CROSS APPLY _count /**orderby**/ OFFSET @PageIndex * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";

                SqlBuilder builder = new SqlBuilder();
                
                var selector = builder.AddTemplate(selectQuery, new { PageIndex = pageIndex, PageSize = pageSize });

                if (!string.IsNullOrEmpty(criteria.EventID))
                    builder.Where("l.IDEvent = @v1", new { v1 = criteria.EventID });

                if (!string.IsNullOrEmpty(criteria.MarketContext))
                {
                    var msg = "%" + criteria.MarketContext + "%";
                    builder.Where("l.MarketContext Like @v2", new { v2 = msg });
                }

                if (criteria.StartTime != null)
                {
                   
                    builder.Where("cast (l.StartTime as date) = @v3", new { v3 = criteria.StartTime });
                }

                if (!string.IsNullOrEmpty(criteria.Status))
                {

                    builder.Where("l.Status = @v4", new { v4 = criteria.Status });
                }

                if (!string.IsNullOrEmpty(criteria.CurrentValue))
                {
                    var msg = "%" + criteria.CurrentValue + "%";
                    builder.Where("l.CurrentValue Like @v5", new { v5 = msg });
                }
                if (!string.IsNullOrEmpty(criteria.VTNComment))
                {
                    var msg = "%" + criteria.VTNComment + "%";
                    builder.Where("l.VTNComment Like @v6", new { v6 = msg });
                }
                

                    if (!string.IsNullOrEmpty(criteria.ResponseRequired))
                {
                    var msg = "%" + criteria.ResponseRequired + "%";
                    builder.Where("l.ResponseRequired Like @v7", new { v7 = msg });
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

                var rows = connection.Query<Event>(selector.RawSql, selector.Parameters).ToList();

                if(rows.Count == 0)
                    return new Tuple<IEnumerable<Event>, int>(rows, 0);
                

                return new Tuple<IEnumerable<Event>, int>(rows, rows[0].TotalCount);
                
            }
        }


    }
}
