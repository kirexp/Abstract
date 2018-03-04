using DAL.Entities.Account;

namespace DAL.Repositories {
    public class PermissionRepository : Repository<Permission> {
        public PermissionRepository(Repository repository)
            : base(repository) {
        }

        public PermissionRepository() {
        }

        public void InitPermissions() {
            if (!this.Any(x => x.Name == Constants.Permission.AdministrationPanelView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.AdministrationPanelView,
                    NameRu = "Панели администрирования. Просмотр",
                });
            }

            if (!this.Any(x => x.Name == Constants.Permission.AdministrationPanelEmployeeEdit)) {
                var editEmployeePermission = new Permission {
                    Name = Constants.Permission.AdministrationPanelEmployeeEdit,
                    NameRu = "Пользователи. Создание/редактирование",
                };
                this.Insert(editEmployeePermission);
            }

            if (!this.Any(x => x.Name == Constants.Permission.AdministrationRoleView)) {
                var roleViewPermission = new Permission {
                    Name = Constants.Permission.AdministrationRoleView,
                    NameRu = "Роли. Просмотр",
                };
                this.Insert(roleViewPermission);
            }
            if (!this.Any(x => x.Name == Constants.Permission.AdministrationRoleEdit)) {
                var roleEditPerm = new Permission {
                    Name = Constants.Permission.AdministrationRoleEdit,
                    NameRu = "Роли. Создание/редактирование",
                };
                this.Insert(roleEditPerm);
            }
            // Статистика
            if (!this.Any(x => x.Name == Constants.Permission.StatisticsView))
            {
                this.Insert(new Permission
                {
                    Name = Constants.Permission.StatisticsView,
                    NameRu = "Просмотр статистики",
                });
            }
        }
    }
}