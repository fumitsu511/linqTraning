using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {

            Func<int?, int?, int?> NullableAdditon = (sum, current) =>
            {
                if (sum == null && current == null)
                    return null;

                if (sum == null)
                    return current;

                if (current == null)
                    return sum;

                return sum + current;
            };

            var list = new List<beans>()
            {
                new beans(1,"1", new int?[2]{0,1}, 1),
                new beans(1,"2", new int?[2]{1,1}, 1),

                new beans(2,"3", new int?[2]{0,2}, 1),
                new beans(2,"4", new int?[2]{2,2}, 1),

                new beans(3,"5", new int?[2]{0,3}, 1),
                new beans(3,"6", new int?[2]{3,3}, 1),

                new beans(4,"7", new int?[2]{0,4}, 1),
                new beans(4,"8", new int?[2]{null,null}, null),

                new beans(5,"9", new int?[2]{null,null}, null),
                new beans(5,"10", new int?[2]{null,null}, null),
            };

            var group = list.GroupBy(x => x.id).ToList();

            var resultList = new List<beans>();

            foreach (var item in group)
            {
                var result = new beans();

                result.id = item.Key;

                result.val = item.Select(x => x.val).Aggregate((sum, current) =>
                 {
                     return new int?[2] 
                     {
                         NullableAdditon(sum[0], current[0]),
                         NullableAdditon(sum[1], current[1])
                     };

                 });

                result.num = item.Select(x => x.num).Aggregate((sum, current) =>
                {
                    return NullableAdditon(sum, current);
                });

                resultList.Add(result);

                Console.WriteLine($"id   : {result.id}");
                Console.WriteLine($"val1 : {result.val[0]}");
                Console.WriteLine($"val2 : {result.val[1]}");
                Console.WriteLine($"num  : {result.num}");
                Console.WriteLine(string.Empty);
            }

            Console.WriteLine("end");
            Console.ReadLine();
        }
    }

    public class beans
    {
        public beans(int _id, string _hoge, int?[] _val, int? _num)
        {
            id = _id;
            hoge = _hoge;
            val = _val;
            num = _num;
        }

        public beans() { }

        public int id { set; get; }

        public string hoge { set; get; }

        public int?[] val { set; get; }

        public int? num { set; get; }
    }
}
