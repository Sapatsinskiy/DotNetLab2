using System.Collections;

namespace DotNetLab2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string mess = "Hello Hello Hello Hello!";
            string el = "l";
            string newMess = mess.InvertLine();
            Console.WriteLine("Демонстрацiя розширених методiв String");
            Console.WriteLine($"Початковий рядок: {mess}");
            Console.WriteLine($"Перевернутий рядок: {newMess}");

            int count = mess.ContainLine(el);
            Console.WriteLine($"Пiдрахунок кiлькостi <{el}> в рядку: {mess} = {count}");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Демонстрацiя розширених методiв для одновимiрних масивiв");
            int[] numbers = {1, 2, 3, 4, 1, 1};
            
            Console.WriteLine($"Масив чисел {string.Join(", ", numbers)}");
            count = numbers.CountElemtsInArr(1);

            Console.WriteLine($"Пiдрахунок кiлькостi 1 в масиві {count}");
            string[] words = {"car", "plane", "car", "tank"};
            count = words.CountElemtsInArr("car");
            Console.WriteLine($"Масив {string.Join(", ", words)}");
            Console.WriteLine($"Пiдрахунок кiлькостi car в масивi: {count}");

            string[] uniqueWords = words.GetUniqueElements();
            Console.WriteLine($"Унiкальнi елементи в масивi: {string.Join(", ", uniqueWords)}");

            Console.WriteLine();
            Console.WriteLine();
            var peopleDictionary = new ExtendedDictionary<string, string, string>();
            peopleDictionary.Add("Tom Shelby", "+123456789", "tom@gmail.com");
            peopleDictionary.Add("Tommy Tommy", "+987654321", "tot@gmail.com");

            Console.WriteLine("Виведення словника");
            foreach (var person in peopleDictionary)
            {
                Console.WriteLine($"Iм'я: {person.Key}, Телефон: {person.Value1}, Email: {person.Value2}");
            }


            Console.WriteLine("Кiлькiсть елементiв у словнику: " + peopleDictionary.Count());
            Console.WriteLine("Перевiрка чи мiстить ключ <Tommy Tommy>: " + peopleDictionary.ContainsKey("Tommy Tommy"));
            Console.WriteLine("Перевiрка чи мiстить значення '+123456789' та 'tom@gmail.com': " +
                              peopleDictionary.ContainsValue("+123456789", "tom@gmail.com"));
            
            var manEl = peopleDictionary["Tommy Tommy"];
            Console.WriteLine($"Виведення даних за ключем <Tommy Tommy> Iм'я: {manEl.Key}, Телефон: {manEl.Value1}, Email: {manEl.Value2}");

            Console.WriteLine($"Видалення <Tommy Tommy>: {peopleDictionary.Remove("Tommy Tommy")}") ;
            
            Console.WriteLine("Кiлькiсть елементiв пiсля видалення: " + peopleDictionary.Count());

        }
    }
    public static class StringExtensions
    {
        public static string InvertLine(this string line)
        {
            string newLine = "";
            for (int i = line.Length - 1; i >= 0; i--)
            {
                newLine += line[i];
            }
            return newLine;
        }
        public static int ContainLine(this string line, string el)
        {
            int count = 0;
            line = line.ToLower();
            el = el.ToLower();

            while (true)
            {
                int index = line.IndexOf(el);
                if (index != -1)
                {
                    count++;
                    line = line.Substring(index + el.Length);
                }
                else
                {
                    break;
                }
            }
            return count;
        }
    }
    public static class ArrayExtensions
    {
        public static int CountElemtsInArr<Type>(this Type[] array, Type value) where Type : IEquatable<Type>
        {
            int count = 0;

            foreach (Type element in array)
            {
                if (element.Equals(value))
                {
                    count++;
                }
            }
            return count;
        }
        public static Type[] GetUniqueElements<Type>(this Type[] array)
        {
            HashSet<Type> uniqueEl = new HashSet<Type>(array);
            return uniqueEl.ToArray();
        }
    }

    public class ExtendedDictionaryElement<T, U, V>
    {
        public T Key { get; }
        public U Value1 { get; }
        public V Value2 { get; }

        public ExtendedDictionaryElement(T key, U value1, V value2)
        {
            Key = key;
            Value1 = value1;
            Value2 = value2;
        }
    }

    public class ExtendedDictionary<T, U, V> : IEnumerable<ExtendedDictionaryElement<T, U, V>>
    {
        private Dictionary<T, ExtendedDictionaryElement<T, U, V>> elements = new Dictionary<T, ExtendedDictionaryElement<T, U, V>>();

        public bool Add(T key, U value1, V value2)
        {
            if (!elements.ContainsKey(key))
            {
                elements[key] = new ExtendedDictionaryElement<T, U, V>(key, value1, value2);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Remove(T key)
        {
            return elements.Remove(key);
        }
        public bool ContainsKey(T key)
        {
            return elements.ContainsKey(key);
        }
        public bool ContainsValue(U value1, V value2)
        {
            foreach (var element in elements.Values)
            {
                if (EqualityComparer<U>.Default.Equals(element.Value1, value1) && EqualityComparer<V>.Default.Equals(element.Value2, value2))
                {
                    return true;
                }
            }
            return false;
        }
        public ExtendedDictionaryElement<T, U, V> this[T key]
        {
            get
            {
                if (elements.ContainsKey(key))
                {
                    return elements[key];
                }
                return null;
            }
        }
        public int Count()
        {
            return elements.Count;
        }

        public IEnumerator<ExtendedDictionaryElement<T, U, V>> GetEnumerator()
        {
            return elements.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
