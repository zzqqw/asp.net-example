using NetOa.Dapper;
using NetOA.Dapper.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace NetOA.Dapper.IRepository
{
    /// <summary>
    /// 工作的仓储接口
    /// </summary>
    public interface IWorkItemRepository
    {

        List<WorkItem> GetWorkItem(int type, Search search);
        /// <returns></returns>
        bool AddWorkItem(WorkItem workItem);

        bool ModifyWorkItem(WorkItem workItem);
    }
}
