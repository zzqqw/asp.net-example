using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NetOa.Dapper;
using NetOA.Dapper.DataModel;
using NetOA.Dapper.IRepository;

namespace NetOA.Dapper.Repository
{
    /// <summary>
    /// 工作的仓储类
    /// </summary>
    public class WorkItemRepository : IWorkItemRepository
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        INetOADB _workingDB;
        public WorkItemRepository(INetOADB workingDB)
        {
            _workingDB = workingDB;

        }

        public List<WorkItem> GetWorkItem(int type, Search search)
        {

            string sql = "select * from workitems where id > 0";
            var p = new DynamicParameters();
            if (type > 0) {
                sql += " and type = @type";
                p.Add("type", type);
            }
            if (search.ID>0) {
                sql += " and ID = @id";
                p.Add("id", search.ID);
            }
            if (search.userId>0) {
                sql += " and CreateUserID = @createUserID";
                p.Add("createUserID", search.userId);
            }
            if (!string.IsNullOrEmpty(search.KeyWords)) {
                sql += " AND ( Area like @keyWords  OR  Title like  @keyWords)";
                p.Add("keyWords", "%"+search.KeyWords+"%");
            }
            return _workingDB.Query<WorkItem>(sql, p).ToList();
        }


        public bool ModifyWorkItem(WorkItem workItem) {
            string sql = "update workitems set Remarks=@remarks,RecordDate=@recordDate,WorkContent=@workContent,Area=@area," +
                "Longitude=@longitude,Latitude=@latitude,Status=@status,ReviewerUserID=@reviewerUserID,type=@type ,Title=@title,Agency=@agency,RelevantPeople=@relevantPeople" +
                "where id=@id";
            var result = _workingDB.Execute(sql, 
                new{
                    remarks = workItem.Remarks,
                    recordDate = workItem.RecordDate,
                    workContent = workItem.WorkContent,
                    area = workItem.Area,
                    longitude = workItem.Longitude,
                    latitude = workItem.Latitude,
                    status = workItem.Status,
                    reviewerUserID = workItem.ReviewerUserID,
                    type = workItem.Type,
                    title=workItem.Title,
                    agency=workItem.Agency,
                    relevantPeople=workItem.RelevantPeople,
                    id =workItem.ID
                });
            return true;
        }

        /// <summary>
        /// 添加工作记录
        /// </summary>
        /// <param name="workItem">工作记录</param>
        /// <returns></returns>
        public bool AddWorkItem(WorkItem workItem)
        {
            if (workItem == null)
            {
                throw new Exception("添加的不能为Null");
            }
            else {
               
                string sql = "insert into workitems(" +
                    "CreateTime,CreateUserID,Remarks,RecordDate,WorkContent,Area,Longitude,Latitude,Status,ReviewerUserID,type,Title,Agency,RelevantPeople" +
                    ")values(" +
                    "@createTime,@createUserID,@remarks,@recordDate,@workContent,@area,@longitude,@latitude,@status,@reviewerUserID,@type,@title,@agency,@relevantPeople" +
                    ")";
                var result = _workingDB.Execute(sql,
                    new
                    {
                        createTime = workItem.CreateTime,
                        createUserID = workItem.CreateUserID,
                        remarks= workItem.Remarks,
                        recordDate=workItem.RecordDate,
                        workContent = workItem.WorkContent,
                        area = workItem.Area,
                        longitude = workItem.Longitude,
                        latitude = workItem.Latitude,
                        status = workItem.Status,
                        reviewerUserID=workItem.ReviewerUserID,
                        type=workItem.Type,
                        title = workItem.Title,
                        agency=workItem.Agency,
                        relevantPeople = workItem.RelevantPeople
                    });
                return result > 0;

            }

        }

    }
}
