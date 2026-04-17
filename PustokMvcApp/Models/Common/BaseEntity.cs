namespace PustokMvcApp.Models.Common
{
    public abstract class BaseEntity
    {
        //guid bize unique idler verir, her entitynin idsi unique olur
        public Guid Id { get; init; }
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
    //public class AuditableEntity : BaseEntity
    //{
    //    public DateTime CreatedAt { get; set; }
    //    public DateTime? UpdatedAt { get; set; }
    //}
}
