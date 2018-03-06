using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopping.Models;

namespace WebShopping.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {
            string cnstr = ConfigurationManager.ConnectionStrings[
                "mvcdbConnectionString"].ConnectionString;

            //データベースに接続する
            DataContext dc = new DataContext(cnstr);

            //商品一覧を取得
            ProductModel model = new ProductModel();
            model.Products = dc.GetTable<TProduct>();
            

            //1ページに表示する商品数
            int max_item = 5;
            //表示中のページ
            int cur_page = page ?? 0;
            int max = dc.GetTable<TProduct>().Count();

            //指定ページの商品数を取得する
            model.Products = (from p in dc.GetTable<TProduct>()
                              select p).Skip(cur_page * max_item).Take(max_item);
            model.CurrentPage = cur_page;

            //前ページが存在するか
            if (cur_page == 0)
            {
                model.HasPrevPage = false;
            }
            else
            {
                model.HasPrevPage = true;
            }

            //後ろページが存在するか
            if (cur_page * max_item + max_item < max)
            {
                model.HasNextPage = true;
            }
            else
            {
                model.HasNextPage = false;
            }

            //モデルを設定する
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
