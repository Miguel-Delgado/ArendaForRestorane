using System;
using System.Collections.Generic;
using System.Linq;

class RestaurantApp
{
    static string adminPassword = "admin123";
    static string userType;
    static List<Table> tables = new List<Table>();
    static Dictionary<int, Booking> bookings = new Dictionary<int, Booking>();
    static List<Dish> menu = new List<Dish>();
    static int guestIdCounter = 1;
    static int tableIdCounter = 17;

    class Dish
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Dish(string category, string name, decimal price)
        {
            Category = category;
            Name = name;
            Price = price;
        }
    }

    class Booking
    {
        public int TableId { get; set; }
        public string ClientName { get; set; }
        public int GuestId { get; set; }
        public string BookingTime { get; set; }
        public string BookingDate { get; set; }

        public Booking(int tableId, string clientName, int guestId, string bookingTime, string bookingDate)
        {
            TableId = tableId;
            ClientName = clientName;
            GuestId = guestId;
            BookingTime = bookingTime;
            BookingDate = bookingDate;
        }
    }

    class Table
    {
        public int TableId { get; set; }
        public string Location { get; set; }
        public int Seats { get; set; }
        public Dictionary<string, string[]> Schedule { get; set; }

        public Table(int tableId, string location, int seats)
        {
            TableId = tableId;
            Location = location;
            Seats = seats;
            Schedule = new Dictionary<string, string[]>();
        }
    }

    static void Main()
    {
        InitializeTables();
        InitializeMenu();
        Console.WriteLine("Добро пожаловать в ресторан!");
        MainMenu();
    }

    static void InitializeTables()
    {
        // Все столы теперь по 4 места
        tables.Add(new Table(1, "Зал", 4));
        tables.Add(new Table(2, "Зал", 4));
        tables.Add(new Table(3, "Зал", 4));
        tables.Add(new Table(4, "Веранда", 4));
        tables.Add(new Table(5, "Зал", 4));
        tables.Add(new Table(6, "Зал", 4));
        tables.Add(new Table(7, "Зал", 4));
        tables.Add(new Table(8, "Веранда", 4));
        tables.Add(new Table(9, "Зал", 4));
        tables.Add(new Table(10, "Зал", 4));
        tables.Add(new Table(11, "Зал", 4));
        tables.Add(new Table(12, "У входа", 4));
        tables.Add(new Table(13, "У окна", 4));
        tables.Add(new Table(14, "У окна", 4));
        tables.Add(new Table(15, "У окна", 4));
        tables.Add(new Table(16, "У входа", 4));
    }

    static void InitializeMenu()
    {
        // Начальное меню
        menu.Add(new Dish("Закуски", "Брускетта с томатами", 350));
        menu.Add(new Dish("Закуски", "Тартар из говядины", 450));
        menu.Add(new Dish("Основные блюда", "Стейк Рибай", 1200));
        menu.Add(new Dish("Основные блюда", "Паста Карбонара", 600));
        menu.Add(new Dish("Основные блюда", "Лосось на гриле", 850));
        menu.Add(new Dish("Десерты", "Тирамису", 400));
        menu.Add(new Dish("Десерты", "Чизкейк", 350));
        menu.Add(new Dish("Напитки", "Мохито", 300));
        menu.Add(new Dish("Напитки", "Капучино", 200));
        menu.Add(new Dish("Напитки", "Сок апельсиновый", 150));
    }

    static void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\nВведите тип пользователя (администратор/гость):");
            userType = Console.ReadLine()?.ToLower();

            if (userType == "администратор")
            {
                AdminLogin();
                break;
            }
            else if (userType == "гость")
            {
                GuestMenu();
                break;
            }
            else
            {
                Console.WriteLine("Неверный тип пользователя.");
            }
        }
    }

    static void AdminLogin()
    {
        Console.WriteLine("Введите пароль администратора:");
        string password = Console.ReadLine();

        if (password == adminPassword)
        {
            Console.WriteLine("Добро пожаловать, администратор!");
            AdminMainMenu();
        }
        else
        {
            Console.WriteLine("Неверный пароль.");
            MainMenu();
        }
    }

    static void AdminMainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== ПАНЕЛЬ АДМИНИСТРАТОРА ===");
            Console.WriteLine("1. МЕНЮ");
            Console.WriteLine("2. СТОЛЫ");
            Console.WriteLine("3. БРОНИ");
            Console.WriteLine("4. Выход");

            int choice = GetValidInt("Ваш выбор: ");

            switch (choice)
            {
                case 1:
                    MenuManagementAdmin();
                    break;
                case 2:
                    TablesManagement();
                    break;
                case 3:
                    BookingManagement();
                    break;
                case 4:
                    Console.WriteLine("Выход из системы администратора.");
                    MainMenu();
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    // === УПРАВЛЕНИЕ МЕНЮ ДЛЯ АДМИНИСТРАТОРА ===
    static void MenuManagementAdmin()
    {
        while (true)
        {
            Console.WriteLine("\n=== УПРАВЛЕНИЕ МЕНЮ ===");
            Console.WriteLine("1. Просмотр меню");
            Console.WriteLine("2. Добавить блюдо");
            Console.WriteLine("3. Удалить блюдо");
            Console.WriteLine("4. Редактировать блюдо");
            Console.WriteLine("5. Назад");

            int choice = GetValidInt("Ваш выбор: ");

            switch (choice)
            {
                case 1:
                    ShowMenuAdmin();
                    break;
                case 2:
                    AddDish();
                    break;
                case 3:
                    RemoveDish();
                    break;
                case 4:
                    EditDish();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    static void ShowMenuAdmin()
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("ТЕКУЩЕЕ МЕНЮ РЕСТОРАНА");
        Console.WriteLine(new string('=', 50));

        var categories = menu.Select(d => d.Category).Distinct();

        foreach (var category in categories)
        {
            Console.WriteLine($"\n{category.ToUpper()}:");
            Console.WriteLine(new string('-', 30));

            var dishesInCategory = menu.Where(d => d.Category == category);
            int counter = 1;
            foreach (var dish in dishesInCategory)
            {
                Console.WriteLine($"{counter}. {dish.Name} - {dish.Price} руб.");
                counter++;
            }
        }
        Console.WriteLine(new string('=', 50));
    }

    static void AddDish()
    {
        Console.WriteLine("\n=== ДОБАВЛЕНИЕ БЛЮДА ===");

        Console.WriteLine("Введите категорию блюда:");
        Console.WriteLine("(например: Закуски, Основные блюда, Десерты, Напитки)");
        string category = Console.ReadLine();

        Console.WriteLine("Введите название блюда:");
        string name = Console.ReadLine();

        Console.WriteLine("Введите цену блюда:");
        decimal price = GetValidDecimal("Цена: ");

        menu.Add(new Dish(category, name, price));
        Console.WriteLine($"Блюдо '{name}' успешно добавлено в меню!");
    }

    static void RemoveDish()
    {
        Console.WriteLine("\n=== УДАЛЕНИЕ БЛЮДА ===");
        ShowMenuAdmin();

        Console.WriteLine("\nВведите название блюда для удаления:");
        string dishName = Console.ReadLine();

        var dishToRemove = menu.FirstOrDefault(d =>
            d.Name.Equals(dishName, StringComparison.OrdinalIgnoreCase));

        if (dishToRemove != null)
        {
            menu.Remove(dishToRemove);
            Console.WriteLine($"Блюдо '{dishName}' успешно удалено!");
        }
        else
        {
            Console.WriteLine("Блюдо с таким названием не найдено.");
        }
    }

    static void EditDish()
    {
        Console.WriteLine("\n=== РЕДАКТИРОВАНИЕ БЛЮДА ===");
        ShowMenuAdmin();

        Console.WriteLine("\nВведите название блюда для редактирования:");
        string dishName = Console.ReadLine();

        var dishToEdit = menu.FirstOrDefault(d =>
            d.Name.Equals(dishName, StringComparison.OrdinalIgnoreCase));

        if (dishToEdit == null)
        {
            Console.WriteLine("Блюдо с таким названием не найдено.");
            return;
        }

        Console.WriteLine($"\nРедактирование блюда: {dishToEdit.Name}");
        Console.WriteLine($"Текущая категория: {dishToEdit.Category}");
        Console.WriteLine($"Текущая цена: {dishToEdit.Price} руб.");

        Console.WriteLine("\nВведите новую категорию (или Enter чтобы оставить текущую):");
        string newCategory = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newCategory))
        {
            dishToEdit.Category = newCategory;
        }

        Console.WriteLine("Введите новое название (или Enter чтобы оставить текущее):");
        string newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName))
        {
            dishToEdit.Name = newName;
        }

        Console.WriteLine("Введите новую цену (или 0 чтобы оставить текущую):");
        decimal newPrice = GetValidDecimal("Цена: ");
        if (newPrice > 0)
        {
            dishToEdit.Price = newPrice;
        }

        Console.WriteLine("Блюдо успешно отредактировано!");
    }

    // === УПРАВЛЕНИЕ СТОЛАМИ ===
    static void TablesManagement()
    {
        while (true)
        {
            Console.WriteLine("\n=== УПРАВЛЕНИЕ СТОЛАМИ ===");
            Console.WriteLine("1. Добавить стол");
            Console.WriteLine("2. Удалить стол по ID");
            Console.WriteLine("3. Информация о столах");
            Console.WriteLine("4. Назад");

            int choice = GetValidInt("Ваш выбор: ");

            switch (choice)
            {
                case 1:
                    AddTable();
                    break;
                case 2:
                    DeleteTable();
                    break;
                case 3:
                    ShowTablesInfo();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    static void AddTable()
    {
        Console.WriteLine("\n=== ДОБАВЛЕНИЕ СТОЛА ===");

        Console.WriteLine("Введите количество мест:");
        int seats = GetValidInt("Количество мест: ");

        Console.WriteLine("Выберите расположение:");
        Console.WriteLine("1. Зал");
        Console.WriteLine("2. Веранда");
        Console.WriteLine("3. У окна");
        Console.WriteLine("4. У входа");

        int locationChoice = GetValidInt("Ваш выбор: ");
        string location = locationChoice switch
        {
            1 => "Зал",
            2 => "Веранда",
            3 => "У окна",
            4 => "У входа",
            _ => "Зал"
        };

        int newTableId = tableIdCounter++;
        tables.Add(new Table(newTableId, location, seats));

        Console.WriteLine($"Стол успешно добавлен! ID стола: {newTableId}");
        Console.WriteLine($"Расположение: {location}, Мест: {seats}");
    }

    static void DeleteTable()
    {
        Console.WriteLine("\n=== УДАЛЕНИЕ СТОЛА ===");

        ShowAllTables();
        int tableId = GetValidInt("Введите ID стола для удаления: ");

        var table = tables.FirstOrDefault(t => t.TableId == tableId);
        if (table == null)
        {
            Console.WriteLine("Стол с таким ID не найден.");
            return;
        }

        bool hasActiveBookings = bookings.Values.Any(b => b.TableId == tableId);
        if (hasActiveBookings)
        {
            Console.WriteLine("Нельзя удалить стол с активными бронированиями!");
            return;
        }

        tables.Remove(table);
        Console.WriteLine($"Стол {tableId} успешно удален.");
    }

    static void ShowTablesInfo()
    {
        while (true)
        {
            Console.WriteLine("\n=== ИНФОРМАЦИЯ О СТОЛАХ ===");
            ShowAllTables();

            Console.WriteLine("\n1. Выбрать стол для детальной информации");
            Console.WriteLine("2. Назад");

            int choice = GetValidInt("Ваш выбор: ");

            if (choice == 1)
            {
                int tableId = GetValidInt("Введите ID стола: ");
                ShowTableDetails(tableId);
            }
            else if (choice == 2)
            {
                return;
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
            }
        }
    }

    static void ShowAllTables()
    {
        Console.WriteLine("\nСписок всех столов:");
        Console.WriteLine("ID\tМест\tРасположение");
        Console.WriteLine("------------------------");
        foreach (var table in tables.OrderBy(t => t.TableId))
        {
            Console.WriteLine($"{table.TableId}\t{table.Seats}\t{table.Location}");
        }
    }

    static void ShowTableDetails(int tableId)
    {
        var table = tables.FirstOrDefault(t => t.TableId == tableId);
        if (table == null)
        {
            Console.WriteLine("Стол не найден.");
            return;
        }

        Console.WriteLine($"\n=== ДЕТАЛЬНАЯ ИНФОРМАЦИЯ О СТОЛЕ {tableId} ===");
        Console.WriteLine($"Расположение: {table.Location}");
        Console.WriteLine($"Количество мест: {table.Seats}");

        var tableBookings = bookings.Values.Where(b => b.TableId == tableId).ToList();
        if (tableBookings.Any())
        {
            Console.WriteLine("\nАктивные бронирования:");
            Console.WriteLine("Дата\t\tВремя\tКлиент");
            Console.WriteLine("--------------------------------");
            foreach (var booking in tableBookings)
            {
                Console.WriteLine($"{booking.BookingDate}\t{booking.BookingTime}\t{booking.ClientName}");
            }
        }
        else
        {
            Console.WriteLine("\nАктивных бронирований нет.");
        }
    }

    // === УПРАВЛЕНИЕ БРОНИРОВАНИЯМИ ===
    static void BookingManagement()
    {
        while (true)
        {
            Console.WriteLine("\n=== УПРАВЛЕНИЕ БРОНИРОВАНИЯМИ ===");
            Console.WriteLine("1. Изменить бронирование");
            Console.WriteLine("2. Добавить бронирование");
            Console.WriteLine("3. Удалить бронирование");
            Console.WriteLine("4. Список бронирований");
            Console.WriteLine("5. Назад");

            int choice = GetValidInt("Ваш выбор: ");

            switch (choice)
            {
                case 1:
                    ModifyBookingAdmin();
                    break;
                case 2:
                    AddBookingAdmin();
                    break;
                case 3:
                    DeleteBookingAdmin();
                    break;
                case 4:
                    ShowAllBookingsAdmin();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    static void ModifyBookingAdmin()
    {
        Console.WriteLine("\n=== ИЗМЕНЕНИЕ БРОНИРОВАНИЯ ===");

        if (bookings.Count == 0)
        {
            Console.WriteLine("Нет активных бронирований.");
            return;
        }

        ShowAllBookingsAdmin();
        int guestId = GetValidInt("Введите ID гостя для изменения бронирования: ");

        if (!bookings.ContainsKey(guestId))
        {
            Console.WriteLine("Бронирование с таким ID не найдено.");
            return;
        }

        var booking = bookings[guestId];
        Console.WriteLine($"\nТекущее бронирование:");
        Console.WriteLine($"Гость: {booking.ClientName}");
        Console.WriteLine($"Стол: {booking.TableId}");
        Console.WriteLine($"Дата: {booking.BookingDate}");
        Console.WriteLine($"Время: {booking.BookingTime}");

        // Освобождаем старое время
        var oldTable = tables.First(t => t.TableId == booking.TableId);
        if (oldTable.Schedule.ContainsKey(booking.BookingDate))
        {
            var schedule = oldTable.Schedule[booking.BookingDate];
            for (int i = 0; i < schedule.Length; i++)
            {
                if (schedule[i] == booking.BookingTime)
                {
                    schedule[i] = null;
                    break;
                }
            }
        }

        // Запрашиваем новые данные
        Console.WriteLine("\nВведите новые данные:");

        ShowAllTables();
        int newTableId = GetValidInt("Новый ID стола: ");
        if (!tables.Any(t => t.TableId == newTableId))
        {
            Console.WriteLine("Стол с таким ID не существует.");
            return;
        }

        Console.WriteLine("Введите дату бронирования (дд.мм.гг):");
        string newDate = Console.ReadLine();

        Console.WriteLine("Введите время бронирования (например, 19:00):");
        string newTime = Console.ReadLine();

        // Проверяем доступность нового времени
        var newTable = tables.First(t => t.TableId == newTableId);
        if (!newTable.Schedule.ContainsKey(newDate))
        {
            newTable.Schedule[newDate] = new string[10];
        }

        var newSchedule = newTable.Schedule[newDate];
        if (Array.Exists(newSchedule, time => time == newTime))
        {
            Console.WriteLine("Это время уже занято!");
            return;
        }

        // Занимаем новое время
        for (int i = 0; i < newSchedule.Length; i++)
        {
            if (newSchedule[i] == null)
            {
                newSchedule[i] = newTime;
                break;
            }
        }

        // Обновляем бронирование
        booking.TableId = newTableId;
        booking.BookingDate = newDate;
        booking.BookingTime = newTime;

        Console.WriteLine("Бронирование успешно изменено!");
    }

    static void AddBookingAdmin()
    {
        Console.WriteLine("\n=== ДОБАВЛЕНИЕ БРОНИРОВАНИЯ ===");

        ShowAllTables();
        int tableId = GetValidInt("Введите ID стола: ");

        if (!tables.Any(t => t.TableId == tableId))
        {
            Console.WriteLine("Стол с таким ID не существует.");
            return;
        }

        Console.WriteLine("Введите имя клиента:");
        string clientName = Console.ReadLine();

        Console.WriteLine("Введите дату бронирования (дд.мм.гг):");
        string date = Console.ReadLine();

        Console.WriteLine("Введите время бронирования (например, 19:00):");
        string time = Console.ReadLine();

        // Проверяем доступность времени
        var table = tables.First(t => t.TableId == tableId);
        if (!table.Schedule.ContainsKey(date))
        {
            table.Schedule[date] = new string[10];
        }

        var schedule = table.Schedule[date];
        if (Array.Exists(schedule, t => t == time))
        {
            Console.WriteLine("Это время уже занято!");
            return;
        }

        // Занимаем время
        for (int i = 0; i < schedule.Length; i++)
        {
            if (schedule[i] == null)
            {
                schedule[i] = time;
                break;
            }
        }

        // Создаем бронирование
        int guestId = guestIdCounter++;
        var newBooking = new Booking(tableId, clientName, guestId, time, date);
        bookings[guestId] = newBooking;

        Console.WriteLine($"Бронирование успешно добавлено! ID гостя: {guestId}");
    }

    static void DeleteBookingAdmin()
    {
        Console.WriteLine("\n=== УДАЛЕНИЕ БРОНИРОВАНИЯ ===");

        if (bookings.Count == 0)
        {
            Console.WriteLine("Нет активных бронирований.");
            return;
        }

        ShowAllBookingsAdmin();
        int guestId = GetValidInt("Введите ID гостя для удаления бронирования: ");

        if (!bookings.ContainsKey(guestId))
        {
            Console.WriteLine("Бронирование с таким ID не найдено.");
            return;
        }

        var booking = bookings[guestId];

        // Освобождаем время
        var table = tables.First(t => t.TableId == booking.TableId);
        if (table.Schedule.ContainsKey(booking.BookingDate))
        {
            var schedule = table.Schedule[booking.BookingDate];
            for (int i = 0; i < schedule.Length; i++)
            {
                if (schedule[i] == booking.BookingTime)
                {
                    schedule[i] = null;
                    break;
                }
            }
        }

        bookings.Remove(guestId);
        Console.WriteLine("Бронирование успешно удалено!");
    }

    static void ShowAllBookingsAdmin()
    {
        Console.WriteLine("\n=== СПИСОК ВСЕХ БРОНИРОВАНИЙ ===");

        if (bookings.Count == 0)
        {
            Console.WriteLine("Нет активных бронирований.");
            return;
        }

        Console.WriteLine("ID гостя\tСтол\tКлиент\t\tДата\t\tВремя");
        Console.WriteLine("------------------------------------------------------------");
        foreach (var booking in bookings.Values)
        {
            Console.WriteLine($"{booking.GuestId}\t\t{booking.TableId}\t{booking.ClientName}\t\t{booking.BookingDate}\t{booking.BookingTime}");
        }
    }

    // === МЕНЮ ГОСТЯ ===
    static void GuestMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== МЕНЮ ГОСТЯ ===");
            Console.WriteLine("1. Сделать заказ");
            Console.WriteLine("2. Забронировать стол");
            Console.WriteLine("3. Посмотреть меню");
            Console.WriteLine("4. Просмотр карты ресторана");
            Console.WriteLine("5. Выход");

            int choice = GetValidInt("Ваш выбор: ");

            switch (choice)
            {
                case 1:
                    MakeOrder();
                    break;
                case 2:
                    ReserveTableGuest();
                    break;
                case 3:
                    ShowMenuGuest();
                    break;
                case 4:
                    ShowRestaurantMapGuest();
                    break;
                case 5:
                    Console.WriteLine("Выход из системы.");
                    MainMenu();
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    static void MakeOrder()
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("  ДОБРО ПОЖАЛОВАТЬ В НАШЕ МЕНЮ! 🍽️");
        Console.WriteLine(new string('=', 50));

        var categories = menu.Select(d => d.Category).Distinct();

        foreach (var category in categories)
        {
            Console.WriteLine($"\n {category.ToUpper()}:");
            Console.WriteLine(new string('─', 40));

            var dishesInCategory = menu.Where(d => d.Category == category);
            foreach (var dish in dishesInCategory)
            {
                Console.WriteLine($"   {dish.Name}");
                Console.WriteLine($"     Цена: {dish.Price} руб.");
                Console.WriteLine();
            }
        }
        Console.WriteLine(new string('=', 50));

        Dictionary<string, int> order = new Dictionary<string, int>();
        decimal total = 0;

        Console.WriteLine("\nФОРМА ЗАКАЗА");
        Console.WriteLine("Введите названия блюд и их количество");
        Console.WriteLine("(для завершения заказа нажмите 2)");

        while (true)
        {
            Console.WriteLine("\n" + new string('─', 30));
            Console.WriteLine("1. Добавить блюдо");
            Console.WriteLine("2. ЗАВЕРШИТЬ ЗАКАЗ");
            Console.WriteLine(new string('─', 30));

            int choice = GetValidInt("Выберите действие: ");

            if (choice == 2)
            {
                break;
            }
            else if (choice == 1)
            {
                Console.Write("\nВведите название блюда: ");
                string dishName = Console.ReadLine();

                var dish = menu.FirstOrDefault(d =>
                    d.Name.Equals(dishName, StringComparison.OrdinalIgnoreCase));

                if (dish == null)
                {
                    Console.WriteLine("❌ Блюдо не найдено. Проверьте название.");
                    continue;
                }

                Console.Write($"Количество порций '{dish.Name}': ");
                int quantity = GetValidInt("");

                if (order.ContainsKey(dish.Name))
                {
                    order[dish.Name] += quantity;
                }
                else
                {
                    order[dish.Name] = quantity;
                }

                total += dish.Price * quantity;
                Console.WriteLine($"Добавлено: {dish.Name} x{quantity}");

                // Показываем текущую корзину
                Console.WriteLine("\n" + new string('─', 30));
                Console.WriteLine("ТЕКУЩАЯ КОРЗИНА:");
                foreach (var item in order)
                {
                    var dishItem = menu.First(d => d.Name == item.Key);
                    decimal itemTotal = dishItem.Price * item.Value;
                    Console.WriteLine($"   {item.Key} x{item.Value} = {itemTotal} руб.");
                }
                Console.WriteLine($"ПРЕДВАРИТЕЛЬНЫЙ ИТОГ: {total} руб.");
                Console.WriteLine(new string('─', 30));
            }
            else
            {
                Console.WriteLine("Неверный выбор. Попробуйте снова.");
            }
        }

        if (order.Count == 0)
        {
            Console.WriteLine("Заказ отменен.");
            return;
        }

        // Показываем итоговый заказ
        Console.WriteLine("\n" + new string('═', 50));
        Console.WriteLine("ВАШ ЗАКАЗ ПОДГОТОВЛЕН!");
        Console.WriteLine(new string('═', 50));

        foreach (var item in order)
        {
            var dish = menu.First(d => d.Name == item.Key);
            decimal itemTotal = dish.Price * item.Value;
            Console.WriteLine($"🍴 {item.Key} x{item.Value} = {itemTotal} руб.");
        }

        Console.WriteLine(new string('─', 50));
        Console.WriteLine($" ОБЩАЯ СУММА: {total} руб.");
        Console.WriteLine(new string('═', 50));

        Console.WriteLine("\nПодтвердить заказ? (да/нет)");
        string confirm = Console.ReadLine();

        if (confirm.ToLower() == "да")
        {
            Console.WriteLine("\n" + new string('⭐', 50));
            Console.WriteLine(" ЗАКАЗ ПОДТВЕРЖДЕН! Спасибо за заказ!");
            Console.WriteLine($" К ОПЛАТЕ: {total} руб.");
            Console.WriteLine(" Ваш заказ готовится...");
            Console.WriteLine(new string('⭐', 50));
        }
        else
        {
            Console.WriteLine("Заказ отменен.");
        }
    }

    static void ShowMenuGuest()
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("  НАШЕ МЕНЮ");
        Console.WriteLine(new string('=', 50));

        var categories = menu.Select(d => d.Category).Distinct();

        foreach (var category in categories)
        {
            Console.WriteLine($"\n {category.ToUpper()}:");
            Console.WriteLine(new string('─', 40));

            var dishesInCategory = menu.Where(d => d.Category == category);
            foreach (var dish in dishesInCategory)
            {
                Console.WriteLine($"    {dish.Name} - {dish.Price} руб.");
            }
        }
        Console.WriteLine(new string('=', 50));
    }

    static void ReserveTableGuest()
    {
        ShowRestaurantMapGuest();

        int tableNumber = GetValidInt("Выберите стол для бронирования: ");
        if (!tables.Any(t => t.TableId == tableNumber))
        {
            Console.WriteLine("Неверный номер стола.");
            return;
        }

        Console.WriteLine("Введите ваше имя:");
        string clientName = Console.ReadLine();

        Console.WriteLine("Введите дату бронирования (дд.мм.гг):");
        string date = Console.ReadLine();

        Console.WriteLine("Введите время бронирования (например, 19:00):");
        string time = Console.ReadLine();

        var table = tables.First(t => t.TableId == tableNumber);
        if (!table.Schedule.ContainsKey(date))
        {
            table.Schedule[date] = new string[10];
        }

        var schedule = table.Schedule[date];
        if (Array.Exists(schedule, t => t == time))
        {
            Console.WriteLine("Это время уже занято!");
            return;
        }

        for (int i = 0; i < schedule.Length; i++)
        {
            if (schedule[i] == null)
            {
                schedule[i] = time;
                break;
            }
        }

        int guestId = guestIdCounter++;
        var newBooking = new Booking(tableNumber, clientName, guestId, time, date);
        bookings[guestId] = newBooking;

        Console.WriteLine($" Стол {tableNumber} успешно забронирован!");
        Console.WriteLine($" Ваш ID гостя: {guestId}");
    }

    static void ShowRestaurantMapGuest()
    {
        Console.WriteLine("\n=== КАРТА РЕСТОРАНА ===");
        ShowAllTables();
    }

    static int GetValidInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                return result;
            }
            Console.WriteLine("Неверный ввод. Пожалуйста, введите целое число.");
        }
    }

    static decimal GetValidDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out decimal result))
            {
                return result;
            }
            Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
        }
    }
}