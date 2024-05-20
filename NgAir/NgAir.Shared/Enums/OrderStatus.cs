using System.ComponentModel;

namespace NgAir.Shared.Enums
{
    public enum OrderStatus
    {
        [Description("New")]
        New,

        [Description("Dispatched")]
        Dispatched,

        [Description("Sent")]
        Sent,

        [Description("Confirmed")]
        Confirmed,

        [Description("Cancelled")]
        Cancelled
    }
}
