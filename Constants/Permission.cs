namespace Constants
{
    public static class Permission
    {
        #region Панель администрирования
        public const string AdministrationPanelView = "AdministrationPanelView";
        public const string AdministrationPanelEmployeeEdit = "AdministrationPanelEmployeeEdit";
        public const string AdministrationRoleEdit = "AdministrationRoleEdit";
        public const string AdministrationRoleView = "AdministrationRoleView";
        #endregion

        #region База данных льготников (просмотр и добавление, редактирование)
        public const string PrivilegedView = "PrivilegedView";
        public const string PrivilegedEdit = "PrivilegedEdit";
        #endregion

        #region Справочники ДДО (добавление новых записей, редактирование)
        public const string DayCareQueueView = "DayCareQueueView";
        public const string DayCareQueueEdit = "DayCareQueueEdit";
        #endregion

        #region База свободных мест в ДДО (просмотр, добавление свободных мест)
        public const string DayCareQueueFreePlacesView = "DayCareQueueFreePlacesView";
        public const string DayCareQueueFreePlacesEdit = "DayCareQueueFreePlacesEdit";
        public const string DayCareQueueReportView = "DayCareQueueReportView";
        #endregion

        #region Журнал очередей ДДО (Просмотр, Постановка в очередь)
        public const string DayCareQueueFreePlacesListView = "DayCareQueueFreePlacesListView";
        public const string DayCareQueueFreePlacesSetInQueue = "DayCareQueueFreePlacesSetInQueue";
        #endregion

        #region Журнал заявлений (просмотр, снятие с очереди, обновление данных)
        public const string RequestListView = "RequestListView";
        public const string RequestListEdit = "RequestListEdit";
        public const string RequestListRemovalFromQueue = "RequestListRemovalFromQueue";
        public const string RequestListCheckDayCareQueue = "RequestListCheckDayCareQueue";
        #endregion

        #region "Жилье 2020" (№5)
        public const string Housing2020RequestListView = "Housing2020RequestListView";
        #endregion

        #region Архив (просмотр)
        public const string ArchiveView = "ArchiveView";
        #endregion

        #region E-попечительство
        public const string EtrusteeshipListView = "EtrusteeshipListView";
        public const string EtrusteeshipListEdit = "EtrusteeshipListEdit";
        #endregion

        #region Установление опеки или попечительства над ребенком-сиротой (№2)
        public const string TrusteeshipListView = "TrusteeshipListView";
        public const string TrusteeshipListEdit = "TrusteeshipListEdit";
        public const string TrusteeshipReqSubmit = "TrusteeshipReqSubmit";
        #endregion

        #region Выдача справок по опеке и попечительству (№11)
        public const string TrusteeshipCertIssueListView = "TrusteeshipCertIssueListView";
        public const string TrusteeshipCertIssueListEdit = "TrusteeshipCertIssueListEdit";
        public const string TrusteeshipCertIssueReqSubmit = "TrusteeshipCertIssueReqSubmit";
        #endregion

        #region Назначение выплаты пособия опекунам или попечителям (№12)
        public const string AllowancePaymentsListView = "AllowancePaymentsListView";
        public const string AllowancePaymentsListEdit = "AllowancePaymentsListEdit";
        public const string AllowancePaymentsReqSubmit = "AllowancePaymentsReqSubmit";
        #endregion

        #region Выдача архивных справок (№13)
        public const string ArchiveTranscriptListView = "ArchiveTranscriptListView";
        public const string ArchiveTranscriptListEdit = "ArchiveTranscriptListEdit";
        #endregion

        #region Прочие услуги, для которых нужен только просмотр списка (№ 3,7,8,9,10)
        public const string CitizensInNeedHousingListView = "CitizensInNeedHousingListView";
        public const string HousingAssistanceListView = "HousingAssistanceListView";
        public const string UnemployedRegistrationListView = "UnemployedRegistrationListView";
        public const string ProbateMinorsListView = "ProbateMinorsListView";
        public const string ProbateMinorsToPensionFoundListView = "ProbateMinorsToPensionFoundListView";
        #endregion

        #region Прием граждан
        public const string ReceptionOfCitizensListView = "ReceptionOfCitizensListView";
        #endregion

        #region Земельные участки
        public const string QueueToTheLandProcess = "QueueToTheLandProcess";
        #endregion
        
        #region Статистика
        public const string StatisticsView = "StatisticsView";
        #endregion
    }
}
