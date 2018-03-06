using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShopping.Models
{
    //<summary>
    //商品情報のモデルクラス
    //</summary>
    public class ProductModel
    {
        //商品リスト
        public IQueryable<TProduct> Products { get; set;}
        //カテゴリ
        public IQueryable<TCategory> Categories { get; set; }

        //カレントページ
        public int CurrentPage { get; set; }
        //前ページがあるか
        public bool HasPrevPage { get; set; }
        //次ページがあるか
        public bool HasNextPage { get; set; }

        //カテゴリID
        public int? Category { get; set; }
    }
}