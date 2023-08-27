using NetOa.Dapper.DataModel;
using NetOa.Dapper.DataModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.IRepository
{
    /// <summary>
    /// 文章仓储接口
    /// </summary>
    public interface IArticleRepository
    {
        List<ArticleUserColumn> GetAllArticle(int type, Search search);
        List<Column> GetAllColumn();
        bool AddArticle(Article article);
        bool RemoveArticle(int articleid);
        bool EditArticle(Article article);

        //文章评论
        bool AddPinglun(Pinglun pinglun);
        bool RemovePinglun(int PinglunID);

        List<PinglunUsers> GetPinglunByArticlesID(int ArticlesID);
    }
    
}
