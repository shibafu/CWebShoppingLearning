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
        public ActionResult Index(int? page, int? Category)
        {
            string cnstr = ConfigurationManager.ConnectionStrings[
                "mvcdbConnectionString"].ConnectionString;

            /*▲▲▲▲▲▲▲▲▲▲▲データベース接続処理▲▲▲▲▲▲▲▲▲▲▲*/
            //データベースに接続する
            DataContext dc = new DataContext(cnstr);

            //商品一覧を取得
            ProductModel model = new ProductModel();
            model.Products = dc.GetTable<TProduct>();
            
            //カテゴリ一覧を取得
            model.Categories = dc.GetTable<TCategory>().OrderBy(c => c.id);


            /*▲▲▲▲▲▲▲▲▲▲▲表示処理スタート▲▲▲▲▲▲▲▲▲▲▲*/
            //1ページに表示する商品数
            int max_item = 5;
            //表示中のページ
            int cur_page = page ?? 0;
            int max = 0;

            /*▲▲▲▲▲▲▲▲▲▲▲商品閲覧処理▲▲▲▲▲▲▲▲▲▲▲*/
            if (Category == null)
            {
                //指定ページの商品数を取得する
                model.Products = (from p in dc.GetTable<TProduct>()
                                  select p).Skip(cur_page * max_item).Take(max_item);
                model.CurrentPage = cur_page;
                max = dc.GetTable<TProduct>().Count();

            }
            /*▲▲▲▲▲▲▲▲▲▲▲カテゴリ閲覧処理▲▲▲▲▲▲▲▲▲▲▲*/
            else
            {
                //指定したカテゴリの商品を表
                model.Products = (
                    from p in dc.GetTable<TProduct>()
                    where p.cateid == (int)Category
                    select p).Skip(cur_page * max_item).Take(max_item);
                //商品数
                max = (
                    from p in dc.GetTable<TProduct>()
                    where p.cateid == (int)Category
                    select p).Count();
            }

            model.Category = Category;
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

        public ActionResult Item(String id)
        {
            //web.configから接続文字列を取得
            String cnstr = ConfigurationManager.ConnectionStrings[
                "mvcdbConnectionString"].ConnectionString;
            //データベースに接続する
            DataContext dc = new DataContext(cnstr);
            //商品情報を取得
            ProductItemModel model = new ProductItemModel();

            try
            {
                //商品情報を取得
                model.Product = (from p in dc.GetTable<TProduct>()
                                 where p.id == id
                                 select p
                                    ).Single<TProduct>();
                //商品詳細情報を取得
                model.ProductDetail = (from p in dc.GetTable<TProductDetail>()
                                       where p.id == id
                                       select p
                                                ).Single<TProductDetail>();
            }
            catch
            {
                return Redirect("/Home/Error");
            }

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
