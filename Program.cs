/*
 * Данное приложение имеет название планировщик, оно было написано, с тем чтобы показать мой (Пирогова Эдуарда Руслановича) уровень познания языка С#, понимания ООП, а также в области баз данных (уровень 3).
 * В целом. я сам проделанной работой остался доволен, так как в процессе впервые работал с делегатами, то есть открыл для себя новый инструмент.
 * Конечно, можно было бы добавить ещё больше функционала, сделать рефакторинг кода, где-то что-то упростить, но работа над этим приложением и так ведётся слишком долго. Явно дольше, чем это разумно.
 * В случае, если код кажется сложным и разбираться в нём долго, моэно заглянуть в репозиторий, с которого был скачан данный файл, есть схемы UML (или нечто похожее на них), которые помогут понять Вам сущность программы.
 * Для работы с базой данных необходимо скачать приложение "MySql Workbench". Решение о том, что работать буду с Mysql и данным приложением, было принято для того, чтобы пользователь мог продолжить работать с базой данных
 * при помощи "MySql Workbench", если вдруг по каким-либо соображениям пользоваться моии приложением пропало желание или попросту нет возможности.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Pelipas //планировщик
{
    class Program
    {
        static void Main(string[] args)
        {

            Menu lolka = new Menu();
            lolka.Add_Option("Добавить дело");
            lolka.Add_Option("Просмотреть список дел (из этого пункта производится удаление и редактирование)");
            lolka.Add_Option("Выйти из программы");
            Spisok_Del spisok_del = new Spisok_Del();
            DataBase test;
            Privetstvie();
            BD(out test);
            test.Make_connection_string();//больше ничего менять не надо, потому собираем финальную строку подключения
            test.Open_connection();//открываем подключение
            Console.WriteLine("Введите имя новой базы данных");
            string name = Console.ReadLine();
            test.CREATE_DATA_BASE(name);
            test.Add_table("dela");//добавляем таблицу
            test.Add_table("dela12");
            test.Add_table("dela13");
            test.Add_table(name);
            test.Chose_table();//выбираем таблицу, с которой решили работать
            Console.WriteLine("Программа поддерживает управление стрелочками, а также клавишами W, S. Для того чтобы выбрать пункт меню, нужно нажать Enter.\nКогда вы находитесь в просмотре списка дел, нажмите Enter, чтобы перейти в режим выбора элемента. Затем клавишами W,S или стрелками выбирайте нужный пункт меню для редактирования. Затем вновь Enter\nЧтобы в любой момент выйти назад, нажмите Esc");
            Console.ReadLine();
            while (true)
            {
                lolka.Show();
                switch (lolka.curpos)
                {
                    case 0:
                        {
                            Delo new_delo = new Delo();
                            new_delo.Add_Zagolovok_for_set_info("Выбрать сложность");
                            new_delo.Add_Zagolovok_for_set_info("Выбрать дату");
                            new_delo.Add_Zagolovok_for_set_info("Выбрать время");
                            new_delo.Add_Zagolovok_for_set_info("Выбрать название");
                            new_delo.Add_Zagolovok_for_set_info("Установить статус актуальности");
                            new_delo.Add_Zagolovok_for_set_info("Редактирование окончено");
                            new_delo.Add_operation_for_set_info(0, new_delo.Change_Difficult);
                            new_delo.Add_operation_for_set_info(1, new_delo.Change_Date);
                            new_delo.Add_operation_for_set_info(2, new_delo.Change_Time);
                            new_delo.Add_operation_for_set_info(3, new_delo.Change_name);
                            new_delo.Add_operation_for_set_info(4, new_delo.Change_Actual);
                            new_delo.Set_Info();
                            spisok_del.AddDelo(new_delo, ref test);
                           
                        }
                        break;
                    case 1:
                        {
                            spisok_del.Show_Spisok(ref test);
                        }
                        break;
                    case 2:
                        {
                            Environment.Exit(0);
                        }
                        break;
                }
            }
        }
        static void BD(out DataBase test)
        {
            Console.WriteLine("Данная программа поддерживает работу с базой данных, потому Вам необходимо ввести некоторые данные\n");
            string name_of_bd = "localhost";
            string name_of_user = "root";
            string name_of_database = "scheduler";
            string name_of_port = "3306";
            string name_of_password = "Тетриандох228";
            Console.WriteLine("1. Я уже видел Ваш код и хочу запустить с стандартными параметрами");
            Console.WriteLine("2. Я хочу ввести свои параметры");
            int chose;
            chose = Int32.Parse(Console.ReadLine());
            if (chose==1)
            {
                test = new DataBase();
            }
            else
            {
                Console.WriteLine("Введите имя сервера, например, localhost, если сервер локальный");
                name_of_bd = Console.ReadLine();
                Console.WriteLine("Введите имя пользователя, например, root");
                name_of_user = Console.ReadLine();
                Console.WriteLine("Введите имя базы данных, например, scheduler");
                name_of_database = Console.ReadLine();
                Console.WriteLine("Введите порт, например, 3306");
                name_of_port = Console.ReadLine();
                Console.WriteLine("Введите пароль, например, Тетриандох228");
                name_of_user = Console.ReadLine();
                test = new DataBase(name_of_bd, name_of_user, name_of_database, name_of_port, name_of_password, true);
            }
        }
        static void Privetstvie()
        {
            Menu privetstvie = new Menu();
            privetstvie.Set_center_for_text("Планировщик");
            privetstvie.gotoxy(0, Console.WindowHeight / 2);
            privetstvie.Change_color_for_text(privetstvie.currenttext, ConsoleColor.Green);
            Console.WriteLine("© Пирогов Эдуард Русланович 2020");
            Task.Delay(2000).GetAwaiter().GetResult();
            Console.Clear();
        }
    }
    class Wait
    {
        public void Wait_for(string where)
        {
            Console.WriteLine("Для выхода из " + where + " нажмите любую клавишу, например, Enter или Escape");
            Console.ReadKey();
        }
    }
    class Menu
    {
        private int menuIndic = 0; // показывает, какой сейчас пункт меню
        public char menu { get; private set; } // клавиша
        public int curpos { get; set; } // финальный выбор пользователя
        private List<string> Tasks = new List<string>(); // аналог вектора из с++, умный массив, с которым легко работать. Да, это сомнительно решение вообще добавлять ещё один список, однако мало ли будет добавлена функция сортировки и нужно будет отсортировать только видимую часть. В общем, мне данное решение кажется обоснованным.
        public string currenttext { get; private set; }
        private List<string> Something_to_print_with_distance = new List<string>();
        public List<List<string>> Range { get; private set; } // это нужно для того, чтобы можно было при выводе
        public delegate void Should_be();
        private int ideal_width;
        private int ideal_lengh;//данное поле нужно для того, чтобы поля, печатающиеся после заголовка, были той же длины. Данное поле будет сравниваться с остальными
        private int Count_Lines = -1;
        private int CurHeightOfCursor = Console.WindowHeight / 2;
        private int Height_of_Zagolovok = 0;
        private int save_position = -1;//нужно для отслеживания предыдущего индекса для методов UpArrow и DownArrow
        private bool down_arrow = false;
        private int previous_exit_from_for = 0;
        public Menu(ref List<string> menusha)
        {
            Tasks = menusha;
            Range = new List<List<string>>();
        }
        public Menu()
        {
            Range = new List<List<string>>();
        }//добавляем лазейку для тех, кто хочет использовать только функции вроде gotoxy, но не хочет работать с Show();
        public void Change_color_for_text(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color; // устанавливаем цвет
            Console.WriteLine(text);
            Console.ResetColor(); // сбрасываем в стандартный
        }
        public void gotoxy(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        } // перемещает курсор
        public void Set_center_for_text(string text)
        {
            int width = Console.WindowWidth;
            if (text.Length < width)
            {
                currenttext = text.PadLeft((width - text.Length) / 2 + text.Length, ' ');
            }
        }// устанавливает текст по центру консоли
        public void Print_to_Center(string text, bool WriteLineOrWrite, int misus_heigh)
        {
            CurHeightOfCursor += misus_heigh;
            gotoxy(0, CurHeightOfCursor);//регулируем по высоте
            Set_center_for_text(text);
            if (WriteLineOrWrite)
                Console.WriteLine(currenttext);
            else Console.Write(currenttext);
        }
        public void Show()
        {
            if (Tasks.Count() < 1)
            {
                gotoxy(0, Console.WindowHeight / 2);
                Set_center_for_text("Нечего отображать");
                return;//раз отображать нечего - выходим из метода
            }
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                for (int i = 0; i < Tasks.Count(); i++)
                {
                    if (i == 0) gotoxy(0, Console.WindowHeight / 2);
                    if (i == menuIndic)
                    {
                        Set_center_for_text(Tasks[i]);
                        Change_color_for_text(currenttext, ConsoleColor.Cyan);
                    }
                    else
                    {
                        Set_center_for_text(Tasks[i]);
                        Console.WriteLine(currenttext);
                    }
                }
                //menu = Console.ReadKey().KeyChar;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        {
                            return;
                        }
                    // если нажали на Esc, то выход
                    case ConsoleKey.UpArrow:
                        {
                            if (menuIndic != 0)
                            {
                                menuIndic--;
                                continue;
                            }
                        }
                        break;
                    case ConsoleKey.W:
                        {
                            if (menuIndic != 0)
                            {
                                menuIndic--;
                                continue;
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (menuIndic != Tasks.Count() - 1) menuIndic++;
                        }
                        break;
                    case ConsoleKey.S:
                        {
                            if (menuIndic != Tasks.Count() - 1) menuIndic++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        {
                            curpos = menuIndic;
                            return;
                        }
                }
            }
        }
        public int Choice()
        {
            return curpos;
        }
        public void Clear_Options()
        {
            Tasks.Clear();
        }//удаление всех элементов
        public void Delete_Option()
        {
            Tasks.RemoveAt(curpos);
        }
        public void Add_Option(string something)
        {
            Tasks.Add(something);
        }
        public void Add_Zagolovok(string something)
        {
            Something_to_print_with_distance.Add(something);
        }
        public void Clear_Zagolovok()
        {
            Something_to_print_with_distance.Clear();
        }//удаление всех элементов заголовков
        public void Delete_Zagolovok(string something)
        {
            try
            {
                Something_to_print_with_distance.Remove(something);
            }
            catch
            {
                gotoxy(0, Console.WindowHeight / 2);
                Set_center_for_text("Что-то пошло не так. Не удалось удалить объект. Быть может, его и нет");
                Console.WriteLine(currenttext);
            }
        }
        public void Print_with_distanse(int distance)
        {
            for (int i = 0; i < Something_to_print_with_distance.Count(); i++)
            {
                if (i == 0)
                {
                    Console.Write("|{0, " + distance.ToString() + "}{1," + distance.ToString() + "}{2," + distance.ToString() + "}", "", Something_to_print_with_distance[i], "|");
                    continue;
                }
                Console.Write("{0, " + distance.ToString() + "}{1," + distance.ToString() + "}{2," + distance.ToString() + "}", "", Something_to_print_with_distance[i], "|");//Доллжно печатать заголовок через нормальную дистанцию
            }
            Console.WriteLine();//переход на новую строку
        }
        public void Print_with_norm_distanse(int stroka, int after, char Otdelenie, ConsoleColor color = ConsoleColor.White)
        {
            //ideal_width = (Console.WindowWidth / Range[stroka].Count());
            ideal_width = (Console.BufferWidth) / Range[0].Count();
            string StupidRules = Serach_the_biggest(Range[0]);
            ideal_lengh = StupidRules.Length;
            string Zagolovok = "";
            for (int i = 0; i < Range[stroka].Count(); i++)
            {
                string Stupid = Range[stroka][i];
                if (i == 0)
                {
                    string For_Print = Stupid;
                    Make_it_to_biggest(ref For_Print, StupidRules);
                    For_Print = Otdelenie + For_Print + Otdelenie;
                    Add_Space(0, after, ref For_Print);
                    Zagolovok += For_Print;
                    continue;
                }
                string For_Print_Other = Stupid;
                Make_it_to_biggest(ref For_Print_Other, StupidRules);
                Zagolovok += (Otdelenie + For_Print_Other + Otdelenie);
                Add_Space(0, after, ref Zagolovok);
            }
            Height_of_Zagolovok += 1;
            gotoxy(0, Height_of_Zagolovok);
            Set_center_for_text(Zagolovok);
            Change_color_for_text(currenttext, color);
            //Console.WriteLine(currenttext);
        }
        public void Print_with_norm_distance_zagolovok(int after, char Otdelenie, ConsoleColor color)
        {
            ideal_width = (Console.BufferWidth) / Range[0].Count();
            string StupidRules = Serach_the_biggest(Range[0]);
            ideal_lengh = StupidRules.Length;
            string Zagolovok = "";
            for (int i = 0; i < Range[0].Count(); i++)
            {
                string Stupid = Range[0][i];
                if (i == 0)
                {
                    string For_Print = Stupid;
                    Make_it_to_biggest(ref For_Print, StupidRules);
                    For_Print = Otdelenie + For_Print + Otdelenie;
                    Add_Space(0, after, ref For_Print);
                    Zagolovok += For_Print;
                    continue;
                }
                string For_Print_Other = Stupid;
                Make_it_to_biggest(ref For_Print_Other, StupidRules);
                Zagolovok += (Otdelenie + For_Print_Other + Otdelenie);
                Add_Space(0, after, ref Zagolovok);
            }
            Console.ForegroundColor = color;
            Set_center_for_text(Zagolovok);
            Console.WriteLine(currenttext);
            Console.ResetColor();
        }
        public string Serach_the_biggest(List<string> something)
        {
            string biggest = something[0];
            for (int i = 1; i < something.Count(); i++)
            {
                if (something[i].Length > biggest.Length)
                {
                    biggest = something[i];
                }
            }
            return biggest;
        }
        public void Make_it_to_biggest(ref string something_to_chek, string biggest)
        {
            if (!Is_Longer(something_to_chek, biggest))
            {
                int different = biggest.Length - something_to_chek.Length;
                Add_Space(0, different, ref something_to_chek);
            }
        }
        public void Pechat_Punktov(int kol_vo_for_pechat, int after, char Otdelenie)
        {
            if (Range.Count() < 1)
            {
                gotoxy(0, Console.WindowHeight / 2);
                Set_center_for_text("Нечего отображать");
                Console.WriteLine(currenttext);
                return;//раз отображать нечего - выходим из метода
            }
            Console.Clear();
            //List<string> SaveForChange = new List<string>();// когда юзер нажмёт на Enter, у него будет возможность выбирать нужный элемент для редактирования
            Print_with_norm_distance_zagolovok(5, '|', ConsoleColor.Yellow); // заголовок напечатан
            Height_of_Zagolovok++;
            int exit_from_for = 0;
            bool stop = false;
            if (kol_vo_for_pechat >= Range.Count())
            {
                exit_from_for = Range.Count();
            }
            else
            {
                exit_from_for = kol_vo_for_pechat + 1;
            }
            if (exit_from_for > Console.BufferHeight) Console.BufferHeight = exit_from_for + 3;//таким образом мы можем вместить весь список дел не зависимо от размера буфера
            for (int i = 1; i < exit_from_for; i++)
            {
                Print_with_norm_distanse(i, after, Otdelenie);
            }
            if (Range.Count() == exit_from_for)
            {
                stop = true;
                save_position = 1;
                Height_of_Zagolovok = 0;
            }
            else
            {
                save_position = 1;
                Height_of_Zagolovok = 0;
                Console.WriteLine();
                currenttext = " ";
                Change_color_for_text("Нажмите стрелку вниз [↓], чтобы листать дальше" +
                    "\n [Escape] - чтобы выйти из режима просмотра" +
                    "\n [Enter] - чтобы перейти в режим редактирования", ConsoleColor.Green);
                Console.WriteLine(currenttext);
            }
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        {
                            menu = 'X';
                            return;
                        }
                    // если нажали на Esc, то выход
                    case ConsoleKey.DownArrow:
                        {

                            if (!stop)
                                Down_Arrow(ref exit_from_for, ref kol_vo_for_pechat, after, Otdelenie);
                        }
                        break;
                    case ConsoleKey.Enter:
                        {
                            menuIndic = 1;
                            while (true)
                            {
                                Console.Clear();
                                int temp = exit_from_for;
                                int time;
                                Print_with_norm_distance_zagolovok(5, '|', ConsoleColor.Yellow); // заголовок напечатан
                                Height_of_Zagolovok++;
                                time = save_position;
                                if (exit_from_for == 1)
                                {
                                    time = 1;
                                    temp = kol_vo_for_pechat + 1;
                                }
                                if (save_position > exit_from_for)
                                {
                                    temp = previous_exit_from_for;
                                }
                                for (int i = time; i < temp; i++)
                                {
                                    if (i >= Range.Count())
                                    {
                                        temp = temp - Range.Count() + 1;
                                        i = 1;
                                    }
                                    if (i == menuIndic)
                                        Print_with_norm_distanse(i, after, Otdelenie, ConsoleColor.Cyan);
                                    else Print_with_norm_distanse(i, after, Otdelenie);
                                }
                                Height_of_Zagolovok = 0;
                                switch (Console.ReadKey().Key)
                                {
                                    case ConsoleKey.Escape:
                                        {
                                            curpos = -1;
                                            menu = 'X';
                                            return;
                                        }
                                    // если нажали на Esc, то выход
                                    case ConsoleKey.UpArrow:
                                        {
                                            if (menuIndic > 1)
                                            {
                                                menuIndic--;
                                                continue;
                                            }
                                        }
                                        break;
                                    case ConsoleKey.W:
                                        {
                                            if (menuIndic != 0)
                                            {
                                                menuIndic--;
                                                continue;
                                            }
                                        }
                                        break;
                                    case ConsoleKey.DownArrow:
                                        {
                                            if (menuIndic != temp - 1) menuIndic++;
                                        }
                                        break;
                                    case ConsoleKey.S:
                                        {
                                            if (menuIndic != temp - 1) menuIndic++;
                                        }
                                        break;
                                    case ConsoleKey.Enter:
                                        {
                                            curpos = menuIndic;
                                            menu = 'E';
                                            return;
                                        }
                                }
                            }

                        }

                }
            }//я конечно понимаю, что данный код уже второй раз встречается, однако тут всё же есть небольшие отличия. Тут уже в разы труднее было бы искать нормальное решение, чем просто скопипастить. Хотя. может, я попробую решить эту проблему делегатами, но это будет потом, когда буду делать рефакторинг кода.
        }
        public void Save_to_Range()
        {

            Range.Add(new List<string>());

            Count_Lines++;//мы не знаем, когда человек закончит добавлять элементы для печати с разделителями, потому самый рациональный способ это сейчас сделать заключение о том, что это конец. Иначе нужно серьёзно подумать, в какой момент инкрементировать данную переменную.
            foreach (string slovo in Something_to_print_with_distance)
            {
                Range[Count_Lines].Add(slovo);
            }
        }//тут надо объяснить, дело в том, что мне нужно чётко понимать, где заголовок, а где сами данные. Также это нужно с тем, чтобы печатать много строк, а не одну + чтобы не копипастить код Add_Zagolovok для Add_Stroka
        public void Clear_Range()
        {
            Range.Clear();
            Count_Lines = -1;
        }
        public void Cut_Zagolovki(int kol_vo_bukv)
        {
            for (int i = 0; i < Something_to_print_with_distance.Count(); i++)
            {
                if (Something_to_print_with_distance[i].Length > kol_vo_bukv)
                    Something_to_print_with_distance[i] = (Something_to_print_with_distance[i]).Substring(0, kol_vo_bukv);
            }
        }
        public bool Is_Longer(string something, string longer)
        {
            if (something.Length > longer.Length)
            {
                return true;
            }
            return false;
        }
        private void Down_Arrow(ref int exit_from_for, ref int kol_vo_for_pechat, int after, char Otdelenie)
        {
            Console.Clear();
            if (down_arrow)
            {
                exit_from_for += kol_vo_for_pechat;
                if (exit_from_for >= Range.Count())
                {
                    exit_from_for = 1;
                }
            }
            down_arrow = false;
            Height_of_Zagolovok = 0;
            Print_with_norm_distance_zagolovok(5, '|', ConsoleColor.Yellow); // заголовок напечатан
            Height_of_Zagolovok++;
            int time = exit_from_for;
            exit_from_for = exit_from_for + kol_vo_for_pechat;
            if (save_position == exit_from_for)
            {
                time = exit_from_for;
                exit_from_for += kol_vo_for_pechat;
                if (exit_from_for >= Range.Count())
                {
                    time = 1;
                    exit_from_for = kol_vo_for_pechat;
                }
            }
            save_position = time;
            previous_exit_from_for = exit_from_for;
            for (int i = time; i < exit_from_for; i++)
            {
                if (i >= Range.Count())
                {
                    exit_from_for = exit_from_for - Range.Count() + 1;
                    i = 1;
                }
                Print_with_norm_distanse(i, after, Otdelenie);
            }// то есть отработали самый что ни на есть стандартный случай. Например, сейчас мы на 6 элементе, значит с него и начнём, а продолжать будем столько, сколько юзер захотел
             //exit_from_for = exit_from_for;//это нужно, чтобы не потерять позицию
            Console.WriteLine();
            currenttext = " ";
            Change_color_for_text("Нажмите стрелку вниз [↓], чтобы листать дальше" +
                    "\n [Escape] - чтобы выйти из режима просмотра" +
                    "\n [Enter] - чтобы перейти в режим редактирования", ConsoleColor.Green);
            Console.WriteLine(currenttext);
            if (exit_from_for >= Range.Count()) exit_from_for = 1;
            Height_of_Zagolovok = 0;
        }
        public void Cut_String(ref string something, int lenght)
        {
            something = something.Substring(0, lenght);
        }
        public void Add_Space(int before, int after, ref string something)
        {
            for (int i = 0; i < before; i++)
            {
                something = something.Insert(0, " ");
            }
            for (int i = 0; i < after; i++)
            {
                something += " ";
            }
        }
        public void Add_Space_To_Zagolovki(int before, int after, List<string> something)
        {
            string StupidRules = " ";
            for (int i = 0; i < something.Count(); i++)
            {
                StupidRules = something[i];
                Add_Space(before, after, ref StupidRules);
                something[i] = StupidRules; // для меня величайшая загадка, почему нельзя было туда сразу передать ref something[i]. Оно выдавало ошибку...Пришлось костыль использовать.
            }
        }
        ~Menu()
        {
            Clear_Options(); // очистка памяти
        }
    }// класс, необходимый для того, чтобы сделать красивую менюшку для пунктов и не только
    //Надо, наверное, объяснить, зачем я делаю класс Меню достаточно универсальным. Дело в том, что этот класс можно будет без проблем, без тоскания за собой сотни других классов применить в другом проекте. Кроме, того я везде стараюсь использовать по-максимуму SOLID и GRASP. У меня каждый класс решает конкретную задачу, используется принцип "Информационный эксперт" и "Слабая связность". Это моих два любимых друга на данный момент.
    class Spisok_Del
    {
        private List<Delo> Dela = new List<Delo>();
        public int CurPos = 0;
        private Menu JustForBeauty;
        public Spisok_Del()
        {
            try
            {
                JustForBeauty = new Menu();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Нам очень жаль, однако для работы с данным классом нужен класс Delo и класс Menu из пространства имён Scheduler");
                Console.WriteLine("Код ошибки: " + e);
                Task.Delay(5000).GetAwaiter().GetResult();
                Environment.Exit(0);
            }
            JustForBeauty.Add_Zagolovok("Имя задачи");
            JustForBeauty.Add_Zagolovok("Сложность");
            JustForBeauty.Add_Zagolovok("Актуальность");
            JustForBeauty.Add_Zagolovok("Время");
            JustForBeauty.Add_Zagolovok("Дата");
            JustForBeauty.Save_to_Range();//таким образом у нас изначально будут заголовки
            JustForBeauty.Cut_Zagolovki(Console.WindowWidth / 5);//подбираем ширину под размер окна
            JustForBeauty.Clear_Zagolovok();//чтобы можно было добавлять новые
        }
        public void AddDelo(Delo something, ref DataBase somebase)
        {
            Dela.Add(something);
            JustForBeauty.Clear_Zagolovok();//а мало ли было тут что-то
            JustForBeauty.Add_Zagolovok(something.name);
            JustForBeauty.Add_Zagolovok(something.difficult.ToString());
            JustForBeauty.Add_Zagolovok(something.is_actual.ToString());
            JustForBeauty.Add_Zagolovok(something.hour.ToString() + ":" + something.minutes.ToString() + ":" + something.sec.ToString());
            JustForBeauty.Add_Zagolovok(something.day.ToString() + "." + something.month.ToString() + "." + something.year.ToString());
            JustForBeauty.Save_to_Range();
            JustForBeauty.Clear_Zagolovok();
            somebase.INSERT(something.name, something.difficult.ToString(),something.year.ToString(),something.month.ToString(),something.day.ToString(),something.hour.ToString(),something.minutes.ToString(),something.sec.ToString(),something.is_actual);//таким образом происходит добавление элемента в базу данных
        }
        public void DeleteCurDelo()
        {
            Dela.RemoveAt(CurPos);
            Change_Range();
        }//удаляет текущее дело из списка
        public void DeleteAll()
        {
            Dela.Clear();
        }// удаляет все элементы
        public void Show_Spisok(ref DataBase somebase)
        {
            JustForBeauty.Clear_Options();
            JustForBeauty.Add_Option("Просмотреть весь список целиком");
            JustForBeauty.Add_Option("Смотреть список частями (по нескольку элементов)");
            JustForBeauty.Show();
            JustForBeauty.Clear_Options();
            if (JustForBeauty.curpos == 0)
            {
                JustForBeauty.curpos = 0;
                JustForBeauty.Pechat_Punktov(JustForBeauty.Range.Count() - 1, 5, '|');
            }
            else
            {
                JustForBeauty.Set_center_for_text("Укажите, пожалуйста, количество, которое будет выводится за раз." +
                    "\nУчитывайте, что сейчас всего " + JustForBeauty.Range.Count() + " элементов");
                Console.WriteLine(JustForBeauty.currenttext);
                Console.WriteLine("Осуществляется ввод в с клавиатуры");
                int how_much = 1;
                try
                {
                    how_much = Int32.Parse(Console.ReadLine());
                    JustForBeauty.Pechat_Punktov(how_much - 1, 5, '|');
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ой, что-то пошло не так, но не волнуйтесь: вы будете возвращены назад через 5 секунд.\nПосле чего сможете попробовать вновь");
                    Console.WriteLine("Код ошибки: " + e);
                    Task.Delay(2000).GetAwaiter().GetResult();
                    how_much = 1;
                    return;
                }
            }
            if (JustForBeauty.menu == 'E')
            {
                CurPos = JustForBeauty.curpos - 1;
                string save = Dela[CurPos].name;
                Dela[CurPos].Add_Zagolovok_for_change_info("Изменить сложность");
                Dela[CurPos].Add_Zagolovok_for_change_info("Изменить дату");
                Dela[CurPos].Add_Zagolovok_for_change_info("Изменить время");
                Dela[CurPos].Add_Zagolovok_for_change_info("Изменить имя");
                Dela[CurPos].Add_Zagolovok_for_change_info("Изменить статус актуальности");
                Dela[CurPos].Add_Zagolovok_for_change_info("Удалить дело");
                Dela[CurPos].Add_Zagolovok_for_change_info("Закончить редактирование");
                Dela[CurPos].Add_operation_for_change_info(0, Dela[CurPos].Change_Difficult);
                Dela[CurPos].Add_operation_for_change_info(1, Dela[CurPos].Change_Date);
                Dela[CurPos].Add_operation_for_change_info(2, Dela[CurPos].Change_Time);
                Dela[CurPos].Add_operation_for_change_info(3, Dela[CurPos].Change_name);
                Dela[CurPos].Add_operation_for_change_info(4, Dela[CurPos].Change_Actual);
                Dela[CurPos].Add_operation_for_change_info(5, DeleteCurDelo);
                Dela[CurPos].Add_operation_for_change_info(6, Change_Range);
                Dela[CurPos].Change_Info();
                Change_Range();
                somebase.UPDATE("name="+Dela[CurPos].name+", difficult="+Dela[CurPos].difficult.ToString()+", year="+Dela[CurPos].year.ToString(), "name="+save);
            }
        }
        public void Change_Range()
        {
            JustForBeauty.Clear_Range();
            JustForBeauty.Add_Zagolovok("Имя задачи");
            JustForBeauty.Add_Zagolovok("Сложность");
            JustForBeauty.Add_Zagolovok("Актуальность");
            JustForBeauty.Add_Zagolovok("Время");
            JustForBeauty.Add_Zagolovok("Дата");
            JustForBeauty.Save_to_Range();//таким образом у нас изначально будут заголовки
            JustForBeauty.Cut_Zagolovki(Console.WindowWidth / 5);//подбираем ширину под размер окна
            JustForBeauty.Clear_Zagolovok();//чтобы можно было добавлять новые
            foreach (Delo something in Dela)
            {
                JustForBeauty.Clear_Zagolovok();//а мало ли было тут что-то
                JustForBeauty.Add_Zagolovok(something.name);
                JustForBeauty.Add_Zagolovok(something.difficult.ToString());
                JustForBeauty.Add_Zagolovok(something.is_actual.ToString());
                JustForBeauty.Add_Zagolovok(something.hour.ToString() + ":" + something.minutes.ToString() + ":" + something.sec.ToString());
                JustForBeauty.Add_Zagolovok(something.day.ToString() + "." + something.month.ToString() + "." + something.year.ToString());
                JustForBeauty.Save_to_Range();
                JustForBeauty.Clear_Zagolovok();
            }//я и сам осознаю, насколько это костыль переписывать всё по новой и какую это даёт нагрузку. Куда лучше было бы переписать один нужный элемент, однако сходу я не догадался как да и времени уже нету, я слишком долго сижу над этой программой
        }
    }
    class Delo
    {
        public int difficult { get; private set; }
        public int year { get; private set; }
        public int month { get; private set; }
        public int day { get; private set; }
        public int hour { get; private set; }
        public int minutes { get; private set; }
        public int sec { get; private set; }
        public bool is_actual { get; private set; }
        public string name { get; private set; }
        public delegate void Add_some_function(); // так как при проектировании, то есть ещё в самом начале было принято, судя по всему, не самое хорошее решение касательно того, что какой класс делает, то пришлось создавать делегат, в который передаётся метод удаления элемента из списка
        public delegate void Operations();
        public Dictionary<int, Operations> _operations_for_set_info { get; private set; }
        public Dictionary<int, Operations> _operations_for_change_info { get; private set; }
        public List<string> Zagolovok_for_set_info { get; private set; }
        public List<string> Zagolovok_for_change_info { get; private set; } // на самом деле не столь обязательно было создавать это отдельно прям, но мне кажется, что так будет правильнее
        private const int Count_of_Monthes = 12;
        private const int Count_of_Days = 31;
        private const int Count_hour = 24;
        private const int Count_minutes = 60;
        private const int Count_sec = 60;
        private const int Count_difficult = 5;
        static public int count_of_dela { get; private set; }
        public Delo(string name = "Новая задача")
        {
            this.name = name; // можно было бы в параметрах указать _name и сделать просто name=_name, но я не хочу таким заниматься, на мой взгляд, это уже был бы какой-то костыль
            is_actual = true; // новая задача априори не может быть сразу же просроченной
            count_of_dela++;
            difficult = 1;
            _operations_for_set_info = new Dictionary<int, Operations>();
            _operations_for_change_info = new Dictionary<int, Operations>();
            Zagolovok_for_set_info = new List<string>();
            Zagolovok_for_change_info = new List<string>();
        }
        ~Delo()
        {
            count_of_dela--;
        }
        public void Change_Difficult()
        {
            Console.Clear();
            List<string> Options = new List<string>();
            AddInCycle(ref Options, Count_difficult);
            Menu JustForBeuty = new Menu(ref Options);
            JustForBeuty.gotoxy(0, Console.WindowHeight / 2);
            JustForBeuty.Set_center_for_text("Введите новую сложность: ");
            Console.WriteLine(JustForBeuty.currenttext);
            JustForBeuty.Show();
            difficult = JustForBeuty.Choice() + 1;//нумерация же с нуля начинается, потому можно просто по индексу отстледить, прибавив единицу :)
        }
        public void Change_Date()
        {
            Console.Clear();
            List<string> Options = new List<string>();
            AddInCycle(ref Options, Count_of_Monthes);
            Menu JustForBeuty = new Menu(ref Options);
            JustForBeuty.gotoxy(0, Console.WindowHeight / 2);
            JustForBeuty.Set_center_for_text("Введите новую дату\nДля начала введите год с клавиатуры ");
            Console.WriteLine(JustForBeuty.currenttext);
            try
            {
                year = Convert.ToInt32(Console.ReadLine());
                if (year < 0)
                {
                    JustForBeuty.Set_center_for_text("Год не может быть отрицательным. Давайте я запишу текущий год");
                    Console.WriteLine(JustForBeuty.currenttext);
                    year = DateTime.Now.Year; // записываем текущий год вместо ерунды
                }
            }
            catch
            {
                JustForBeuty.Set_center_for_text("Что-то пошло не так, был зафиксирован неадекватный ввод\nСтрашного в этом мало: Вы сможете отредактировать данный параметр позднее\nА на данный момент будет вписан текущий год");
                Console.WriteLine(JustForBeuty.currenttext);
                year = DateTime.Now.Year; // записываем текущий год вместо мусора
            }
            JustForBeuty.Set_center_for_text("Теперь месяц");
            Console.WriteLine(JustForBeuty.currenttext);
            Task.Delay(5000).GetAwaiter().GetResult();//делаем задержку по времени, чтобы пользователь успел понять, что ему нужно вводить
            JustForBeuty.Show();
            month = JustForBeuty.Choice() + 1;
            JustForBeuty.Set_center_for_text("Теперь день");
            Console.WriteLine(JustForBeuty.currenttext);
            Task.Delay(5000).GetAwaiter().GetResult();//делаем задержку по времени, чтобы пользователь успел понять, что ему нужно вводить
            Options.Clear();
            JustForBeuty.Clear_Options();
            AddInCycle(ref Options, Count_of_Days);
            int j = 0;
            do
            {
                j++;
                if (j > 1) JustForBeuty.Set_center_for_text("Вы выбрали некорректное значение.\nУчитывайте, что в году 12 месяцев, из них 30 дней в 4 месяцах: сентябрь, апрель, июнь, ноябрь" +
                      "\n28 или 29 дней в феврале (в зависимости от того, высокостный ли год)" +
                      "\nВ остальных 7 месяцах 31 день");
                Console.WriteLine(JustForBeuty.currenttext);
                JustForBeuty.Show();
                day = JustForBeuty.Choice() + 1;
            }
            while (((month == 2) && (day > 29)) || ((month == 4) && (day > 30)) || ((month == 6) && (day > 30)) || ((month == 9) && (day > 30)) || ((month == 11) && (day > 30)) || (day > 32));
            JustForBeuty.Set_center_for_text("Дата была изменена");
            Console.WriteLine(JustForBeuty.currenttext);
            Task.Delay(5000).GetAwaiter().GetResult();//делаем задержку по времени, чтобы пользователь успел понять, что ему нужно вводить
            JustForBeuty.Clear_Options();
            Options.Clear();
        }
        public void Change_Time()
        {
            Console.Clear();
            List<string> Options = new List<string>();
            AddInCycle(ref Options, Count_hour);
            Menu JustForBeuty = new Menu(ref Options);
            JustForBeuty.gotoxy(0, Console.WindowHeight / 2);
            JustForBeuty.Set_center_for_text("Укажите часы");
            Console.WriteLine(JustForBeuty.currenttext);
            Task.Delay(5000).GetAwaiter().GetResult();//делаем задержку по времени, чтобы пользователь успел понять, что ему нужно вводить
            JustForBeuty.Show();
            hour = JustForBeuty.Choice() + 1;
            JustForBeuty.Set_center_for_text("Теперь минуты");
            Console.WriteLine(JustForBeuty.currenttext);
            Task.Delay(5000).GetAwaiter().GetResult();//делаем задержку по времени, чтобы пользователь успел понять, что ему нужно вводить
            Options.Clear();
            JustForBeuty.Clear_Options();
            AddInCycle(ref Options, Count_minutes);
            minutes = JustForBeuty.Choice() + 1;
            JustForBeuty.Set_center_for_text("Теперь секунды");
            Console.WriteLine(JustForBeuty.currenttext);
            Task.Delay(5000).GetAwaiter().GetResult();//делаем задержку по времени, чтобы пользователь успел понять, что ему нужно вводить
            Options.Clear();
            AddInCycle(ref Options, Count_sec);
            JustForBeuty.Show();
            sec = JustForBeuty.Choice() + 1;
            JustForBeuty.Clear_Options();
            Options.Clear();
        }
        public void Change_Actual()
        {
            Console.Clear();
            List<string> Options = new List<string>();
            Options.Add("Завершено");
            Options.Add("Актуально");
            Menu JustForBeuty = new Menu(ref Options);
            JustForBeuty.Show();
            is_actual = Convert.ToBoolean(JustForBeuty.Choice());
        }
        public void Change_name()
        {
            Console.Clear();
            List<string> Options = new List<string>();
            AddInCycle(ref Options, Count_of_Monthes);
            Menu JustForBeuty = new Menu(ref Options);
            JustForBeuty.gotoxy(0, Console.WindowHeight / 2);
            JustForBeuty.Set_center_for_text("Введите новое имя с клавиатуры");
            Console.WriteLine(JustForBeuty.currenttext);
            string temp = name;
            try
            {
                name = Console.ReadLine();
            }
            catch
            {
                JustForBeuty.Set_center_for_text("Что-то пошло не так. Поменять имя не удалось. Попробуйте снова");
                Console.WriteLine(JustForBeuty.currenttext);
                name = temp;//Оставили прежнее имя задачи
            }
        }
        private void AddInCycle(ref List<string> Options, int Case)
        {
            for (int i = 1; i < Case; i++)
            {
                Options.Add("" + i);
            }
        }
        private void What_is_changing(object menu)
        {
            Menu menu_for_change = (Menu)menu;//дааа, вот это костыли си шарп плодит. В С++ с многопотоком намного проще. Наверное, усложнили, чтобы такие как я меньше с этим игрались
            menu_for_change.gotoxy(0, 0);
            menu_for_change.Set_center_for_text("Редактирование дела: " + name +
                "\n" + name + "\t" + difficult.ToString() + "\t" + day.ToString() + "." + month.ToString() + "." + year.ToString() + "\t" + hour.ToString() + ":" + minutes.ToString() + ":" + sec.ToString() + "\t" + is_actual.ToString());
            menu_for_change.Change_color_for_text(menu_for_change.currenttext, ConsoleColor.Yellow);
        }
        public void Change_Info()
        {
            List<string> textovochka = new List<string>();
            Menu menu_for_change = new Menu(ref textovochka);
            Console.Clear(); // очищаем консоль от всего, что было до этого
            menu_for_change.Set_center_for_text("Редактирование дела: " + name +
                "\n" + name + "\t" + difficult.ToString() + "\t" + day.ToString() + "." + month.ToString() + "." + year.ToString() + "\t" + hour.ToString() + ":" + minutes.ToString() + ":" + sec.ToString() + "\t" + is_actual.ToString());
            foreach (string text_to_add in Zagolovok_for_change_info)
                textovochka.Add(text_to_add);
            while (true)
            {
                Console.WriteLine(menu_for_change.currenttext);//да уж, не удобно как-то получилось, что метод Show() делает Console.Clear(), но там это реально нужно. А добавлять в метод Show() вот эту логику с отображением дела...связность сильная будет
                Thread thr1 = new Thread(new ParameterizedThreadStart(What_is_changing)); //да здравствует многопоток
                thr1.Start(menu_for_change);
                menu_for_change.Show();
                thr1.Join();//чего-то я в этой жизни не понимаю, оно должно было тут завершать поток и в след. итерации снова включать, а оно как-то костыльно работает)
                if (!_operations_for_change_info.ContainsKey(menu_for_change.Choice() + 1))
                {
                    return;
                }
                Do_operation_for_change(menu_for_change.Choice());//switch больше не нужен :)
            }
        }
        public void Set_Info()
        {
            List<string> textovochka = new List<string>();
            Menu menu_for_change = new Menu(ref textovochka);
            Console.Clear(); // очищаем консоль от всего, что было до этого
            menu_for_change.Set_center_for_text("Создание дела: " + name +
                "\n" + name + "\t" + difficult.ToString() + "\t" + day.ToString() + "." + month.ToString() + "." + year.ToString() + "\t" + hour.ToString() + ":" + minutes.ToString() + ":" + sec.ToString() + "\t" + is_actual.ToString());
            foreach (string text_to_add in Zagolovok_for_set_info)
                textovochka.Add(text_to_add);
            while (true)
            {
                Console.WriteLine(menu_for_change.currenttext);//да уж, не удобно как-то получилось, что метод Show() делает Console.Clear(), но там это реально нужно. А добавлять в метод Show() вот эту логику с отображением дела...связность сильная будет
                Thread thr1 = new Thread(new ParameterizedThreadStart(What_is_changing)); //да здравствует многопоток
                thr1.Start(menu_for_change);
                menu_for_change.Show();
                thr1.Join();//чего-то я в этой жизни не понимаю, оно должно было тут завершать поток и в след. итерации снова включать, а оно как-то костыльно работает)
                if (!_operations_for_set_info.ContainsKey(menu_for_change.Choice() + 1))
                {
                    return;
                }
                Do_operation_for_set(menu_for_change.Choice());//switch больше не нужен :)
            }
        }
        public void Add_operation_for_set_info(int number, Operations something)
        {
            if (_operations_for_set_info.ContainsKey(number))
                throw new ArgumentException(string.Format("Операция {0} уже есть", number));
            _operations_for_set_info.Add(number, something); //таким образом мы можем добавлять бесконечно много метОдов. Это намного лучше, чем switch
        }
        public void Do_operation_for_set(int number)
        {
            try
            {
                if (!_operations_for_set_info.ContainsKey(number))
                    throw new ArgumentException(string.Format("Операции под номером {0} нету", number));
            }
            catch
            {
                Console.WriteLine("Что-то пошло не так");
            }
            _operations_for_set_info[number]();
        }
        public void Add_operation_for_change_info(int number, Operations something)
        {
            if (_operations_for_change_info.ContainsKey(number))
                throw new ArgumentException(string.Format("Операция {0} уже есть", number));
            _operations_for_change_info.Add(number, something); //таким образом мы можем добавлять бесконечно много метОдов. Это намного лучше, чем switch
        }
        public void Do_operation_for_change(int number)
        {
            try
            {
                if (!_operations_for_change_info.ContainsKey(number))
                    throw new ArgumentException(string.Format("Операции под номером {0} нету", number));
            }
            catch
            {
                Console.WriteLine("Что-то пошло не так");
            }
            _operations_for_change_info[number]();
        }
        public void Add_Zagolovok_for_set_info(string something)
        {
            Zagolovok_for_set_info.Add(something);
        }
        public void Del_All_Zagolovok_for_set_info()
        {
            Zagolovok_for_set_info.Clear();
        }
        public void Add_Zagolovok_for_change_info(string something)
        {
            Zagolovok_for_change_info.Add(something);
        }
        public void Del_All_Zagolovok_for_change_info()
        {
            Zagolovok_for_set_info.Clear();
        }
    }
    class DataBase
    {
        private string connec_str = "";
        private Dictionary<string, string> var_for_connect;//дело в том, что строка подключения может содержать ряд дополнительных параметров, такие как convert zero datetime=True и т.д., поэтому рационально будет сделать словарь параметров <параметр, значение>, чтобы данный класс можно было использовать для самых различных целей
        private List<string> tables;//таблиц в базе данных тоже может быть сколь угодно много на самом деле, потому пусть будет список таблиц, хотя в рамках данного проекта это достаточно излишне, но я считаю, надо делать код достаточно универсальным, чтобы не делать одни и те же вещи +100500 раз. Вот сейчас сел, сделал на совесть, и в случае чего, если понадобиться вновь работать с бд, я просто возьму и буду использовать этот класс. Может, добавлю пару вещей, но сделаю это без перекомпеляции класса. Потому я трачу время не зря
        private string curr_table;
        MySqlConnection conn;
        public DataBase(string name_of_server = "localhost", string name_of_user = "root", string name_of_database = "scheduler", string port = "3306", string password = "Тетриандох228", bool i_need_these_parametrs_to_use = true)
        {
            var_for_connect = new Dictionary<string, string>(); // выделили память под словарь
            tables = new List<string>();// выделили память под имена таблиц
            if (i_need_these_parametrs_to_use)
            {
                Add_parametr("server", name_of_server);
                Add_parametr("user", name_of_user);
                Add_parametr("database", name_of_database);
                Add_parametr("port", port);
                Add_parametr("password", password);
            }
        }//на этом роль конструктора окончена. Его сущность была лишь в выделении памяти и вбивке ключевых параметров при желании
        ~DataBase()
        {
            conn.Close();
        }
        public void Open_connection()
        {
            conn = new MySqlConnection(connec_str);
            conn.Open();
        }//хотелось бы, конечно в конструктор это, но увы, не получится. Я хочу, чтобы пользователь сначала настроил все параметры подключения
        public void Add_parametr(string parametr, string value_of_parametr)
        {
            try
            {
                if (var_for_connect.ContainsKey(parametr))
                    throw new ArgumentException(string.Format("Параметр {0} уже существует", parametr));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
            var_for_connect.Add(parametr, value_of_parametr);//добавляем параметр
        }
        public void Make_connection_string()
        {
            foreach (KeyValuePair<string, string> something in var_for_connect)
            {
                connec_str = connec_str + something.Key + "=" + something.Value + ";";
            }
        }
        public void Add_table(string new_table)
        {
            if (tables.Contains(new_table))
            {
                Console.WriteLine("Таблица с таким же именем уже есть, придумайте что-то новое");
                return;
            }
            tables.Add(new_table);
        }
        public void Chose_table()
        {
            foreach (var something in tables.Select((value, i) => new { i, value }))
            {
                Console.WriteLine("{0}.{1}", something.i, something.value);
            }
            Console.WriteLine("Выбирете вариант, который необходим (нужно ввести нужное число, затем нажать Enter)");
            try
            {
                int curr_table;
                curr_table = Int32.Parse(Console.ReadLine());
                if (curr_table > tables.Count() || curr_table < 0) throw new ArgumentException(string.Format("Таблицы под таким номером нет"));
                this.curr_table = tables[curr_table];//да, намудрил тут с названием, но да ладно
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Что-то пошло не так");
                return;
            }
        }
        public virtual void INSERT(string name, string difficult, string year, string month, string day, string hour, string min, string sec, bool actual = true)
        {
            int is_actual = 0;
            if (actual) is_actual = 1;
            else is_actual = 0;
            string query = "INSERT INTO " + curr_table + "(`name`, `difficult`, `year`, `month`,`day`,`hour`,`min`,`sec`,`actual`) VALUES ('" + name + "','" + difficult + "','" + year + "','" + month + "','" + day + "','" + hour + "','" + min + "','" + sec + "','" + is_actual.ToString() + "')";
            MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                MySqlDataReader myReader = command.ExecuteReader();
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }//на самом деле данный метод тоже можно было бы сделать достаточно универсальным, однако тут много тонкостей, пусть останется таким, как есть. Единственное, что я сходу придумал это сделать данный метод виртуальным, чтобы человек мог в будущем, возможно, даже в другой программе унаследовать свой класс от данного, а метод INSERT переопределить
        public virtual string SELECT(string what_should_be_selected, string where_what)
        {
            string query = "SELECT " + what_should_be_selected + " FROM " + curr_table + " WHERE " + where_what;
            MySqlCommand command = new MySqlCommand(query, conn);//выполняем соответствующий запрос
            try
            {
                return command.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Что-то пошло не так :(";
            }
        }//Данный метод возвращает строку, которую нам возвращает бд
        public void DELETE(string where_what)
        {
            string query = "DELETE FROM "+curr_table+" WHERE "+where_what;
            MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void UPDATE(string what_should_be_set, string where_set)
        {
            string query = "UPDATE " + curr_table + " SET " + what_should_be_set+" WHERE "+where_set;
            MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void CREATE_DATA_BASE(string name_of_table)
        {
            //эх, учиться и ещё раз учить мне гуглить. Надеюсь, на курсах научите)
            string sql = "CREATE TABLE `"+name_of_table+"` (`name` varchar(100) DEFAULT NULL,`difficult` int(11) DEFAULT NULL,`year` int(11) DEFAULT NULL,`month` int(11) DEFAULT NULL,`day` int(11) DEFAULT NULL,`hour` int(11) DEFAULT NULL,`min` int(11) DEFAULT NULL, `sec` int(11) DEFAULT NULL,`actual` tinyint(4) DEFAULT NULL ) ENGINE = InnoDB DEFAULT CHARSET = utf8;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
    }
}