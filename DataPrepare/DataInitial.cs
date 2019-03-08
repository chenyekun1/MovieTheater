using MovieTheater.Data;
using MovieTheater.Models;
using System;
using System.Linq;

namespace MovieTheater.DataPrepare
{
    public sealed class DbInitializer
    {
        public static void Initialize(WebContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Count() == 0)
            {
                var cus = new Customer { CustomerName = "chen", CustomerPwd = "1234" };
                context.Customers.Add(cus);
                context.SaveChanges();
            }

            if (context.Admins.Count() == 0)
            {
                var admin = new Admin { AdminName = "admin", AdminPwd = "1234" };
                context.Admins.Add(admin);
                context.SaveChanges();
            }

            if (context.MovieCategories.Count() == 0)
            {
                var categories = new MovieCategory[]
                {
                    new MovieCategory{CategoryName = "动作"},
                    new MovieCategory{CategoryName = "冒险"},
                    new MovieCategory{CategoryName = "娱乐"},
                };
                foreach (var category in categories)
                {
                    context.MovieCategories.Add(category);
                }
                context.SaveChanges();
            }

            if (context.MovieCountries.Count() == 0)
            {
                var countries = new MovieCountry[]
                {
                    new MovieCountry{CountryName = "中国"},
                    new MovieCountry{CountryName = "美国"},
                };
                foreach (var country in countries)
                {
                    context.MovieCountries.Add(country);
                }
                context.SaveChanges();
            }

            if (context.Movies.Count() == 0)
            {
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
                        new Movie{MovieName="流浪地球",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="郭帆",
                                MovieGrade="9.3",
                                MovieActor="吴京",
                                MovieDescription="太阳即将毁灭，人类在地球表面建造出巨大的推进器，寻找新家园。然而宇宙之路危机四伏，为了拯救地球，为了人类能在漫长的2500年后抵达新的家园，流浪地球时代的年轻人挺身而出，展开争分夺秒的生死之战。",
                                MovieImg="lldq.jpg",
                                MoviePrice=36},

                        new Movie{MovieName="一吻定情",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="陈玉珊",
                                MovieGrade="8.6",
                                MovieActor="王大陆",
                                MovieDescription="笨蛋爱上天才，会有结果吗？平凡女孩原湘琴（林允 饰）喜欢上了天才少年江直树（王大陆 饰），在她表白失败准备放弃之际，爸爸居然带着自己搬进了直树家里？！一个猛追，一个猛逃，热闹欢腾的纯真高中生活就此上演。朝夕相处中，直树渐渐被湘琴乐观的无畏精神吸引，他开始怀疑：湘琴究竟是人生偏差、还是自己的命中注定?",
                                MovieImg="ywdq.jpg",
                                MoviePrice=42},

                        new Movie{MovieName="朝花夕誓-于离别之朝束起约定之花",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="筱原俊哉，冈田磨里",
                                MovieGrade="9.2",
                                MovieActor="None",
                                MovieDescription="长生不老的“伊奥鲁夫”族人以织布为生，因远离尘世而被誉为“离别一族”。随着外界对长寿奥秘的追寻，“伊奥鲁夫”族人惨遭灭顶之灾。族中少女玛奇亚在绝望之际，意外捡到人类婴儿艾瑞尔，并决定将之抚养成人。婴儿、顽童、叛逆、成熟乃至老去，少女以“母亲”的身份陪伴着孩子长大成人，成家立业；也以长寿一族的目光注视着人类生老病死，爱恨离别。同样的季节，相异的时间；时光荏苒，两人的羁绊逐渐加深。由两个孤独灵魂相遇编织出的温馨物语，缓缓展开。",
                                MovieImg="zhxs.jpg",
                                MoviePrice=46},

                        new Movie{MovieName="夏目友人帐",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="大森贵弘",
                                MovieGrade="暂无评分",
                                MovieActor="None",
                                MovieDescription="夏目（神谷浩史 配音）在一次归还妖怪名字的过程中，结识了外祖母玲子（小林沙苗 配音）的旧识津村容莉枝岛本须美 配音）和她的儿子椋雄（高良健吾 配音）。在与其接触后，夏目的“保镖”猫咪老师（井上和彦 配音）竟第一次意外分裂成了三只！夏目能否解决危机？这对母子又与妖怪有何关系？",
                                MovieImg="zhxs.jpg",
                                MoviePrice=26},

                        new Movie{MovieName="“大”人物",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="五百",
                                MovieGrade="9.1",
                                MovieActor="王千源",
                                MovieDescription="无力维权的修车工遭遇非法强拆后，选择跳楼自杀；随着小刑警孙大圣（王千源 饰）调查的深入，发现这场看似简单的民事纠纷背后其实另有隐情；随着嫌疑目标的锁定，赵泰（包贝尔 饰）和崔京民（王迅 饰）为代表的反派集团被盯上后，公然藐视法律挑衅警察。面对反派集团金钱诱惑、顶头上司的警告劝阻、家人性命遭受威胁，这场力量悬殊的正邪较量将会如何收场……",
                                MovieImg="drw.jpg",
                                MoviePrice=28},

                        new Movie{MovieName="驯龙高手3",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="迪恩·德布洛斯",
                                MovieGrade="9.1",
                                MovieActor="None",
                                MovieDescription="统领伯克岛的酋长嗝嗝（刘昊然 配音），与阿丝翠德（亚美莉卡·费雷拉 配音）共同打造了一个奇妙而热闹的飞龙乌托邦。一只雌性光煞飞龙的意外出现，加上一个前所未有的威胁的到来，令嗝嗝和没牙仔不得不离开自己唯一的家园，前往他们本以为只存在于神话之中的隐秘之境。在发现自己真正的命运之后，飞龙与骑士将携手殊死奋战，保护他们所珍爱的一切。",
                                MovieImg="xlgs.jpg",
                                MoviePrice=42},

                        new Movie{MovieName="阿丽塔：战斗天使3",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="罗伯特·罗德里格兹",
                                MovieGrade="9.0",
                                MovieActor="罗莎·萨拉查",
                                MovieDescription="未来26世纪，科技发展，人类与机械改造人共存，弱肉强食是钢铁城唯一的生存法则。依德（克里斯托夫·沃尔兹 饰）是钢铁城著名的改造人医生，他在垃圾场发现了一个半机械少女残躯，依德医生将其拯救后为她取名阿丽塔（罗莎·萨拉扎尔 饰）。阿丽塔虽然重获生命却失去了记忆，如一个新生儿一样对这个世界充满新鲜感。在依德医生与好友雨果（基恩·约翰逊 饰）的帮助下，她逐步适应着新生活和街头险恶。一次偶然的机会，阿丽塔发现自己竟有着惊人的战斗天赋。 一次次猎杀激发着她的觉醒，阿丽塔逐渐明白自己注定为战斗而生，为正义而战。一场揭开自己身世之谜，并打破宇宙旧秩序的史诗级冒险之旅就这样展开。",
                                MovieImg="alt.jpg",
                                MoviePrice=40},

                        new Movie{MovieName="惊奇队长",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="安娜·波顿",
                                MovieGrade="暂无评分",
                                MovieActor="瑞安·弗雷克",
                                MovieDescription="惊奇队长原名Carol Danvers（布丽·拉尔森 饰），她曾是一名美国空军情报局探员，仰慕当时的惊奇队长马维尔。之后再一次意外事故中她获得了超能力，成为“惊奇女士”，在漫画中是非常典型的女性英雄人物。 她可以吸收并控制任意形态的能量，拥有众多超能力。《惊奇队长》将是漫威电影宇宙首部以女性超级英雄为主角的电影。",
                                MovieImg="jqdz.jpg",
                                MoviePrice=47},

                        new Movie{MovieName="熊出没·原始时代",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="丁亮，林汇达",
                                MovieGrade="9.1",
                                MovieActor="None",
                                MovieDescription="熊大（张伟 配音）熊二（张秉君 配音）光头强（谭笑 配音）意外穿越回恢宏的石器时代，与猛犸象、剑齿虎、史前大鸟等一众奇特生物、以及原始人类开启一段眼界大开的奇幻之旅！原始时代瑰丽非常，却又危机四伏。熊强三人组磕磕绊绊，笑料百出。一只可爱狼女一路相伴，背后竟是人族与狼族的对立。面对原始人类的不断质疑、凶猛狼族的步步紧逼、自然危机的全面爆发，熊强究竟何去何从？他们又能否回归现代？一场关于守护与成长、爱与勇气的冒险，已经拉开序幕……",
                                MovieImg="xcm.jpg",
                                MoviePrice=30},

                        new Movie{MovieName="新喜剧之王",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="周星驰，邱礼涛",
                                MovieGrade="8.0",
                                MovieActor="王宝强",
                                MovieDescription="一直有个明星梦的小镇大龄女青年如梦（鄂靖文 饰）跑龙套多年未果。她和父亲（张琪 饰）关系紧张，亲友都劝她放弃，只有男友查理（张全蛋 饰）还支持她。在剧组，如梦遇见了年少时启发她演戏的男演员马可（王宝强 饰）。但此时过气多年的马可却因内心自卑而性情狂躁，对如梦百般折磨。如梦仍乐观坚持演戏，然而一次比一次沉重的打击却接踵而来，最后她决定放弃梦想，回到父母身边找了份稳定工作，但却得知自己入围了知名导演新片的大型选角。如梦陷入艰难抉择。",
                                MovieImg="xxjzw.jpg",
                                MoviePrice=36},

                        new Movie{MovieName="飞驰人生",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="韩寒",
                                MovieGrade="8.8",
                                MovieActor="沈腾",
                                MovieDescription="曾经在赛车界叱咤风云、如今却只能经营炒饭大排档的赛车手张驰（沈腾 饰）决定重返车坛挑战年轻一代的天才。然而没钱没车没队友，甚至驾照都得重新考，这场笑料百出不断被打脸的复出之路，还有更多哭笑不得的窘境在等待着这位过气车神……",
                                MovieImg="fcrs.jpg",
                                MoviePrice=38},

                        new Movie{MovieName="疯狂的外星人",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="宁浩",
                                MovieGrade="8.5",
                                MovieActor="黄渤",
                                MovieDescription="耿浩（黄渤 饰）与一心想发大财的好兄弟大飞（沈腾 饰），经营着各自惨淡的“事业”，然而“天外来客”（徐峥 饰）的意外降临，打破了二人平静又拮据的生活。神秘的西方力量也派出“哼哈二将”在全球搜查外星人行踪。啼笑皆非的跨物种对决，别开生面的“星战”，在中国某海边城市激情上演",
                                MovieImg="fkdwxr.jpg",
                                MoviePrice=36},

                        new Movie{MovieName="蓝色生死恋",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="王才涛",
                                MovieGrade="7.6",
                                MovieActor="徐凯",
                                MovieDescription="根据经典IP电视剧改编，二十年之后以电影形式重回大众视野。讲述了两个同天出生的女孩儿因无意间被掉包，从而牵连出几位主角间命运的错位和爱情的纠葛的故事。",
                                MovieImg="lsssl.jpg",
                                MoviePrice=48},

                        new Movie{MovieName="神探蒲松龄",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="严嘉",
                                MovieGrade="7.8",
                                MovieActor="成龙",
                                MovieDescription="一代文豪蒲松龄（成龙 饰）执阴阳判化身神探，与捕快严飞（林柏宏 饰）联手追踪金华镇少女失踪案。蒲松龄带领“猪狮虎”“屁屁”“忘忧”“千手”等一众小妖深入案情，在找寻真相的过程中，牵扯出一段旷世奇恋。",
                                MovieImg="stpsl.jpg",
                                MoviePrice=43},

                        new Movie{MovieName="小猪佩奇过大年",
                                MovieCategoryId=1,
                                MovieCountryId=1,
                                MovieDirector="张大鹏",
                                MovieGrade="5.7",
                                MovieActor="None",
                                MovieDescription="汤圆、饺子和爸爸妈妈生活在一个温暖的家庭里。除夕清晨，汤圆和饺子兴高采烈地穿着好准备去爷爷奶奶家，却看到正在家里打扫的爸爸妈妈，原来爸爸妈妈忘记告诉他们一个秘密 - 姥姥姥爷从南方过来陪汤圆饺子一家过春节。为了逗孩子们开心，爸爸讲起了小猪佩奇去儿童节的故事。想念孩子们的爷爷奶奶提早结束了三亚的旅程，赶回到了北方。奶奶抵达后为孩子们讲述了小猪佩奇在儿童节的第二天遇到了意想不到的事情。爷爷奶奶的到来为家中增添了浓浓的年味并一起体验了传统的中国习俗 - 挂中国节，剪窗花，贴春联，发红包等。他们和佩奇乔治一样，每经历一件事情，就会让他们更加热爱彼此，热爱家人，热爱生活。",
                                MovieImg="xzpq.jpg",
                                MoviePrice=43}
                        };

                foreach (var movie in movies)
                {
                    context.Movies.Add(movie);
                }
                context.SaveChanges();
            }
        }
    }
}