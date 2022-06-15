using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_15.Models
{
    public class Person : IInit, IComparable, ICloneable, IEquatable<object>
    {
        protected int age;
        public string post;
        protected static Random rnd = new Random();
        public string Post
        {
            get { return post; }
            set { post = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public Person()
        {
            post = RandomString(4);
            Init();
        }
        public Person(int age)
        {
            Age = age;
            post = "никто";
        }
        protected string RandomString(int length)
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder builder = new StringBuilder(length);

            for (int i = 0; i < length; ++i)
                builder.Append(chars[rnd.Next(chars.Length)]);

            return builder.ToString();
        }

        public Person(int a, int b, int c)
        {
            Age = rnd.Next(15);
            post = RandomString(4);

        }
        public Person(int age, string s)
        {
            Age = age;
            post = s;

        }
        public Person(int age, string s, int idn)
        {
            Age = age;
            post = s;


        }
        virtual public void ShowInfo()
        {
            Console.WriteLine($"Я человек, мой возраст: {age}, моя должность: {post}");
        }
        public void Show()
        {
            Console.WriteLine($"Я человек, мой возраст: {age}, моя должность: {post}");
        }
        public override string ToString()
        {
            return String.Format($"Мой возраст: {age} и мое имя {post} ");
        }
        public virtual int CompareTo(object obj)
        {
            Person person = obj as Person;
            if (this.Age - person.Age > 0) return 1;
            if (this.Age - person.Age < 0) return -1;
            return 0;
        }
        public virtual object Clone()
        {
            return new Person(Age, Post); // глубокое
        }
        public virtual object ShallowCopy()
        {
            return MemberwiseClone(); // поверхностное
        }



        public virtual void Init()
        {
            Age = rnd.Next(100);
        }
        public override bool Equals(object pers)
        {
            Person other = pers as Person;
            if (other == null)
                return false;

            if ((this.Age == other.Age) && (this.Post == other.Post))
                return true;
            else
                return false;

        }
        public override int GetHashCode()
        {
            return Math.Abs(Age + (((int)post[0] - 1040) % 10));
        }
    }
}
