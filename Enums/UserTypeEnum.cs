using System.ComponentModel.DataAnnotations;

namespace Enums {
    public enum UserTypeEnum {
        [Display(Name = "Пусто")]
        None = 0,
        [Display(Name = "Работник")]
        Employee = 1,
        [Display(Name = "Клиент")]
        Client = 2,
        [Display(Name = "Админ")]
        Admin = 3
    }
}