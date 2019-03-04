using MovieTheater.Data;
using MovieTheater.Models;
using System;
using System.Linq;

namespace MovieTheater.DataPrepare
{
    internal sealed class DbInitializer
    {
        public static void Initialize(WebContext context)
        {
        context.Database.EnsureCreated();

        if (context.Movies.Any())
        {
            Console.WriteLine("The Database is not empty!");
            return;
        }


        #region TestDataInput
            
            var movies = new Movie[]
            {
                new Movie{MovieName="绿皮书",
                          MovieCategoryId=1,
                          MovieCountryId=1,
                          MovieDirector="彼得·法雷里",
                          MovieGrade="9.6",
                          MovieActor="None",
                          MovieDescription="一名黑人钢琴家，为前往种族歧视严重的南方巡演,找了一个粗暴的白人混混做司机。在一路开车南下的过程里，截然不同的两人矛盾不断，引发了不少争吵和笑料。但又在彼此最需要的时候，一起共渡难关。行程临近结束，两人也慢慢放下了偏见...... 绿皮书，是一本专为黑人而设的旅行指南，标注了各城市中允许黑人进入的旅店、餐馆。电影由真实故事改编。",
                          MovieImg="lps.jpg",
                          MoviePrice=47},

            };

        #endregion
        }
    }
}