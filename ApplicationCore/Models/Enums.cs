using System.ComponentModel;

namespace ApplicationCore.Models
{
    public enum SqlOperationType
    {
        [Description("CREATE")] CREATE = 1,
        [Description("UPDATE")] UPDATE = 2,
        [Description("DELETE")] DELETE = 3,
    }
}
