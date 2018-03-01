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
                    NameKz = "Панели администрирования. Просмотр",
                });
            }

            if (!this.Any(x => x.Name == Constants.Permission.AdministrationPanelEmployeeEdit)) {
                var editEmployeePermission = new Permission {
                    Name = Constants.Permission.AdministrationPanelEmployeeEdit,
                    NameRu = "Пользователи. Создание/редактирование",
                    NameKz = "Пользователи. Создание/редактирование",
                };
                this.Insert(editEmployeePermission);
            }

            if (!this.Any(x => x.Name == Constants.Permission.AdministrationRoleView)) {
                var roleViewPermission = new Permission {
                    Name = Constants.Permission.AdministrationRoleView,
                    NameRu = "Роли. Просмотр",
                    NameKz = "Роли. Просмотр",
                };
                this.Insert(roleViewPermission);
            }
            if (!this.Any(x => x.Name == Constants.Permission.AdministrationRoleEdit)) {
                var roleEditPerm = new Permission {
                    Name = Constants.Permission.AdministrationRoleEdit,
                    NameRu = "Роли. Создание/редактирование",
                    NameKz = "Роли. Создание/редактирование",
                };
                this.Insert(roleEditPerm);
            }
            
            if (!this.Any(x => x.Name == Constants.Permission.PrivilegedView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.PrivilegedView,
                    NameRu = "База данных льготников. Просмотр",
                    NameKz = "База данных льготников. Просмотр",
                });
            }

            if (!this.Any(x => x.Name == Constants.Permission.PrivilegedEdit)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.PrivilegedEdit,
                    NameRu = "База данных льготников. Создание/редактирование",
                    NameKz = "База данных льготников. Создание/редактирование",
                });
            }

            if (!this.Any(x => x.Name == Constants.Permission.DayCareQueueView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.DayCareQueueView,
                    NameRu = "Справочники ДДО. Просмотр",
                    NameKz = "Справочники ДДО. Просмотр",
                });
            }

            if (!this.Any(x => x.Name == Constants.Permission.DayCareQueueEdit)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.DayCareQueueEdit,
                    NameRu = "Справочники ДДО. Создание/редактирование",
                    NameKz = "Справочники ДДО. Создание/редактирование",
                });
            }

            if (!this.Any(x => x.Name == Constants.Permission.DayCareQueueFreePlacesView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.DayCareQueueFreePlacesView,
                    NameRu = "База свободных мест в ДДО. Просмотр",
                    NameKz = "База свободных мест в ДДО. Просмотр",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.DayCareQueueFreePlacesEdit)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.DayCareQueueFreePlacesEdit,
                    NameRu = "База свободных мест в ДДО. Добавление свободных мест",
                    NameKz = "База свободных мест в ДДО. Добавление свободных мест",
                });
            }

            if (!this.Any(x => x.Name == Constants.Permission.DayCareQueueFreePlacesListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.DayCareQueueFreePlacesListView,
                    NameRu = "Журнал очередей ДДО. Просмотр",
                    NameKz = "Журнал очередей ДДО. Просмотр",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.DayCareQueueFreePlacesSetInQueue)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.DayCareQueueFreePlacesSetInQueue,
                    NameRu = "Журнал очередей ДДО. Постановка в очередь",
                    NameKz = "Журнал очередей ДДО. Постановка в очередь",
                });
            }

            if (!this.Any(x => x.Name == Constants.Permission.RequestListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.RequestListView,
                    NameRu = "Журнал заявлений. Просмотр",
                    NameKz = "Журнал заявлений. Просмотр",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.RequestListEdit)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.RequestListEdit,
                    NameRu = "Журнал заявлений. Обновление данных",
                    NameKz = "Журнал заявлений. Обновление данных",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.RequestListRemovalFromQueue)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.RequestListRemovalFromQueue,
                    NameRu = "Журнал заявлений. Снятие с очереди",
                    NameKz = "Журнал заявлений. Снятие с очереди",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.RequestListCheckDayCareQueue)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.RequestListCheckDayCareQueue,
                    NameRu = "Журнал заявлений. Выбор ДДО",
                    NameKz = "Журнал заявлений. Выбор ДДО",
                });
            }

            if (!this.Any(x => x.Name == Constants.Permission.ArchiveView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.ArchiveView,
                    NameRu = "Архив. Просмотр",
                    NameKz = "Архив. Просмотр",
                });
            }

            if (!this.Any(x => x.Name == Constants.Permission.Housing2020RequestListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.Housing2020RequestListView,
                    NameRu = "Журнал заявлений программы Жилье. Просмотр",
                    NameKz = "Журнал заявлений программы Жилье. Просмотр",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.DayCareQueueReportView))
            {
                this.Insert(new Permission
                {
                    Name = Constants.Permission.DayCareQueueReportView,
                    NameRu = "Просмотр общего журнала свободных мест",
                    NameKz = "Просмотр общего журнала свободных мест",
                });
            }
            // База данных Е-попечительства 
            if (!this.Any(x => x.Name == Constants.Permission.EtrusteeshipListView))
            {
                this.Insert(new Permission
                {
                    Name = Constants.Permission.EtrusteeshipListView,
                    NameRu = "Просмотр Е-попечительство",
                    NameKz = "Просмотр Е-попечительство",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.EtrusteeshipListEdit))
            {
                this.Insert(new Permission
                {
                    Name = Constants.Permission.EtrusteeshipListEdit,
                    NameRu = "Обработка Е-попечительство",
                    NameKz = "Обработка Е-попечительство",
                });
            }
            // Выдача справок по опеке и попечительству
            if (!this.Any(x => x.Name == Constants.Permission.TrusteeshipCertIssueListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.TrusteeshipCertIssueListView,
                    NameRu = "Просмотр заявок на выдачу справок по опеке",
                    NameKz = "Просмотр заявок на выдачу по опеке",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.TrusteeshipCertIssueListEdit)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.TrusteeshipCertIssueListEdit,
                    NameRu = "Обработка заявок на выдачу справок по опеке",
                    NameKz = "Обработка заявок на выдачу справок по опеке",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.TrusteeshipCertIssueReqSubmit)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.TrusteeshipCertIssueReqSubmit,
                    NameRu = "Подача заявок на выдачу справок по опеке от заявителя",
                    NameKz = "Подача заявок на выдачу справок по опеке от заявителя",
                });
            }
            // Назначение выплаты пособия опекунам или попечителям на содержание ребенка-сироты
            if (!this.Any(x => x.Name == Constants.Permission.AllowancePaymentsListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.AllowancePaymentsListView,
                    NameRu = "Просмотр заявок по выплате пособия опекунам",
                    NameKz = "Просмотр заявок по выплате пособия опекунам",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.AllowancePaymentsListEdit)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.AllowancePaymentsListEdit,
                    NameRu = "Обработка заявок по выплате пособия опекунам",
                    NameKz = "Обработка заявок по выплате пособия опекунам",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.AllowancePaymentsReqSubmit)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.AllowancePaymentsReqSubmit,
                    NameRu = "Подача заявок заявок по выплате пособия опекунам заявителя",
                    NameKz = "Подача заявок заявок по выплате пособия опекунам от заявителя",
                });
            }
            // Установление опеки или попечительства над ребенком-сиротой (детьми-сиротами) и ребенком (детьми)
            if (!this.Any(x => x.Name == Constants.Permission.TrusteeshipListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.TrusteeshipListView,
                    NameRu = "Просмотр заявок на установление опеки или попечительства",
                    NameKz = "Просмотр заявок на установление опеки или попечительства",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.TrusteeshipListEdit)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.TrusteeshipListEdit,
                    NameRu = "Обработка заявок на установление опеки или попечительства",
                    NameKz = "Обработка заявок на установление опеки или попечительства",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.TrusteeshipReqSubmit)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.TrusteeshipReqSubmit,
                    NameRu = "Подача заявок на установление опеки или попечительства от заявителя",
                    NameKz = "Подача заявок на установление опеки или попечительства от заявителя",
                });
            }
            // Прочие услуги с доступом только на просмотр списка
            if (!this.Any(x => x.Name == Constants.Permission.CitizensInNeedHousingListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.CitizensInNeedHousingListView,
                    NameRu = "Просмотр заявок граждан, нуждающихся в жилье",
                    NameKz = "Просмотр заявок граждан, нуждающихся в жилье",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.HousingAssistanceListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.HousingAssistanceListView,
                    NameRu = "Просмотр заявок на назначение жилищной помощи",
                    NameKz = "Просмотр заявок на назначение жилищной помощи",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.UnemployedRegistrationListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.UnemployedRegistrationListView,
                    NameRu = "Просмотр заявок по постановке на учет безработных граждан",
                    NameKz = "Просмотр заявок по постановке на учет безработных граждан",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.ProbateMinorsListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.ProbateMinorsListView,
                    NameRu = "Просмотр заявок по выдаче справок в пенс. фонды для МВД",
                    NameKz = "Просмотр заявок по выдаче справок в пенс. фонды для МВД",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.ProbateMinorsToPensionFoundListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.ProbateMinorsToPensionFoundListView,
                    NameRu = "Просмотр заявок по выдаче справок в пенс. фонды для оформления наследства",
                    NameKz = "Просмотр заявок по выдаче справок в пенс. фонды для оформления наследства",
                });
            }
            // Прием граждан
            if (!this.Any(x => x.Name == Constants.Permission.ReceptionOfCitizensListView))
            {
                this.Insert(new Permission
                {
                    Name = Constants.Permission.ReceptionOfCitizensListView,
                    NameRu = "Прием граждан. Просмотр заявлений",
                    NameKz = "Прием граждан. Просмотр заявлений",
                });
            }
            // Назначение выплаты пособия опекунам или попечителям на содержание ребенка-сироты
            if (!this.Any(x => x.Name == Constants.Permission.ArchiveTranscriptListView)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.ArchiveTranscriptListView,
                    NameRu = "Просмотр заявок по выдаче архивных справок",
                    NameKz = "Просмотр заявок по выдаче архивных справок",
                });
            }
            if (!this.Any(x => x.Name == Constants.Permission.ArchiveTranscriptListEdit)) {
                this.Insert(new Permission {
                    Name = Constants.Permission.ArchiveTranscriptListEdit,
                    NameRu = "Обработка заявок по выдаче архивных справок",
                    NameKz = "Обработка заявок по выдаче архивных справок",
                });
            }
            // Статистика
            if (!this.Any(x => x.Name == Constants.Permission.StatisticsView))
            {
                this.Insert(new Permission
                {
                    Name = Constants.Permission.StatisticsView,
                    NameRu = "Просмотр статистики",
                    NameKz = "Просмотр статистики",
                });
            }
        }
    }
}