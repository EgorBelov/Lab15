using LAB_15.Models;
using Lab15.Commands;
using Lab15.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Lab15.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private Random rnd = new Random();

        private MyCollection<Person> _MyCollection1 = new MyCollection<Person>();
        public MyCollection<Person> MyCollection1
        {
            get
            {
                return _MyCollection1;
            }
            set
            {
                Set(ref _MyCollection1, value);
            }
        }



        #region CollectionTextView : string - содержимое коллекции
        private string _CollectionTextView = "Коллекция пустая";
        public string CollectionTextView
        {
            get
            {
                return _CollectionTextView;
            }
            set
            {
                Set(ref _CollectionTextView, value);
            }
        }
        #endregion

        #region добавление элемента

        #region PersonNameToAdd : string
        private string _PersonNameToAdd = "";
        public string PersonNameToAdd
        {
            get
            {
                return _PersonNameToAdd;
            }
            set
            {
                Set(ref _PersonNameToAdd, value);
            }
        }
        #endregion

        #region PersonAgeToAdd : string
        private string _PersonAgeToAdd = "";
        public string PersonAgeToAdd
        {
            get => _PersonAgeToAdd;
            set
            {
                Set(ref _PersonAgeToAdd, value);
            }
        }
        #endregion

        #region AddMessage : string
        private string _AddMessage = "";
        public string AddMessage
        {
            get => _AddMessage;
            set
            {
                Set(ref _AddMessage, value);
            }
        }
        #endregion

        #region AddPerson : ICommand
        public ICommand AddPerson { get; }
        private bool CanAddPersonExecute(object p)
        {
            return true;
        }
        private void OnAddPersonExecuted(object p)
        {
            int speed = 0;
            if (int.TryParse(PersonAgeToAdd, out speed))
            {
                AddMessage = "Успешно";
                MyCollection1.Add(new Person(speed, PersonNameToAdd));
                CollectionTextView = MyCollection1.TextView;
            }
            else
            {
                AddMessage = "Ошибка";
            }
        }
        #endregion

        #endregion

        #region удаление элемента

        #region IndexToRemove : string
        private string _IndexToRemove = "";
        public string IndexToRemove
        {
            get
            {
                return _IndexToRemove;
            }
            set
            {
                Set(ref _IndexToRemove, value);
            }
        }
        #endregion

        #region RemoveMessage : string
        private string _RemoveMessage = "";
        public string RemoveMessage
        {
            get
            {
                return _RemoveMessage;
            } 
            set
            {
                Set(ref _RemoveMessage, value);
            }
        }
        #endregion


        #region RemoveIndex : ICommand
        public ICommand RemoveIndex { get; }
        private bool CanRemoveIndexExecute(object p)
        {
            return true;
        }
        private void OnRemoveIndexExecuted(object p)
        {
            int index = 0;
            if (int.TryParse(IndexToRemove, out index))
            {
                if (index >= 1 && index <= MyCollection1.Count)
                {
                    MyCollection1.Remove(MyCollection1[index - 1].Data);
                    RemoveMessage = "Успешно";
                }
                else
                    RemoveMessage = "Элемент не найден";

                CollectionTextView = MyCollection1.TextView;
            }
            else
            {
                RemoveMessage = "Ошибка";
            }
        }
        #endregion

        #endregion

        #region корректировка по ключу

        #region IndexToChange : string
        private string _IndexToChange = "";
        public string IndexToChange
        {
            get
            {
                return _IndexToChange;
            } 
            set
            {
                Set(ref _IndexToChange, value);
            }
        }
        #endregion

        #region PersonNameToChange : string
        private string _PersonNameToChange = "";
        public string PersonNameToChange
        {
            get
            {
                return _PersonNameToChange;
            } 
            set
            {
                Set(ref _PersonNameToChange, value);
            }
        }
        #endregion

        #region PersonAgeToChange : string
        private string _PersonAgeToChange = "";
        public string PersonAgeToChange
        {
            get
            {
                return _PersonAgeToChange;
            } 
            set
            {
                Set(ref _PersonAgeToChange, value);
            }
        }
        #endregion

        #region ChangeMessage : string
        private string _ChangeMessage = "";
        public string ChangeMessage
        {
            get
            {
                return _ChangeMessage;
            } 
            set
            {
                Set(ref _ChangeMessage, value);
            }
        }
        #endregion


        #region ChangeItem : ICommand
        public ICommand ChangeItem { get; }
        private bool CanChangeItemExecute(object p)
        {
            return true;
        }
        private void OnChangeItemExecuted(object p)
        {


            int key = 0;
            int speed = 0;
            if (int.TryParse(IndexToChange, out key) && int.TryParse(PersonAgeToChange, out speed) && key >= 1 && key <= MyCollection1.Count)
            {
                    try
                    {
                        MyCollection1[key - 1].Data = new Person(speed, PersonNameToChange);
                        ChangeMessage = "Успешно";
                    }
                    catch (Exception)
                    {
                        ChangeMessage = "Элемент не найден";
                    }

                CollectionTextView = MyCollection1.TextView;
            }
            else
            {
                ChangeMessage = "Ошибка";
            }
        }
        #endregion

        #endregion


        #region сохранить в файл

        #region LoadToBinary : ICommand
        public ICommand LoadToBinary { get; }
        private bool CanLoadToBinaryExecute(object p)
        {
            return true;
        }
        private void OnLoadToBinaryExecuted(object p)
        {
            using (BinaryWriter bw = new BinaryWriter(new FileStream("person.bin", FileMode.Create)))
            {
                foreach (var item in MyCollection1)
                {
                    bw.Write(item.Post);
                    bw.Write(item.Age);
                }
            }
        }
        #endregion

        #region LoadToJSON : ICommand
        public ICommand LoadToJSON { get; }
        private bool CanLoadToJSONExecute(object p)
        {
            return true;
        }
        private void OnLoadToJSONExecuted(object p)
        {
            using (FileStream fs = new FileStream("person.json", FileMode.Create))
            {
                JsonSerializer.Serialize(fs, MyCollection1);
            }
        }
        #endregion

        #region LoadToXML : ICommand
        public ICommand LoadToXML { get; }
        private bool CanLoadToXMLExecute(object p)
        {
            return true;
        }
        private void OnLoadToXMLExecuted(object p)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(MyCollection1.GetType());
            using (FileStream fs = new FileStream("person.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, MyCollection1);
            }
        }
        #endregion

        #endregion


        #region загрузить из файла


        #region LoadMessage : string
        private string _LoadMessage = "";
        public string LoadMessage
        {
            get
            {
                return _LoadMessage;
            } 
            set
            {
                Set(ref _LoadMessage, value);
            }
        }
        #endregion

        #region LoadFromBinary : ICommand
        public ICommand LoadFromBinary { get; }
        private bool CanLoadFromBinaryExecute(object p)
        {
            return true;
        }
        private void OnLoadFromBinaryExecuted(object p)
        {
            try
            {
                using (BinaryReader br = new BinaryReader(new FileStream("person.bin", FileMode.Open)))
                {
                    var collection = new MyCollection<Person>();
                    while (br.PeekChar() > -1)
                    {
                        string name = br.ReadString();
                        int age = br.ReadInt32();
                        Person s = new Person(age, name);
                        collection.Add(s);
                    }

                    MyCollection1 = collection;
                }
                CollectionTextView = MyCollection1.TextView;
                LoadMessage = "Успешно";
            }
            catch (Exception)
            {
                LoadMessage = "Файл не найден";
            }
        }
        #endregion

        #region LoadFromJSON : ICommand
        public ICommand LoadFromJSON { get; }
        private bool CanLoadFromJSONExecute(object p)
        {
            return true;
        }
        private void OnLoadFromJSONExecuted(object p)
        {
            try
            {
                using (FileStream fs = new FileStream("person.json", FileMode.Open))
                {
                    List<Person> col = JsonSerializer.Deserialize<List<Person>>(fs);
                    MyCollection1 = new MyCollection<Person>();
                    foreach (Person person in col) MyCollection1.Add(person);
                }
                CollectionTextView = MyCollection1.TextView;
                LoadMessage = "Успешно";
            }
            catch (Exception)
            {
                LoadMessage = "Файл не найден";
            }
        }
        #endregion

        #region LoadFromXML : ICommand
        public ICommand LoadFromXML { get; }
        private bool CanLoadFromXMLExecute(object p)
        {
            return true;
        }
        private void OnLoadFromXMLExecuted(object p)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(MyCollection1.GetType());
                using (FileStream fs = new FileStream("person.xml", FileMode.OpenOrCreate))
                {
                    MyCollection1 = xmlSerializer.Deserialize(fs) as MyCollection<Person>;
                }
                CollectionTextView = MyCollection1.TextView;
                LoadMessage = "Успешно";
            }
            catch (Exception)
            {
                LoadMessage = "Файл не найден";
            }
        }
        #endregion

        #endregion


        public MainViewModel()
        {
            MyCollection1 = new MyCollection<Person>();
            AddPerson = new LambdaCommand(OnAddPersonExecuted, CanAddPersonExecute);
            RemoveIndex = new LambdaCommand(OnRemoveIndexExecuted, CanRemoveIndexExecute);
            ChangeItem = new LambdaCommand(OnChangeItemExecuted, CanChangeItemExecute);


            LoadToBinary = new LambdaCommand(OnLoadToBinaryExecuted, CanLoadToBinaryExecute);
            LoadToJSON = new LambdaCommand(OnLoadToJSONExecuted, CanLoadToJSONExecute);
            LoadToXML = new LambdaCommand(OnLoadToXMLExecuted, CanLoadToXMLExecute);
            LoadFromBinary = new LambdaCommand(OnLoadFromBinaryExecuted, CanLoadFromBinaryExecute);
            LoadFromJSON = new LambdaCommand(OnLoadFromJSONExecuted, CanLoadFromJSONExecute);
            LoadFromXML = new LambdaCommand(OnLoadFromXMLExecuted, CanLoadFromXMLExecute);
        }
    }
}
