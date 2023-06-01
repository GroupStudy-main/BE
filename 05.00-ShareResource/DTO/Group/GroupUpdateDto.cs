using ShareResource.Enums;

namespace ShareResource.DTO
{
    public class GroupUpdateDto : BaseUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public virtual ICollection<SubjectEnum> SubjectIds { get; set; }
    }
}
