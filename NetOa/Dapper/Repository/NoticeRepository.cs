using Dapper;
using NetOa.Dapper.DataModel;
using NetOa.Dapper.DataModelView;
using NetOa.Dapper.IRepository;
using NetOA.Dapper.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.Repository
{
    public class NoticeRepository:INoticeRepository
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        INetOADB _workingDB;
        public NoticeRepository(INetOADB workingDB)
        {
            _workingDB = workingDB;
        }

        public List<NoticeState> GetNoticeByToUserID(Search search)
        {
            //search.userId=3;

            string sql = "select * from Notices WHERE ID>0 ";
            var param = new DynamicParameters();


            sql += " AND ToUserID LIKE @toUserID";
            param.Add("toUserID",  "%"+search.userId.ToString()+"%"  );

            if (search.ID > 0)
            {
                sql += " AND id = @id";
                param.Add("id", search.ID);
            }
            var NoticeState = new List<NoticeState>();

            NoticeState.AddRange(_workingDB.Query<NoticeState>(sql, param).ToList());

            string sql2 = "select * from IsReadNotices where NoticeID=@noticeID AND UserID=@uid ";

            foreach (var n in NoticeState)
            {

                IsReadNotice isReadNotices = _workingDB.Query<IsReadNotice>(sql2, new { noticeID = n.ID, uid = search.userId }).SingleOrDefault();
                if (isReadNotices != null)
                {
                    n.statusMsg = isReadNotices.statusMsg;
                    n.Remarks = isReadNotices.Remarks;
                }

            }


            return NoticeState;
        }
        /// <summary>
        /// 查看是否已读未读
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<IsReadNotice> GetIsReadNotice(Search search)
        {
            //search.userId=3;

            string sql = "select * from IsReadNotices WHERE ID>0 ";
            var param = new DynamicParameters();
            sql += " AND UserID = @userID";
            param.Add("userID", search.userId);

            if (search.ID > 0)
            {
                sql += " AND NoticeID = @id";
                param.Add("id", search.ID);
            }
            var IsReadNotice = new List<IsReadNotice>();
            IsReadNotice.AddRange(_workingDB.Query<IsReadNotice>(sql, param).ToList());
            return IsReadNotice;
        }

        public List<Notice> GetNoticeByFormUserID(Search  search)
        {
            //search.userId=3;
 
            string sql = "select * from Notices WHERE ID>0 ";
            var param = new DynamicParameters();
            sql += " AND FormUserID = @FormUserID";
            param.Add("FormUserID",  search.userId );
            if (search.ID>0) {
                sql += " AND id = @id";
                param.Add("id",search.ID);
            }
            var Notice = new List<Notice>();
            Notice.AddRange(_workingDB.Query<Notice>(sql, param).ToList());
            return Notice;
        }
        /// <summary>
        /// 标记已读
        /// </summary>
        /// <param name="isReadNotice"></param>
        /// <returns></returns>
        public int AddIsReadNotices(IsReadNotice isReadNotice)
        {
            isReadNotice.CreateTime = DateTime.Now;
            string sql = "insert into IsReadNotices" +
                "(UserID,NoticeID,statusMsg,Remarks,CreateTime)" +
                " values" +
                "(@userId,@noticeID,@statusMsg,@remarks,@createTime)";
            DynamicParameters pars = new DynamicParameters();
            pars.Add("userId", isReadNotice.UserID);
            pars.Add("statusMsg", isReadNotice.statusMsg);
            pars.Add("remarks", isReadNotice.Remarks);
            pars.Add("createTime", isReadNotice.CreateTime);
            int intRows = 0;


            string sqlfind = "select * from IsReadNotices where NoticeID=@noticeID AND UserID=@userID";
            //NoticeID=1，或则=1的时候处理方式
            if (isReadNotice.NoticeID.Length <= 2)
            {

                isReadNotice.NoticeID = isReadNotice.NoticeID.Length == 2 ? isReadNotice.NoticeID.Substring(0, isReadNotice.NoticeID.Length - 1) : isReadNotice.NoticeID;

                var IsReadNoticeSelect = _workingDB.Query<IsReadNotice>(sqlfind, new { noticeID = isReadNotice.NoticeID, userID = isReadNotice.UserID });
                if (IsReadNoticeSelect.Count() == 0)
                {
                    pars.Add("noticeID", isReadNotice.NoticeID);
                    intRows = _workingDB.Execute(sql, pars);
                }
                else
                {
                    intRows = 0;
                }


            }
            else
            {
                string[] strArryy = isReadNotice.NoticeID.Split(',');

                foreach (string NoticeID in strArryy)
                {

                    var IsReadNoticeSelect = _workingDB.Query<IsReadNotice>(sqlfind, new { noticeID = NoticeID, userID = isReadNotice.UserID });
                    if (IsReadNoticeSelect.Count() == 0)
                    {
                        pars.Add("noticeID", NoticeID);
                        intRows = _workingDB.Execute(sql, pars);
                    }
                }


            }

            return intRows;

        }
        /// <summary>
        /// 添加通知
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public bool AddNotice(Notice notice) {
            notice.CreateTime = DateTime.Now;

            string sql = "insert into Notices" +
                "(NoticeType,NoticeTitle,NoticeContent,FormUserID,ToUserID,CreateTime)" +
                " values" +
                "(@noticeType,@noticeTitle,@noticeContent,@formUserID,@toUserID,@createTime)";
            DynamicParameters pars = new DynamicParameters();

            return _workingDB.Execute(sql,new {
                noticeType  = notice.NoticeType,
                noticeTitle = notice.NoticeTitle,
                noticeContent=notice.NoticeContent,
                formUserID=notice.FormUserID,
                toUserID=notice.ToUserID,
                createTime=notice.CreateTime
            }) > 0;

        }
        public bool RemoveNotice(Notice notice) {
            return _workingDB.Execute("delete from Notices where id=@id", new { id = notice.ID }) > 0;
        }
        public bool ModifyNotice(Notice notice) {
            //string sql = "update Notices set " +
            //     "NoticeType=@noticeType," +
            //     "NoticeTitle=@noticeTitle," +
            //     "NoticeContent=@noticeContent," +
            //     "ToUserID=@toUserID where Id=@id AND FormUserID=@formUserID";
            string sql = "update Notices set " +
                 "NoticeType=@noticeType," +
                 "NoticeTitle=@noticeTitle," +
                 "NoticeContent=@noticeContent," +
                 "ToUserID=@toUserID where Id=@id ";
            return _workingDB.Execute(sql,
                new
                {
                    noticeType = notice.NoticeType,
                    noticeTitle = notice.NoticeTitle,
                    noticeContent = notice.NoticeContent,
                    toUserID = notice.ToUserID,
                    id = notice.ID,
                    //formUserID = notice.FormUserID
                }) > 0;
        }
    }
}
