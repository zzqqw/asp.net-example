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
    /// <summary>
    /// 文章仓储类
    /// </summary>
    public class ArticleRepository:IArticleRepository
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        INetOADB _workingDB;
        public ArticleRepository(INetOADB workingDB)
        {
            _workingDB = workingDB;
        }
        public List<PinglunUsers> GetPinglunByArticlesID(int ArticlesID)
        {
            string sql = "select Pingluns.*,Users.Name as UserName " +
                "from  Pingluns,Users" +
                " where Pingluns.userID=Users.ID";
            //string sql = "select * form Pingluns join Users ON  Pingluns.userID=Users.ID";
            var param = new DynamicParameters();

            sql += " and Pingluns.ArticlesID = @articlesID";
            param.Add("articlesID", ArticlesID);

            return _workingDB.Query<PinglunUsers>(sql, param).ToList();

        }
        public bool AddPinglun(Pinglun pinglun) {
            var sql = "insert into Pingluns" +
                "(UserID,ArticlesID,content,CreateTime)" +
                " values" +
                "(@userID,@articlesID,@content,@createTime)";
            return _workingDB.Execute(sql,
                        new
                        {
                            userID=pinglun.UserID,
                            articlesID=pinglun.ArticlesID,
                            content=pinglun.content,
                            createTime=pinglun.CreateTime

                        }) > 0;

        }
        public bool RemovePinglun(int PinglunID)
        {
            return _workingDB.Execute("delete from Pingluns where id=@id", new { id = PinglunID }) > 0;

        }
        public bool EditArticle(Article article)
        {
            if (article == null)
            {
                throw new Exception("修改的不能为Null");
            }
            else {
                string sql = "update Articles set " +
                    "Title=@title," +
                    "Content=@content," +
                    "ColumnID=@columnID " +
                    "where id=@id";
                return _workingDB.Execute(sql, 
                    new {
                        title = article.Title,
                        content = article.Content,
                        columnID = article.ColumnID,
                        id = article.ID
                    }) > 0;

            }

        }
        public bool RemoveArticle(int articleid)
        {
            return _workingDB.Execute("delete from Articles where id=@id", new { id = articleid }) > 0;
        }
        public List<ArticleUserColumn> GetAllArticle(int type, Search search) {
            string sqlText = "select " +
                "Articles.* ,Articles.id as aid ," +
                "Columns.columnName ," +
                "Users.Name as username " +
                "from Articles ,Columns ,Users " +
                "where Articles.ColumnID=Columns.id AND Articles.CreateUserID=Users.id  AND Articles.Type=@Type ";
            DynamicParameters pars = new DynamicParameters();
            pars.Add("Type", type);
            if (search.userId > 0)
            {
                sqlText += " AND Articles.CreateUserID=@userid";
                pars.Add("userid", search.userId);
            }
            return _workingDB.Query<ArticleUserColumn>(sqlText, pars).ToList();

        }
        public List<Column> GetAllColumn()
        {
            return _workingDB.Query<Column>("select * from  Columns").ToList();

        }
        public bool AddArticle(Article article)
        {
            if (article == null)
            {
                throw new Exception("添加的不能为Null");
            }
            else
            {
                var sql = "insert into Articles" +
                    "(CreateTime,CreateUserID,Title,Content,Click,Type,ColumnID)" +
                    " values" +
                    "(@CreateTime,@CreateUserID,@Title,@Content,@Click,@Type,@ColumnID)";
                var result = _workingDB.Execute(sql,
                    new {
                        CreateTime = article.CreateTime,
                        CreateUserID = article.CreateUserID,
                        Title = article.Title,
                        Content = article.Content,
                        Type =article.Type,
                        Click=article.Click,
                        ColumnID = article.ColumnID
                    });
                return result > 0;
            }
        }
    }
}
