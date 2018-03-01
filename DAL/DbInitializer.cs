using System;
using System.Collections.Generic;
using DAL.Entities.Account;
using DAL.Repositories;
using Enums;
using Microsoft.AspNet.Identity;
using Permission = DAL.Entities.Account.Permission;
namespace DAL {
    public class DbInitializer {
        public  void Seed() {
            using (var repository = new Repository<IEntity>()) {           
                this.InitUsers(repository);
                repository.Commit();
            }
        }

        private void InitUsers(Repository rep) {
            var hasher = new PasswordHasher();
            var permissionsRepository = new Repository<Entities.Account.Permission>(rep);
            var userRep = new Repository<User>(rep);
            if (!userRep.Any()) {
                var rolesRepository = new Repository<Entities.Account.Role>(rep);
                var administrationPanelView = new Permission {
                    Name = Constants.Permission.AdministrationPanelView,
                    NameRu = "Панели администрирования. Просмотр",
                    NameKz = "Панели администрирования. Просмотр",
                };
                permissionsRepository.Insert(administrationPanelView);
                var editEmployeePermission = new Permission {
                    Name = Constants.Permission.AdministrationPanelEmployeeEdit,
                    NameRu = "Пользователи. Создание/редактирование",
                    NameKz = "Пользователи. Создание/редактирование",
                };
                permissionsRepository.Insert(editEmployeePermission);
                var roleViewPermission = new Permission {
                    Name = Constants.Permission.AdministrationRoleView,
                    NameRu = "Роли. Просмотр",
                    NameKz = "Роли. Просмотр",
                };
                permissionsRepository.Insert(roleViewPermission);
                var roleEditPerm = new Permission {
                    Name = Constants.Permission.AdministrationRoleEdit,
                    NameRu = "Роли. Создание/редактирование",
                    NameKz = "Роли. Создание/редактирование",
                };
                permissionsRepository.Insert(roleEditPerm);
                var privilegedView = new Permission {
                    Name = Constants.Permission.PrivilegedView,
                    NameRu = "База данных льготников. Просмотр",
                    NameKz = "База данных льготников. Просмотр",
                };
                permissionsRepository.Insert(privilegedView);
                var statisticsView = new Permission {
                    Name = Constants.Permission.StatisticsView,
                    NameRu = "Просмотр статистики",
                    NameKz = "Просмотр статистики",
                };
                permissionsRepository.Insert(statisticsView);
                var adminRole = new Role {
                    Description = "Admin-All-Permissions",
                    Name = "Admin",
                    Permissions = new HashSet<Permission>() {
                        administrationPanelView,
                        editEmployeePermission,
                        roleViewPermission,
                        roleEditPerm,
                        privilegedView,
                        statisticsView,
                    }
                };
                rolesRepository.Insert(adminRole);
                var admin = new User {
                    Email = "testSS@gomeol.com",
                    Password = hasher.HashPassword("ASD123qwe"),
                    UserName = "111122223333",
                    UserType = UserTypeEnum.Admin,
                    Roles = new HashSet<Role>() {adminRole},
                    LastPasswordChangedDate = DateTime.Now,
                };
                userRep.Insert(admin);
            }

        }
    }
}