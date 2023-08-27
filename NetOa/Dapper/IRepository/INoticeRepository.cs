using NetOa.Dapper.DataModel;
using NetOa.Dapper.DataModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.IRepository
{
    public interface INoticeRepository
    {
        List<NoticeState> GetNoticeByToUserID(Search search);
        List<Notice> GetNoticeByFormUserID(Search search);

        bool AddNotice(Notice notice);
        bool RemoveNotice(Notice notice);
        bool ModifyNotice(Notice notice);


        /// <summary>
        /// 针对Notice的状态表进行操作
        /// </summary>
        /// <param name="isReadNotic"></param>
        /// <returns></returns>
        int AddIsReadNotices(IsReadNotice  isReadNotice);
        List<IsReadNotice> GetIsReadNotice(Search search);
    }
}
