using System.Collections.Generic;
using System.Linq;

namespace CWLOTPH.DB
{
    public class DbInteraсtion
    {
        public static List<Material> getAllMaterialTypes()
        {
            using (var _db = new UserMTypeDBContext()) // Используем контекст базы данных
            {
                // Display all MaterialTypes from the database 
                var _query = from mType in _db.Material.Include("MaterialEmpiricalCoefficients").Include("MaterialProperties")  // Из таблицы типов материалов
                            orderby mType.Name             // Сортируя по имени
                            select mType;                  // Выбираем тип материала
                    return _query.ToList();              // Присваиваем как список материалов
            }
        }




        public static Material getMaterialType(string mTypeName)
        {
            using (var _db = new UserMTypeDBContext())      // Используя контекст базы данных
            {
                var _query = from matType in _db.Material.Include("MaterialEmpiricalCoefficients").Include("MaterialProperties")  // Из таблицы типов материалов
                            where matType.Name == mTypeName // По соответствию имени материала введенному
                            select matType;                 // Выбираем тип материала
                return _query.First();
            }
        }

        public static void addMaterialType(Material mType)
        {
            using (var _db = new UserMTypeDBContext())
            {
                _db.Material.Add(mType);          // Добавляем тип материала
                _db.SaveChanges();                    // Сохраняем изменения
            }

        }

        public static void updateMaterialType(Material updatedMType)
        {
            using (var _db = new UserMTypeDBContext())
            {
                var _original = _db.Material.Find(updatedMType.ID);        // Находим оригинальную запись по соответсвию имени

                if (_original != null)                                          // Если таковая найдена
                {
                    _db.Entry(_original).CurrentValues.SetValues(updatedMType);  // Обновляем запись
                    _db.SaveChanges();                                          // Сохраняем изменения
                }

            }
        }

        public static void deleteMaterialType(Material mType)
        {
            using (var _db = new UserMTypeDBContext())
            {
                _db.Material.Attach(mType);                                 // Приаттачиваем запись, чтобы избежать ошибки отсутствия записи
                _db.Material.Remove(mType);                                 // Удаляем запись
                _db.SaveChanges();                                              // Сохраняем изменения
            }
        }

        public static List<User> getAllUserRecords()
        {
            using (var _db = new UserMTypeDBContext())        // Используя контекст БД
            {
                // Display all Users from the database 
                var _query = from user in _db.User.Include("AdditiveData")              // из таблицы Пользователей
                            orderby user.ID                   // Сортируя по ID
                            select user;                      // Выбираем пользователей
                return _query.ToList();           // Присваиваем как список пользователей
            }
        }

        public static User getUser(string userLogin)
        {
            using (var _db = new UserMTypeDBContext())     // Используя контекст БД
            {
                var _query = from user  in _db.User.Include("AdditiveData")           // Из таблицы пользователей
                            where user.Login == userLogin        // По соответствию ID введенному
                            select user;                   // Выбираем пользователя
                return _query.First();
            }
        }

        public static void addUser(User user)
        {
            using (var _db = new UserMTypeDBContext())
            {
                _db.User.Add(user);          // Добавляем пользователя
                _db.SaveChanges();           // Сохраняем изменения
            }
        }

        public static void updateUser(User updatedUser)
        {
            using (var _db = new UserMTypeDBContext())
            {
                var _originalUser = _db.User.Find(updatedUser.ID);          // Выбираем оригинальную запись по ID

                if (_originalUser != null)                                         // Если таковая существует
                {
                    _db.Entry(_originalUser).CurrentValues.SetValues(updatedUser);  // Обновляем запись
                    _db.SaveChanges();                                              // Сохраняем изменения
                }
            }

        }

        public static void deleteUser(User user)
        {

            using (var _db = new UserMTypeDBContext())
            {
                _db.User.Attach(user);             // Приаттачиваем пользователя для избежания ошибки при отсутствии такого пользователя
                _db.User.Remove(user);             // Удаляем пользователя
                _db.SaveChanges();                 // Сохраняем изменения
            }

        }
    }
}
