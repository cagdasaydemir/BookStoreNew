using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreNew
{
    public abstract class BaseClass
    {
        const int MAX_NUMS = 999999999;
        const int MIN_NUMS = 100000000;
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        Random random = new Random();


        public BaseClass()
        {
            Id = random.Next(MIN_NUMS, MAX_NUMS);
            CreatedTime = DateTime.Now;
            UpdatedTime = DateTime.Now;
        }
    }
}
